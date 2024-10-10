using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Topic.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewsletterLinkCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LinksCount",
                table: "Newsletter",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinksCount",
                table: "Newsletter");
        }
    }
}
