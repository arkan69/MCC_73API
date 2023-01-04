using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class newmigatrion1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_registers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nik = table.Column<string>(type: "nchar(5)", nullable: false),
                    fullname = table.Column<string>(name: "full_name", type: "nvarchar(25)", maxLength: 25, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false),
                    birthdate = table.Column<DateTime>(name: "birth_date", type: "datetime2", nullable: false),
                    salary = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_registers", x => x.id);
                    table.UniqueConstraint("AK_tb_m_registers_email", x => x.email);
                    table.UniqueConstraint("AK_tb_m_registers_phone", x => x.phone);
                    table.ForeignKey(
                        name: "FK_tb_m_registers_tb_m_accounts_nik",
                        column: x => x.nik,
                        principalTable: "tb_m_accounts",
                        principalColumn: "nik",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_registers_nik",
                table: "tb_m_registers",
                column: "nik",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_m_registers");
        }
    }
}
