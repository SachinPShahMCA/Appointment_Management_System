using Appointment_Management_System.BL.ExtensionMethods;
using Appointment_Management_System.BL.Interface;
using Appointment_Management_System.BL.Repositories;
using Appointment_Management_System.Data;
using Appointment_Management_System.Observability.Extensions;
using Appointment_Management_System.Observability.Middlewares;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
        });
});

// Add services to the container.
builder.Services.AddControllers(options => {
    options.Filters.Add<Appointment_Management_System.Filters.ValidateModelAttribute>();
    options.Filters.Add<Appointment_Management_System.Filters.ResponseWrapper>();
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new UtcDateTimeConverter());
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddObservability();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SQLite DB
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("Default") ?? "Data Source=appointments.db"));

// Repositories & UnitOfWork
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IPatient, PatientRepository>();
builder.Services.AddScoped<IDoctor, DoctorRepository>();
builder.Services.AddScoped<IAppointment, AppointmentRepository>();
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Host.UseSerilog();
builder.Services.AddHealthChecks()
    .AddDbContextCheck<AppDbContext>();


var app = builder.Build();
app.UseCors("AllowAngular");

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
     .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithEnvironmentName()
    .WriteTo.File(
        path: "Logs/app-.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 14,
        outputTemplate:
            "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] " +
            "Env={EnvironmentName} TenantId={TenantId} UserId={UserId} CorrelationId={CorrelationId} " +
            "{Message:lj}{NewLine}{Exception}"
    )
    .CreateLogger();



app.MapHealthChecks("/health");
app.UseMiddleware<LogContextMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<PerformanceMiddleware>();
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}
app.Run();
