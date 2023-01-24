using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnWords.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlockOfWords",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Version = table.Column<byte[]>(type: "timestamp", maxLength: 8, rowVersion: true, nullable: false),
                    RepeatCount = table.Column<short>(type: "smallint", nullable: false),
                    NextRepeat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockOfWords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Version = table.Column<byte[]>(type: "timestamp", maxLength: 8, rowVersion: true, nullable: false),
                    Russian = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    English = table.Column<string>(type: "nvarchar(350)", maxLength: 350, nullable: false),
                    IsNew = table.Column<bool>(type: "bit", nullable: false),
                    Learned = table.Column<bool>(type: "bit", nullable: false),
                    PartOfSpeech = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WordBlocks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Version = table.Column<byte[]>(type: "timestamp", maxLength: 8, rowVersion: true, nullable: false),
                    BlockId = table.Column<long>(type: "bigint", nullable: false),
                    WordId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordBlocks_BlockOfWords_BlockId",
                        column: x => x.BlockId,
                        principalTable: "BlockOfWords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WordBlocks_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WordBlocks_BlockId",
                table: "WordBlocks",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_WordBlocks_WordId",
                table: "WordBlocks",
                column: "WordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WordBlocks");

            migrationBuilder.DropTable(
                name: "BlockOfWords");

            migrationBuilder.DropTable(
                name: "Words");
        }
    }
}
