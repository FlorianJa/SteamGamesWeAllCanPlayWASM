using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SteamGamesWeAllCanPlayWASM.Shared.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerSummaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MPlayerSummaryId = table.Column<int>(type: "INTEGER", nullable: true),
                    SteamId = table.Column<ulong>(type: "INTEGER", nullable: false),
                    ProfileVisibility = table.Column<int>(type: "INTEGER", nullable: false),
                    ProfileState = table.Column<uint>(type: "INTEGER", nullable: false),
                    Nickname = table.Column<string>(type: "TEXT", nullable: true),
                    LastLoggedOffDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CommentPermission = table.Column<int>(type: "INTEGER", nullable: false),
                    ProfileUrl = table.Column<string>(type: "TEXT", nullable: true),
                    AvatarUrl = table.Column<string>(type: "TEXT", nullable: true),
                    AvatarMediumUrl = table.Column<string>(type: "TEXT", nullable: true),
                    AvatarFullUrl = table.Column<string>(type: "TEXT", nullable: true),
                    UserStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    RealName = table.Column<string>(type: "TEXT", nullable: true),
                    PrimaryGroupId = table.Column<string>(type: "TEXT", nullable: true),
                    AccountCreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CountryCode = table.Column<string>(type: "TEXT", nullable: true),
                    StateCode = table.Column<string>(type: "TEXT", nullable: true),
                    CityCode = table.Column<uint>(type: "INTEGER", nullable: false),
                    PlayingGameName = table.Column<string>(type: "TEXT", nullable: true),
                    PlayingGameId = table.Column<string>(type: "TEXT", nullable: true),
                    PlayingGameServerIP = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerSummaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerSummaries_PlayerSummaries_MPlayerSummaryId",
                        column: x => x.MPlayerSummaryId,
                        principalTable: "PlayerSummaries",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SteamId = table.Column<string>(type: "TEXT", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSummaries_MPlayerSummaryId",
                table: "PlayerSummaries",
                column: "MPlayerSummaryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerSummaries");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
