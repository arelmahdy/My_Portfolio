using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adress",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Street = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adress", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioItems",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "NewID()"),
                    ProjectName = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioItems", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Owner",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false, defaultValueSql: "NewID()"),
                    FullName = table.Column<string>(nullable: true),
                    Profile = table.Column<string>(nullable: true),
                    Avatar = table.Column<string>(nullable: true),
                    AdressID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owner", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Owner_Adress_AdressID",
                        column: x => x.AdressID,
                        principalTable: "Adress",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Owner",
                columns: new[] { "ID", "AdressID", "Avatar", "FullName", "Profile" },
                values: new object[] { new Guid("2a4a05df-3b6f-4882-8816-0cc029e8965f"), null, "Avatar.npg", "Ahmed Raafat", "Developer" });

            migrationBuilder.CreateIndex(
                name: "IX_Owner_AdressID",
                table: "Owner",
                column: "AdressID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Owner");

            migrationBuilder.DropTable(
                name: "PortfolioItems");

            migrationBuilder.DropTable(
                name: "Adress");
        }
    }
}
