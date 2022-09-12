using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiscussionNet.Persistence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(500)", maxLength: 500, nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(500)", maxLength: 500, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TimeOffset = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    TimeOffsetHour = table.Column<byte>(type: "tinyint", maxLength: 255, nullable: false),
                    Signature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthorStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    Roles = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsEmailVerified = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Threads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TopicId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    ParentThreadId = table.Column<int>(type: "int", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(39)", maxLength: 39, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Threads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Threads_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Threads_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserEmailVerifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsValid = table.Column<bool>(type: "bit", nullable: false),
                    ValidationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEmailVerifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserEmailVerifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ThreadReactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ThreadId = table.Column<int>(type: "int", nullable: true),
                    ReactedById = table.Column<int>(type: "int", nullable: true),
                    Reaction = table.Column<byte>(type: "tinyint", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreadReactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThreadReactions_Threads_ThreadId",
                        column: x => x.ThreadId,
                        principalTable: "Threads",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ThreadReactions_Users_ReactedById",
                        column: x => x.ReactedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThreadReactions_ReactedById",
                table: "ThreadReactions",
                column: "ReactedById");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadReactions_ThreadId",
                table: "ThreadReactions",
                column: "ThreadId");

            migrationBuilder.CreateIndex(
                name: "IX_Threads_TopicId",
                table: "Threads",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Threads_UserId",
                table: "Threads",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEmailVerifications_UserId",
                table: "UserEmailVerifications",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThreadReactions");

            migrationBuilder.DropTable(
                name: "UserEmailVerifications");

            migrationBuilder.DropTable(
                name: "Threads");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
