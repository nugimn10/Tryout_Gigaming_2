using Microsoft.EntityFrameworkCore.Migrations;

namespace notification_services.Migrations
{
    public partial class initNewMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email_destionation",
                table: "Notification_Logs");

            migrationBuilder.AddColumn<string>(
                name: "email_destination",
                table: "Notification_Logs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email_destination",
                table: "Notification_Logs");

            migrationBuilder.AddColumn<string>(
                name: "email_destionation",
                table: "Notification_Logs",
                type: "text",
                nullable: true);
        }
    }
}
