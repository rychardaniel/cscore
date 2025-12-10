using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cscore.API.Migrations
{
    /// <inheritdoc />
    public partial class ExpandMatchModelAndAddParticipants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "type_match",
                table: "match",
                newName: "status");

            migrationBuilder.AddColumn<DateTime>(
                name: "finished_at",
                table: "match",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "mongo_score_id",
                table: "match",
                type: "character varying(24)",
                maxLength: 24,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "notes",
                table: "match",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "scheduled_date",
                table: "match",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "sport_type",
                table: "match",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "started_at",
                table: "match",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "venue",
                table: "match",
                type: "character varying(200)",
                maxLength: 200,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "match_participant",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    match_id = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    side = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    logo_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    result = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_match_participant", x => x.id);
                    table.ForeignKey(
                        name: "FK_match_participant_match_match_id",
                        column: x => x.match_id,
                        principalTable: "match",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_match_participant_match_id",
                table: "match_participant",
                column: "match_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "match_participant");

            migrationBuilder.DropColumn(
                name: "finished_at",
                table: "match");

            migrationBuilder.DropColumn(
                name: "mongo_score_id",
                table: "match");

            migrationBuilder.DropColumn(
                name: "notes",
                table: "match");

            migrationBuilder.DropColumn(
                name: "scheduled_date",
                table: "match");

            migrationBuilder.DropColumn(
                name: "sport_type",
                table: "match");

            migrationBuilder.DropColumn(
                name: "started_at",
                table: "match");

            migrationBuilder.DropColumn(
                name: "venue",
                table: "match");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "match",
                newName: "type_match");
        }
    }
}
