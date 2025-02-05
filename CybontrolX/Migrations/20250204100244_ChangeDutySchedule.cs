using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CybontrolX.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDutySchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DutyDates",
                table: "DutySchedules");

            migrationBuilder.Sql(
                "ALTER TABLE \"Employee\" ALTER COLUMN \"ShiftStart\" TYPE interval USING (\"ShiftStart\" - '2000-01-01 00:00:00'::timestamp);");

            migrationBuilder.Sql(
                "ALTER TABLE \"Employee\" ALTER COLUMN \"ShiftEnd\" TYPE interval USING (\"ShiftEnd\" - '2000-01-01 00:00:00'::timestamp);");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ShiftStart",
                table: "Employee",
                type: "interval",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "ShiftEnd",
                table: "Employee",
                type: "interval",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DutyDate",
                table: "DutySchedules",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ShiftEnd",
                table: "DutySchedules",
                type: "interval",
                nullable: false,
                defaultValue: TimeSpan.Zero);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "ShiftStart",
                table: "DutySchedules",
                type: "interval",
                nullable: false,
                defaultValue: TimeSpan.Zero);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DutyDate",
                table: "DutySchedules");

            migrationBuilder.DropColumn(
                name: "ShiftEnd",
                table: "DutySchedules");

            migrationBuilder.DropColumn(
                name: "ShiftStart",
                table: "DutySchedules");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ShiftStart",
                table: "Employee",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "interval",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ShiftEnd",
                table: "Employee",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "interval",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DutyDates",
                table: "DutySchedules",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
