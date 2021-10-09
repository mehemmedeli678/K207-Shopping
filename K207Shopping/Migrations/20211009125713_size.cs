using Microsoft.EntityFrameworkCore.Migrations;

namespace K207Shopping.Migrations
{
    public partial class size : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Size",
                table: "ProductSize",
                newName: "SizeID");

            migrationBuilder.RenameColumn(
                name: "Color",
                table: "ProductColor",
                newName: "ColorID");

            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SizeName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Size", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSize_SizeID",
                table: "ProductSize",
                column: "SizeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColor_ColorID",
                table: "ProductColor",
                column: "ColorID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductColor_Color_ColorID",
                table: "ProductColor",
                column: "ColorID",
                principalTable: "Color",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSize_Size_SizeID",
                table: "ProductSize",
                column: "SizeID",
                principalTable: "Size",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductColor_Color_ColorID",
                table: "ProductColor");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSize_Size_SizeID",
                table: "ProductSize");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "Size");

            migrationBuilder.DropIndex(
                name: "IX_ProductSize_SizeID",
                table: "ProductSize");

            migrationBuilder.DropIndex(
                name: "IX_ProductColor_ColorID",
                table: "ProductColor");

            migrationBuilder.RenameColumn(
                name: "SizeID",
                table: "ProductSize",
                newName: "Size");

            migrationBuilder.RenameColumn(
                name: "ColorID",
                table: "ProductColor",
                newName: "Color");
        }
    }
}
