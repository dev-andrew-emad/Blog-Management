using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blogdatalayer.Migrations
{
    /// <inheritdoc />
    public partial class addcommentlike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "commentlikes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userid = table.Column<int>(type: "int", nullable: false),
                    commentid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_commentlikes", x => x.id);
                    table.ForeignKey(
                        name: "FK_commentlikes_comments_commentid",
                        column: x => x.commentid,
                        principalTable: "comments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_commentlikes_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_commentlikes_commentid",
                table: "commentlikes",
                column: "commentid");

            migrationBuilder.CreateIndex(
                name: "IX_commentlikes_userid",
                table: "commentlikes",
                column: "userid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "commentlikes");
        }
    }
}
