using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TheraGuide.Migrations
{
    /// <inheritdoc />
    public partial class seeddatatip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tips",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tips_Categorys_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Tips",
                columns: new[] { "Id", "CategoryId", "Content", "Type" },
                values: new object[,]
                {
                    { 1L, 4L, "Set screen time limits: Create specific limits for how much time you spend on devices daily.", 1 },
                    { 2L, 4L, "Schedule 'device-free' times: Dedicate specific periods in your day to disconnect completely from screens.", 1 },
                    { 3L, 4L, "Turn off non-essential notifications: Reduce interruptions that take your attention away from tasks.", 1 },
                    { 4L, 4L, "Mindful Disconnect: Engage in an activity without screens (e.g., reading, walking).", 2 },
                    { 5L, 4L, "Digital-Free Zones: Create spaces where devices are not allowed.", 2 },
                    { 6L, 5L, "Reach out to someone you trust: Share your feelings with a friend or therapist.", 1 },
                    { 7L, 5L, "Join a social group: Find communities with shared interests to build connections.", 1 },
                    { 8L, 5L, "Emotional Expression: Write about your feelings in a journal.", 2 },
                    { 9L, 5L, "Connect with a Friend: Schedule a call or meetup to talk.", 2 },
                    { 10L, 6L, "Set small, achievable goals: Break tasks into smaller steps.", 1 },
                    { 11L, 6L, "Celebrate small wins: Reward yourself for completing tasks.", 1 },
                    { 12L, 6L, "Morning Stretch: Start your day with gentle stretches.", 2 },
                    { 13L, 6L, "Physical Activity: Walk or do yoga for 15 minutes.", 2 },
                    { 14L, 3L, "Limit screen time before bed: Avoid devices 30 minutes before sleep.", 1 },
                    { 15L, 3L, "Breathing Exercises: Practice deep breathing to calm your mind.", 2 },
                    { 16L, 2L, "Practice mindfulness: Meditate to stay present.", 1 },
                    { 17L, 2L, "Guided Meditation: Listen to a calming meditation.", 2 },
                    { 18L, 1L, "Focus on breathing: Use deep breaths to reduce anxiety.", 1 },
                    { 19L, 1L, "Body Scan: Release tension by focusing on each body part.", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tips_CategoryId",
                table: "Tips",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tips");
        }
    }
}
