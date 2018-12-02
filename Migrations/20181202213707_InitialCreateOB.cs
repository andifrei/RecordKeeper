using Microsoft.EntityFrameworkCore.Migrations;

namespace RecordKeeper.Migrations
{
    public partial class InitialCreateOB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "RecordItem",
                nullable: false,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "RecordItem",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
