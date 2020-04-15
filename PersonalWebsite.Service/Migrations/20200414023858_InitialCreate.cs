using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalWebsite.Service.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChannelEntities",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedDateTime = table.Column<DateTime>(nullable: false),
                    ParentId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserEntities",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedDateTime = table.Column<DateTime>(nullable: false),
                    PhoneNum = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PasswordSalt = table.Column<string>(nullable: true),
                    LoginErrorTimes = table.Column<int>(nullable: false),
                    LastLoginErrorDateTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleEntities",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedDateTime = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    PostDate = table.Column<DateTime>(nullable: false),
                    Introduce = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    StaticPath = table.Column<string>(nullable: true),
                    SupportCount = table.Column<int>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    IsVisible = table.Column<bool>(nullable: false),
                    IsFirst = table.Column<bool>(nullable: false),
                    ChannelId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArticleEntities_ChannelEntities_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "ChannelEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleEntities_UserEntities_UserId",
                        column: x => x.UserId,
                        principalTable: "UserEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleEntities_ChannelId",
                table: "ArticleEntities",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleEntities_UserId",
                table: "ArticleEntities",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleEntities");

            migrationBuilder.DropTable(
                name: "ChannelEntities");

            migrationBuilder.DropTable(
                name: "UserEntities");
        }
    }
}
