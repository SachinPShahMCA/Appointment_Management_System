using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Appointment_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class AddAppointmentFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Appointments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoctorComment",
                table: "Appointments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Appointments",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "DoctorComment",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Appointments");
        }
    }
}
