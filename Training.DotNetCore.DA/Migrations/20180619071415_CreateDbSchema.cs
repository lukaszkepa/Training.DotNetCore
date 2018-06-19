using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Training.DotNetCore.DA.Migrations
{
    public partial class CreateDbSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attendees",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trainer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trainings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TrainerId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainings_Trainer_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingAttendee",
                columns: table => new
                {
                    TrainingId = table.Column<int>(nullable: false),
                    AttendeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingAttendee", x => new { x.TrainingId, x.AttendeeId });
                    table.ForeignKey(
                        name: "FK_TrainingAttendee_Attendees_AttendeeId",
                        column: x => x.AttendeeId,
                        principalTable: "Attendees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingAttendee_Trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Attendees",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Test attendee" },
                    { 2, "Test attendee 2" }
                });

            migrationBuilder.InsertData(
                table: "Trainer",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Test trainer" },
                    { 2, "Test trainer 2" }
                });

            migrationBuilder.InsertData(
                table: "Trainings",
                columns: new[] { "Id", "Description", "EndDate", "Name", "StartDate", "TrainerId" },
                values: new object[] { 1, "Description", new DateTime(2018, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test training", new DateTime(2018, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.InsertData(
                table: "Trainings",
                columns: new[] { "Id", "Description", "EndDate", "Name", "StartDate", "TrainerId" },
                values: new object[] { 2, "Description", new DateTime(2018, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test training 2", new DateTime(2018, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.InsertData(
                table: "TrainingAttendee",
                columns: new[] { "TrainingId", "AttendeeId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "TrainingAttendee",
                columns: new[] { "TrainingId", "AttendeeId" },
                values: new object[] { 1, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingAttendee_AttendeeId",
                table: "TrainingAttendee",
                column: "AttendeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_TrainerId",
                table: "Trainings",
                column: "TrainerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainingAttendee");

            migrationBuilder.DropTable(
                name: "Attendees");

            migrationBuilder.DropTable(
                name: "Trainings");

            migrationBuilder.DropTable(
                name: "Trainer");
        }
    }
}
