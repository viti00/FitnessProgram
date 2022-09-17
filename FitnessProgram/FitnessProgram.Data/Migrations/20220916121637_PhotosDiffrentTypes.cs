namespace FitnessProgram.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class PhotosDiffrentTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Photos_ProfilePictureId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_userLikedPosts_Posts_PostId",
                table: "userLikedPosts");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userLikedPosts",
                table: "userLikedPosts");

            migrationBuilder.RenameTable(
                name: "userLikedPosts",
                newName: "UserLikedPosts");

            migrationBuilder.RenameIndex(
                name: "IX_userLikedPosts_PostId",
                table: "UserLikedPosts",
                newName: "IX_UserLikedPosts_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLikedPosts",
                table: "UserLikedPosts",
                columns: new[] { "UserId", "PostId" });

            migrationBuilder.CreateTable(
                name: "BestResultPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BestResultId = table.Column<int>(type: "int", nullable: false),
                    PhotoType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BestResultPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BestResultPhotos_BestResults_BestResultId",
                        column: x => x.BestResultId,
                        principalTable: "BestResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartnerPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartnerId = table.Column<int>(type: "int", nullable: false),
                    Bytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartnerPhotos_Partners_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Bytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostPhotos_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfilePhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Bytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfilePhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProfilePhotos_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BestResultPhotos_BestResultId",
                table: "BestResultPhotos",
                column: "BestResultId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerPhotos_PartnerId",
                table: "PartnerPhotos",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PostPhotos_PostId",
                table: "PostPhotos",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfilePhotos_UserId",
                table: "ProfilePhotos",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ProfilePhotos_ProfilePictureId",
                table: "AspNetUsers",
                column: "ProfilePictureId",
                principalTable: "ProfilePhotos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikedPosts_Posts_PostId",
                table: "UserLikedPosts",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ProfilePhotos_ProfilePictureId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLikedPosts_Posts_PostId",
                table: "UserLikedPosts");

            migrationBuilder.DropTable(
                name: "BestResultPhotos");

            migrationBuilder.DropTable(
                name: "PartnerPhotos");

            migrationBuilder.DropTable(
                name: "PostPhotos");

            migrationBuilder.DropTable(
                name: "ProfilePhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLikedPosts",
                table: "UserLikedPosts");

            migrationBuilder.RenameTable(
                name: "UserLikedPosts",
                newName: "userLikedPosts");

            migrationBuilder.RenameIndex(
                name: "IX_UserLikedPosts_PostId",
                table: "userLikedPosts",
                newName: "IX_userLikedPosts_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userLikedPosts",
                table: "userLikedPosts",
                columns: new[] { "UserId", "PostId" });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bytes = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Size = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_PostId",
                table: "Photos",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Photos_ProfilePictureId",
                table: "AspNetUsers",
                column: "ProfilePictureId",
                principalTable: "Photos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_userLikedPosts_Posts_PostId",
                table: "userLikedPosts",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
