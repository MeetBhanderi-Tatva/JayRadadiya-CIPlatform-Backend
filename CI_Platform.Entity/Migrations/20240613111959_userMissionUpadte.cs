using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CI_Platform.Entity.Migrations
{
    /// <inheritdoc />
    public partial class userMissionUpadte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserMissions",
                table: "UserMissions");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "UserMissions",
                type: "bigint",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldMaxLength: 20)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "UserMissions",
                type: "bigint",
                maxLength: 20,
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserMissions",
                table: "UserMissions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserMissions_UserId",
                table: "UserMissions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMissions_User_UserId",
                table: "UserMissions",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMissions_User_UserId",
                table: "UserMissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserMissions",
                table: "UserMissions");

            migrationBuilder.DropIndex(
                name: "IX_UserMissions_UserId",
                table: "UserMissions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserMissions");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "UserMissions",
                type: "bigint",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldMaxLength: 20)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserMissions",
                table: "UserMissions",
                column: "UserId");
        }
    }
}
