using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Topic.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Newsletter",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Keywords = table.Column<string[]>(type: "text[]", nullable: false),
                    Status = table.Column<byte>(type: "smallint", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Newsletter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsletterLink",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Url = table.Column<string>(type: "character varying(2083)", maxLength: 2083, nullable: false),
                    NewsletterId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsletterLink", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewsletterLink_Newsletter_NewsletterId",
                        column: x => x.NewsletterId,
                        principalTable: "Newsletter",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewsletterLink_NewsletterId",
                table: "NewsletterLink",
                column: "NewsletterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewsletterLink");

            migrationBuilder.DropTable(
                name: "Newsletter");
        }
    }
}
