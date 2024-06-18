using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Swaed.Migrations
{
    /// <inheritdoc />
    public partial class GestEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Events",
                newName: "Program");

            migrationBuilder.RenameColumn(
                name: "Objectives",
                table: "Events",
                newName: "TrainingType");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "AbuDhabiLicenseRequired",
                table: "Events",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AgeRequiredFrom",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AgeRequiredTo",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cause",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactInformation",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CovidVaccinationRequired",
                table: "Events",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EventDuration",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EventType",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GenderRequired",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPrivateEvent",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LanguageRequired",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinimumHoursRequired",
                table: "Events",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationalityRequired",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Timing",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrainingObjectives",
                table: "Events",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VolunteersRequired",
                table: "Events",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AbuDhabiLicenseRequired",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AgeRequiredFrom",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "AgeRequiredTo",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Cause",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ContactInformation",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CovidVaccinationRequired",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventDuration",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventType",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "GenderRequired",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "IsPrivateEvent",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "LanguageRequired",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "MinimumHoursRequired",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "NationalityRequired",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Timing",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TrainingObjectives",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "VolunteersRequired",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "TrainingType",
                table: "Events",
                newName: "Objectives");

            migrationBuilder.RenameColumn(
                name: "Program",
                table: "Events",
                newName: "Title");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
