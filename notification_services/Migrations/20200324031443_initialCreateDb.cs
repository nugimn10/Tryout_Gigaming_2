using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace notification_services.Migrations
{
    public partial class initialCreateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(nullable: true),
                    message = table.Column<string>(nullable: true),
                    created_at = table.Column<DateTime>(nullable: false),
                    update_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Notification_Logs",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    notification_id = table.Column<int>(nullable: false),
                    type = table.Column<string>(nullable: true),
                    from = table.Column<int>(nullable: false),
                    target = table.Column<int>(nullable: false),
                    email_destionation = table.Column<string>(nullable: true),
                    read_at = table.Column<DateTime>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false),
                    update_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification_Logs", x => x.id);
                    table.ForeignKey(
                        name: "FK_Notification_Logs_Notification_notification_id",
                        column: x => x.notification_id,
                        principalTable: "Notification",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notification_Logs_notification_id",
                table: "Notification_Logs",
                column: "notification_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notification_Logs");

            migrationBuilder.DropTable(
                name: "Notification");
        }
    }
}
