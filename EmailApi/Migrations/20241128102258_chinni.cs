using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmailApi.Migrations
{
    /// <inheritdoc />
    public partial class chinni : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Details3312",
                columns: table => new
                {
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Pwd = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Details3312", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "File11",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileContent = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File11", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sathii331",
                columns: table => new
                {
                    userid = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sathii331", x => x.userid);
                });

            migrationBuilder.CreateTable(
                name: "uploadedFile13",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_uploadedFile13", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user112",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEmailVerified = table.Column<bool>(type: "bit", nullable: true),
                    OTP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OTPExpiry = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user112", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User118",
                columns: table => new
                {
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    age = table.Column<int>(type: "int", nullable: false),
                    MobileNo = table.Column<long>(type: "bigint", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    otp = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User118", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin111",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin111", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User118_Email",
                table: "User118",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User118_MobileNo",
                table: "User118",
                column: "MobileNo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Details3312");

            migrationBuilder.DropTable(
                name: "File11");

            migrationBuilder.DropTable(
                name: "sathii331");

            migrationBuilder.DropTable(
                name: "uploadedFile13");

            migrationBuilder.DropTable(
                name: "user112");

            migrationBuilder.DropTable(
                name: "User118");

            migrationBuilder.DropTable(
                name: "UserLogin111");
        }
    }
}
