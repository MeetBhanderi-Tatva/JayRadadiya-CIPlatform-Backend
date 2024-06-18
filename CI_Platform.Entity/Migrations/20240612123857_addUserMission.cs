using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CI_Platform.Entity.Migrations
{
    /// <inheritdoc />
    public partial class addUserMission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MissionSkills",
                table: "Mission");

            migrationBuilder.AlterColumn<int>(
                name: "TotalSeats",
                table: "Mission",
                type: "integer",
                maxLength: 10,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AchievedGoal",
                table: "Mission",
                type: "integer",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OccupiedSeats",
                table: "Mission",
                type: "integer",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalGoal",
                table: "Mission",
                type: "integer",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MissionSkills",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", maxLength: 20, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SkillId = table.Column<int>(type: "integer", maxLength: 10, nullable: false),
                    MissionId = table.Column<long>(type: "bigint", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissionSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MissionSkills_Mission_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Mission",
                        principalColumn: "MissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MissionSkills_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MissionSkills_MissionId",
                table: "MissionSkills",
                column: "MissionId");

            migrationBuilder.CreateIndex(
                name: "IX_MissionSkills_SkillId",
                table: "MissionSkills",
                column: "SkillId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MissionSkills");

            migrationBuilder.DropColumn(
                name: "AchievedGoal",
                table: "Mission");

            migrationBuilder.DropColumn(
                name: "OccupiedSeats",
                table: "Mission");

            migrationBuilder.DropColumn(
                name: "TotalGoal",
                table: "Mission");

            migrationBuilder.AlterColumn<int>(
                name: "TotalSeats",
                table: "Mission",
                type: "integer",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldMaxLength: 10);

            migrationBuilder.AddColumn<string>(
                name: "MissionSkills",
                table: "Mission",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
