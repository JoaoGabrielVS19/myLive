using Microsoft.EntityFrameworkCore.Migrations;

namespace myLive.Migrations
{
    public partial class CriacaoTabelaContatos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EnderecoInstagem",
                table: "Instrutores",
                newName: "EnderecoInstagram");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EnderecoInstagram",
                table: "Instrutores",
                newName: "EnderecoInstagem");
        }
    }
}
