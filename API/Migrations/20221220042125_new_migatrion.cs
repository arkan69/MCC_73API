using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class newmigatrion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_employees",
                columns: table => new
                {
                    nik = table.Column<string>(type: "nchar(5)", nullable: false),
                    firstname = table.Column<string>(name: "first_name", type: "nvarchar(25)", maxLength: 25, nullable: false),
                    lastname = table.Column<string>(name: "last_name", type: "nvarchar(25)", maxLength: 25, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    birthdate = table.Column<DateTime>(name: "birth_date", type: "datetime2", nullable: false),
                    salary = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_employees", x => x.nik);
                    table.UniqueConstraint("AK_tb_m_employees_email", x => x.email);
                    table.UniqueConstraint("AK_tb_m_employees_phone", x => x.phone);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_university",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_university", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_accounts",
                columns: table => new
                {
                    nik = table.Column<string>(type: "nchar(5)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OTP = table.Column<int>(type: "int", nullable: false),
                    ExpiredToken = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_accounts", x => x.nik);
                    table.ForeignKey(
                        name: "FK_tb_m_accounts_tb_m_employees_nik",
                        column: x => x.nik,
                        principalTable: "tb_m_employees",
                        principalColumn: "nik",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_education",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    degree = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    gpa = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    universityid = table.Column<int>(name: "university_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_education", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_m_education_tb_m_university_university_id",
                        column: x => x.universityid,
                        principalTable: "tb_m_university",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_r_accounts_roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    accountnik = table.Column<string>(name: "account_nik", type: "nchar(5)", nullable: false),
                    roleid = table.Column<int>(name: "role_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_r_accounts_roles", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_r_accounts_roles_tb_m_accounts_account_nik",
                        column: x => x.accountnik,
                        principalTable: "tb_m_accounts",
                        principalColumn: "nik",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_r_accounts_roles_tb_m_roles_role_id",
                        column: x => x.roleid,
                        principalTable: "tb_m_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_r_profile",
                columns: table => new
                {
                    nik = table.Column<string>(type: "nchar(5)", nullable: false),
                    educationid = table.Column<int>(name: "education_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_r_profile", x => x.nik);
                    table.ForeignKey(
                        name: "FK_tb_r_profile_tb_m_accounts_nik",
                        column: x => x.nik,
                        principalTable: "tb_m_accounts",
                        principalColumn: "nik",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_r_profile_tb_m_education_education_id",
                        column: x => x.educationid,
                        principalTable: "tb_m_education",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_accounts_nik",
                table: "tb_m_accounts",
                column: "nik",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_education_university_id",
                table: "tb_m_education",
                column: "university_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_r_accounts_roles_account_nik",
                table: "tb_r_accounts_roles",
                column: "account_nik");

            migrationBuilder.CreateIndex(
                name: "IX_tb_r_accounts_roles_role_id",
                table: "tb_r_accounts_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_r_profile_education_id",
                table: "tb_r_profile",
                column: "education_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_r_accounts_roles");

            migrationBuilder.DropTable(
                name: "tb_r_profile");

            migrationBuilder.DropTable(
                name: "tb_m_roles");

            migrationBuilder.DropTable(
                name: "tb_m_accounts");

            migrationBuilder.DropTable(
                name: "tb_m_education");

            migrationBuilder.DropTable(
                name: "tb_m_employees");

            migrationBuilder.DropTable(
                name: "tb_m_university");
        }
    }
}
