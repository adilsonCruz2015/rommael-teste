using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rommanel.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    Document = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime", maxLength: 23, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    ZipCode = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    Address_Street = table.Column<string>(type: "varchar(350)", maxLength: 350, nullable: true),
                    Number = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    Neighborhood = table.Column<string>(type: "varchar(350)", maxLength: 350, nullable: true),
                    City = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    State = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    TaxExempt = table.Column<bool>(type: "bit", maxLength: 20, nullable: false),
                    StateRegistration = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
