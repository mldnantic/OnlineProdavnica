using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineProdavnica.Migrations
{
    public partial class v20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Radnje",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Radnje", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Artikli",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cena = table.Column<int>(type: "int", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RadnjaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artikli", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Artikli_Radnje_RadnjaID",
                        column: x => x.RadnjaID,
                        principalTable: "Radnje",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Racuni",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Iznos = table.Column<int>(type: "int", nullable: false),
                    RadnjaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racuni", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Racuni_Radnje_RadnjaID",
                        column: x => x.RadnjaID,
                        principalTable: "Radnje",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KorpaSpojevi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RadnjaID = table.Column<int>(type: "int", nullable: true),
                    ArtikalID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorpaSpojevi", x => x.ID);
                    table.ForeignKey(
                        name: "FK_KorpaSpojevi_Artikli_ArtikalID",
                        column: x => x.ArtikalID,
                        principalTable: "Artikli",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KorpaSpojevi_Radnje_RadnjaID",
                        column: x => x.RadnjaID,
                        principalTable: "Radnje",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RacunSpojevi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArtikalID = table.Column<int>(type: "int", nullable: true),
                    RacunID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RacunSpojevi", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RacunSpojevi_Artikli_ArtikalID",
                        column: x => x.ArtikalID,
                        principalTable: "Artikli",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RacunSpojevi_Racuni_RacunID",
                        column: x => x.RacunID,
                        principalTable: "Racuni",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Artikli_RadnjaID",
                table: "Artikli",
                column: "RadnjaID");

            migrationBuilder.CreateIndex(
                name: "IX_KorpaSpojevi_ArtikalID",
                table: "KorpaSpojevi",
                column: "ArtikalID");

            migrationBuilder.CreateIndex(
                name: "IX_KorpaSpojevi_RadnjaID",
                table: "KorpaSpojevi",
                column: "RadnjaID");

            migrationBuilder.CreateIndex(
                name: "IX_Racuni_RadnjaID",
                table: "Racuni",
                column: "RadnjaID");

            migrationBuilder.CreateIndex(
                name: "IX_RacunSpojevi_ArtikalID",
                table: "RacunSpojevi",
                column: "ArtikalID");

            migrationBuilder.CreateIndex(
                name: "IX_RacunSpojevi_RacunID",
                table: "RacunSpojevi",
                column: "RacunID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KorpaSpojevi");

            migrationBuilder.DropTable(
                name: "RacunSpojevi");

            migrationBuilder.DropTable(
                name: "Artikli");

            migrationBuilder.DropTable(
                name: "Racuni");

            migrationBuilder.DropTable(
                name: "Radnje");
        }
    }
}
