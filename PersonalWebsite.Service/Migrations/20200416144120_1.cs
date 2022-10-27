using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonalWebsite.Service.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_Articles_ChannelEntities_ChannelId",
                table: "T_Articles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChannelEntities",
                table: "ChannelEntities");

            migrationBuilder.RenameTable(
                name: "ChannelEntities",
                newName: "T_Channels");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "T_Channels",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_T_Channels",
                table: "T_Channels",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_T_Articles_T_Channels_ChannelId",
                table: "T_Articles",
                column: "ChannelId",
                principalTable: "T_Channels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_T_Articles_T_Channels_ChannelId",
                table: "T_Articles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_T_Channels",
                table: "T_Channels");

            migrationBuilder.RenameTable(
                name: "T_Channels",
                newName: "ChannelEntities");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ChannelEntities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChannelEntities",
                table: "ChannelEntities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_T_Articles_ChannelEntities_ChannelId",
                table: "T_Articles",
                column: "ChannelId",
                principalTable: "ChannelEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
