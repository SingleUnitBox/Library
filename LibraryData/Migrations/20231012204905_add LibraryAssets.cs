using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryData.Migrations
{
    public partial class addLibraryAssets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckoutHistories_LibraryAsset_LibraryAssetId",
                table: "CheckoutHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Checkouts_LibraryAsset_LibraryAssetId",
                table: "Checkouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Holds_LibraryAsset_LibraryAssetId",
                table: "Holds");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryAsset_LibraryBranches_LocationId",
                table: "LibraryAsset");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryAsset_Statuses_StatusId",
                table: "LibraryAsset");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LibraryAsset",
                table: "LibraryAsset");

            migrationBuilder.RenameTable(
                name: "LibraryAsset",
                newName: "LibraryAssets");

            migrationBuilder.RenameIndex(
                name: "IX_LibraryAsset_StatusId",
                table: "LibraryAssets",
                newName: "IX_LibraryAssets_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_LibraryAsset_LocationId",
                table: "LibraryAssets",
                newName: "IX_LibraryAssets_LocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LibraryAssets",
                table: "LibraryAssets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckoutHistories_LibraryAssets_LibraryAssetId",
                table: "CheckoutHistories",
                column: "LibraryAssetId",
                principalTable: "LibraryAssets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Checkouts_LibraryAssets_LibraryAssetId",
                table: "Checkouts",
                column: "LibraryAssetId",
                principalTable: "LibraryAssets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Holds_LibraryAssets_LibraryAssetId",
                table: "Holds",
                column: "LibraryAssetId",
                principalTable: "LibraryAssets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryAssets_LibraryBranches_LocationId",
                table: "LibraryAssets",
                column: "LocationId",
                principalTable: "LibraryBranches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryAssets_Statuses_StatusId",
                table: "LibraryAssets",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckoutHistories_LibraryAssets_LibraryAssetId",
                table: "CheckoutHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Checkouts_LibraryAssets_LibraryAssetId",
                table: "Checkouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Holds_LibraryAssets_LibraryAssetId",
                table: "Holds");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryAssets_LibraryBranches_LocationId",
                table: "LibraryAssets");

            migrationBuilder.DropForeignKey(
                name: "FK_LibraryAssets_Statuses_StatusId",
                table: "LibraryAssets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LibraryAssets",
                table: "LibraryAssets");

            migrationBuilder.RenameTable(
                name: "LibraryAssets",
                newName: "LibraryAsset");

            migrationBuilder.RenameIndex(
                name: "IX_LibraryAssets_StatusId",
                table: "LibraryAsset",
                newName: "IX_LibraryAsset_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_LibraryAssets_LocationId",
                table: "LibraryAsset",
                newName: "IX_LibraryAsset_LocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LibraryAsset",
                table: "LibraryAsset",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckoutHistories_LibraryAsset_LibraryAssetId",
                table: "CheckoutHistories",
                column: "LibraryAssetId",
                principalTable: "LibraryAsset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Checkouts_LibraryAsset_LibraryAssetId",
                table: "Checkouts",
                column: "LibraryAssetId",
                principalTable: "LibraryAsset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Holds_LibraryAsset_LibraryAssetId",
                table: "Holds",
                column: "LibraryAssetId",
                principalTable: "LibraryAsset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryAsset_LibraryBranches_LocationId",
                table: "LibraryAsset",
                column: "LocationId",
                principalTable: "LibraryBranches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LibraryAsset_Statuses_StatusId",
                table: "LibraryAsset",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
