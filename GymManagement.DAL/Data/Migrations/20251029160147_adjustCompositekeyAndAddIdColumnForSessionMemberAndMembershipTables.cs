using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagement.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class adjustCompositekeyAndAddIdColumnForSessionMemberAndMembershipTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberShips",
                table: "MemberShips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberSessions",
                table: "MemberSessions");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MemberShips",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MemberSessions",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberShips",
                table: "MemberShips",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberSessions",
                table: "MemberSessions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MemberShips_MemberId_PlanId_StartDate",
                table: "MemberShips",
                columns: new[] { "MemberId", "PlanId", "StartDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MemberSessions_BookingDate_MemberId_SessionId",
                table: "MemberSessions",
                columns: new[] { "BookingDate", "MemberId", "SessionId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberShips",
                table: "MemberShips");

            migrationBuilder.DropIndex(
                name: "IX_MemberShips_MemberId_PlanId_StartDate",
                table: "MemberShips");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberSessions",
                table: "MemberSessions");

            migrationBuilder.DropIndex(
                name: "IX_MemberSessions_BookingDate_MemberId_SessionId",
                table: "MemberSessions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MemberShips");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MemberSessions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberShips",
                table: "MemberShips",
                columns: new[] { "MemberId", "PlanId", "StartDate" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberSessions",
                table: "MemberSessions",
                columns: new[] { "BookingDate", "MemberId", "SessionId" });
        }
    }
}
