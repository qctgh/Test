using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalWebsite.Service.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_T_AdminUserRoles",
                table: "T_AdminUserRoles");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "T_AdminUserRoles",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_AdminUserRoles",
                table: "T_AdminUserRoles",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_T_AdminUserRoles_AdminUserId",
                table: "T_AdminUserRoles",
                column: "AdminUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_T_AdminUserRoles",
                table: "T_AdminUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_T_AdminUserRoles_AdminUserId",
                table: "T_AdminUserRoles");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "T_AdminUserRoles",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_AdminUserRoles",
                table: "T_AdminUserRoles",
                columns: new[] { "AdminUserId", "RoleId" });
        }
    }
}
