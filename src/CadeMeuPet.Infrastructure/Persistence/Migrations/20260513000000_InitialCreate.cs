using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CadeMeuPet.Infrastructure.Persistence.Migrations;

public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Tutors",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                PhoneNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Tutors", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Pets",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TutorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                Species = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                Breed = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                Color = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Pets", x => x.Id);
                table.ForeignKey(
                    name: "FK_Pets_Tutors_TutorId",
                    column: x => x.TutorId,
                    principalTable: "Tutors",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Notifications",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                TutorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ScanHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Channel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                SentAt = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Notifications", x => x.Id);
                table.ForeignKey(
                    name: "FK_Notifications_Tutors_TutorId",
                    column: x => x.TutorId,
                    principalTable: "Tutors",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "QrCodes",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Code = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_QrCodes", x => x.Id);
                table.ForeignKey(
                    name: "FK_QrCodes_Pets_PetId",
                    column: x => x.PetId,
                    principalTable: "Pets",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "ScanHistories",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                IpAddress = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                UserAgent = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                Latitude = table.Column<decimal>(type: "decimal(9,6)", precision: 9, scale: 6, nullable: true),
                Longitude = table.Column<decimal>(type: "decimal(9,6)", precision: 9, scale: 6, nullable: true),
                ScannedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ScanHistories", x => x.Id);
                table.ForeignKey(
                    name: "FK_ScanHistories_Pets_PetId",
                    column: x => x.PetId,
                    principalTable: "Pets",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(name: "IX_Tutors_Email", table: "Tutors", column: "Email", unique: true);
        migrationBuilder.CreateIndex(name: "IX_Pets_TutorId", table: "Pets", column: "TutorId");
        migrationBuilder.CreateIndex(name: "IX_Notifications_ScanHistoryId", table: "Notifications", column: "ScanHistoryId");
        migrationBuilder.CreateIndex(name: "IX_Notifications_TutorId", table: "Notifications", column: "TutorId");
        migrationBuilder.CreateIndex(name: "IX_QrCodes_Code", table: "QrCodes", column: "Code", unique: true);
        migrationBuilder.CreateIndex(name: "IX_QrCodes_PetId", table: "QrCodes", column: "PetId", unique: true);
        migrationBuilder.CreateIndex(name: "IX_ScanHistories_PetId", table: "ScanHistories", column: "PetId");

        migrationBuilder.AddForeignKey(
            name: "FK_Notifications_ScanHistories_ScanHistoryId",
            table: "Notifications",
            column: "ScanHistoryId",
            principalTable: "ScanHistories",
            principalColumn: "Id");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Notifications");
        migrationBuilder.DropTable(name: "QrCodes");
        migrationBuilder.DropTable(name: "ScanHistories");
        migrationBuilder.DropTable(name: "Pets");
        migrationBuilder.DropTable(name: "Tutors");
    }
}
