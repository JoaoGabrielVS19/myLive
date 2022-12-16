using Microsoft.EntityFrameworkCore.Migrations;

namespace myLive.Migrations
{
    public partial class InserirColunaCanceladoInstrutor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Excluido",
                table: "Instrutores",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Excluido",
                table: "Instrutores");
        }
    }
}
