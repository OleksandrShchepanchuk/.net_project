using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital.Migrations
{
    /// <inheritdoc />
    public partial class DoctorReviewRelationAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Reviews",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_DoctorId",
                table: "Reviews",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Doctors_DoctorId",
                table: "Reviews",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Doctors_DoctorId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_DoctorId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Reviews");
        }
    }
}
