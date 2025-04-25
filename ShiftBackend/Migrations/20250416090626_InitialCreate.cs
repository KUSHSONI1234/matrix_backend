using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShiftBackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EndTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttendanceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HalfDay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullDay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinHoursRequired = table.Column<bool>(type: "bit", nullable: true),
                    DeviationStart = table.Column<int>(type: "int", nullable: true),
                    DeviationEnd = table.Column<int>(type: "int", nullable: true),
                    ShiftAllowance = table.Column<bool>(type: "bit", nullable: true),
                    BreakStart = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BreakEnd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BreakDuration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviationAllowed = table.Column<bool>(type: "bit", nullable: false),
                    AddLateIn = table.Column<bool>(type: "bit", nullable: false),
                    AddEarlyOut = table.Column<bool>(type: "bit", nullable: false),
                    Deduct2Punch = table.Column<bool>(type: "bit", nullable: false),
                    Deduct2PunchType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deduct2PlusPunch = table.Column<bool>(type: "bit", nullable: false),
                    Deduct2PlusPunchType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncludeGraceTime = table.Column<bool>(type: "bit", nullable: false),
                    ShiftLateIn = table.Column<int>(type: "int", nullable: true),
                    ShiftLateInOverlap = table.Column<bool>(type: "bit", nullable: false),
                    ShiftEarlyOut = table.Column<int>(type: "int", nullable: true),
                    ShiftEarlyOutOverlap = table.Column<bool>(type: "bit", nullable: false),
                    BreakLateIn = table.Column<int>(type: "int", nullable: true),
                    BreakLateInOverlap = table.Column<bool>(type: "bit", nullable: false),
                    BreakEarlyOut = table.Column<int>(type: "int", nullable: true),
                    BreakEarlyOutOverlap = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shifts");
        }
    }
}
