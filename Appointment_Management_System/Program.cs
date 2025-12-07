using Appointment_Management_System.BL.Interface;
using Appointment_Management_System.BL.Repositories;
using Appointment_Management_System.Data;
using Appointment_Management_System.BL.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using System;
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

builder.Services.AddControllers();
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


var app = builder.Build();
app.UseCors("AllowAngular");

app.UseMiddleware<ExceptionMiddleware>();


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
