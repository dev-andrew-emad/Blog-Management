using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blogdatalayer.Migrations
{
    /// <inheritdoc />
    public partial class addreply : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "replies",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userid = table.Column<int>(type: "int", nullable: false),
                    content = table.Column<string>(type: "varchar(300)", nullable: false),
                    commentid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_replies", x => x.id);
                    table.ForeignKey(
                        name: "FK_replies_comments_commentid",
                        column: x => x.commentid,
                        principalTable: "comments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_replies_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "replylikes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userid = table.Column<int>(type: "int", nullable: false),
                    replyid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_replylikes", x => x.id);
                    table.ForeignKey(
                        name: "FK_replylikes_replies_replyid",
                        column: x => x.replyid,
                        principalTable: "replies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_replylikes_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_replies_commentid",
                table: "replies",
                column: "commentid");

            migrationBuilder.CreateIndex(
                name: "IX_replies_userid",
                table: "replies",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_replylikes_replyid",
                table: "replylikes",
                column: "replyid");

            migrationBuilder.CreateIndex(
                name: "IX_replylikes_userid",
                table: "replylikes",
                column: "userid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "replylikes");

            migrationBuilder.DropTable(
                name: "replies");
        }
    }
}
