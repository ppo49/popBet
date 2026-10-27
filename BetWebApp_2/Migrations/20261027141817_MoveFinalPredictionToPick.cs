using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetWebApp_2.Migrations
{
    /// <inheritdoc />
    public partial class MoveFinalPredictionToPick : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalPrediction",
                table: "MyPicks");

            migrationBuilder.AddColumn<string>(
                name: "FinalPrediction",
                table: "Picks",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "MyPrediction",
                table: "MyPicks",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalPrediction",
                table: "Picks");

            migrationBuilder.AlterColumn<int>(
                name: "MyPrediction",
                table: "MyPicks",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "FinalPrediction",
                table: "MyPicks",
                type: "INTEGER",
                nullable: true);
        }
    }
}
