using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalWebsite.Service.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_T_IdNames",
                table: "T_IdNames");

            migrationBuilder.RenameTable(
                name: "T_IdNames",
                newName: "T_KeyValues");

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_KeyValues",
                table: "T_KeyValues",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "T_AdminUserRoles",
                columns: table => new
                {
                    AdminUserId = table.Column<long>(nullable: false),
                    RoleId = table.Column<long>(nullable: false),
                    Id = table.Column<long>(nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_AdminUserRoles", x => new { x.AdminUserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_T_AdminUserRoles_T_AdminUsers_AdminUserId",
                        column: x => x.AdminUserId,
                        principalTable: "T_AdminUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_AdminUserRoles_T_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "T_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "T_RolePermissions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedDateTime = table.Column<DateTime>(nullable: false),
                    RoleId = table.Column<long>(nullable: false),
                    PermissionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_RolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_RolePermissions_T_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "T_Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_T_RolePermissions_T_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "T_Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_AdminUserRoles_RoleId",
                table: "T_AdminUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_T_RolePermissions_PermissionId",
                table: "T_RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_T_RolePermissions_RoleId",
                table: "T_RolePermissions",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_AdminUserRoles");

            migrationBuilder.DropTable(
                name: "T_RolePermissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_T_KeyValues",
                table: "T_KeyValues");

            migrationBuilder.RenameTable(
                name: "T_KeyValues",
                newName: "T_IdNames");

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_IdNames",
                table: "T_IdNames",
                column: "Id");
        }
    }
}
