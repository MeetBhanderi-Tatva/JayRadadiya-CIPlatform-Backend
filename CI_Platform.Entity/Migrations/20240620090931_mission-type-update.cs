using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CI_Platform.Entity.Migrations
{
    /// <inheritdoc />
    public partial class missiontypeupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MissionType_Mission_MissionId",
                table: "MissionType");

            migrationBuilder.DropIndex(
                name: "IX_MissionType_MissionId",
                table: "MissionType");

            migrationBuilder.DropColumn(
                name: "MissionId",
                table: "MissionType");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "MissionType",
                type: "integer",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldMaxLength: 20)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_Mission_MissionType",
                table: "Mission",
                column: "MissionType");

            migrationBuilder.AddForeignKey(
                name: "FK_Mission_MissionType_MissionType",
                table: "Mission",
                column: "MissionType",
                principalTable: "MissionType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mission_MissionType_MissionType",
                table: "Mission");

            migrationBuilder.DropIndex(
                name: "IX_Mission_MissionType",
                table: "Mission");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "MissionType",
                type: "bigint",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldMaxLength: 10)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<long>(
                name: "MissionId",
                table: "MissionType",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_MissionType_MissionId",
                table: "MissionType",
                column: "MissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MissionType_Mission_MissionId",
                table: "MissionType",
                column: "MissionId",
                principalTable: "Mission",
                principalColumn: "MissionId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
