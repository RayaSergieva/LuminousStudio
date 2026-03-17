using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LuminousStudio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentsToEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "TiffanyLamps",
                comment: "Stores the main Tiffany lamp products offered in the application.");

            migrationBuilder.AlterTable(
                name: "ShoppingCarts",
                comment: "Stores shopping cart items for users before they place orders.");

            migrationBuilder.AlterTable(
                name: "Orders",
                comment: "Stores customer orders for Tiffany lamps.");

            migrationBuilder.AlterTable(
                name: "Manufacturers",
                comment: "Stores the manufacturers or designers associated with Tiffany lamps.");

            migrationBuilder.AlterTable(
                name: "LampStyles",
                comment: "Stores the available lamp style categories.");

            migrationBuilder.AlterTable(
                name: "AspNetUsers",
                comment: "Stores application users, including administrators and clients.");

            migrationBuilder.AlterColumn<string>(
                name: "TiffanyLampName",
                table: "TiffanyLamps",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                comment: "The display name of the Tiffany lamp.",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "TiffanyLamps",
                type: "int",
                nullable: false,
                comment: "The available quantity in stock.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "TiffanyLamps",
                type: "decimal(18,2)",
                nullable: false,
                comment: "The standard price of the Tiffany lamp.",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Picture",
                table: "TiffanyLamps",
                type: "nvarchar(max)",
                nullable: false,
                comment: "URL or path to the image of the Tiffany lamp.",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "ManufacturerId",
                table: "TiffanyLamps",
                type: "int",
                nullable: false,
                comment: "Foreign key to the manufacturer of the Tiffany lamp.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "LampStyleId",
                table: "TiffanyLamps",
                type: "int",
                nullable: false,
                comment: "Foreign key to the lamp style of the Tiffany lamp.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "TiffanyLamps",
                type: "decimal(18,2)",
                nullable: false,
                comment: "The discount percentage applied to the Tiffany lamp.",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "TiffanyLamps",
                type: "int",
                nullable: false,
                comment: "Primary key of the Tiffany lamp.",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TiffanyLampId",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                comment: "Foreign key to the selected Tiffany lamp.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "ShoppingCarts",
                type: "decimal(18,2)",
                nullable: false,
                comment: "The current effective unit price of the lamp in the shopping cart.",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "Count",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                comment: "The quantity of the selected Tiffany lamp in the shopping cart.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "ShoppingCarts",
                type: "nvarchar(450)",
                nullable: false,
                comment: "Foreign key to the user who owns the shopping cart item.",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                comment: "Primary key of the shopping cart item.",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: false,
                comment: "Foreign key to the user who placed the order.",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "TiffanyLampId",
                table: "Orders",
                type: "int",
                nullable: false,
                comment: "Foreign key to the ordered Tiffany lamp.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Orders",
                type: "int",
                nullable: false,
                comment: "The quantity of lamps included in the order.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                comment: "The unit price of the lamp at the time of the order.",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                comment: "The date and time when the order was created.",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                comment: "The discount percentage applied to the order.",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Orders",
                type: "int",
                nullable: false,
                comment: "Primary key of the order.",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "ManufacturerName",
                table: "Manufacturers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                comment: "The full name of the manufacturer or designer.",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Manufacturers",
                type: "int",
                nullable: false,
                comment: "Primary key of the manufacturer.",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "LampStyleName",
                table: "LampStyles",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                comment: "The name of the lamp style category.",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "LampStyles",
                type: "int",
                nullable: false,
                comment: "Primary key of the lamp style.",
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                comment: "The last name of the user.",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                comment: "The first name of the user.",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "The delivery or contact address of the user.",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterTable(
                name: "TiffanyLamps",
                oldComment: "Stores the main Tiffany lamp products offered in the application.");

            migrationBuilder.AlterTable(
                name: "ShoppingCarts",
                oldComment: "Stores shopping cart items for users before they place orders.");

            migrationBuilder.AlterTable(
                name: "Orders",
                oldComment: "Stores customer orders for Tiffany lamps.");

            migrationBuilder.AlterTable(
                name: "Manufacturers",
                oldComment: "Stores the manufacturers or designers associated with Tiffany lamps.");

            migrationBuilder.AlterTable(
                name: "LampStyles",
                oldComment: "Stores the available lamp style categories.");

            migrationBuilder.AlterTable(
                name: "AspNetUsers",
                oldComment: "Stores application users, including administrators and clients.");

            migrationBuilder.AlterColumn<string>(
                name: "TiffanyLampName",
                table: "TiffanyLamps",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldComment: "The display name of the Tiffany lamp.");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "TiffanyLamps",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The available quantity in stock.");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "TiffanyLamps",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "The standard price of the Tiffany lamp.");

            migrationBuilder.AlterColumn<string>(
                name: "Picture",
                table: "TiffanyLamps",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "URL or path to the image of the Tiffany lamp.");

            migrationBuilder.AlterColumn<int>(
                name: "ManufacturerId",
                table: "TiffanyLamps",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Foreign key to the manufacturer of the Tiffany lamp.");

            migrationBuilder.AlterColumn<int>(
                name: "LampStyleId",
                table: "TiffanyLamps",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Foreign key to the lamp style of the Tiffany lamp.");

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "TiffanyLamps",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "The discount percentage applied to the Tiffany lamp.");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "TiffanyLamps",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Primary key of the Tiffany lamp.")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "TiffanyLampId",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Foreign key to the selected Tiffany lamp.");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "ShoppingCarts",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "The current effective unit price of the lamp in the shopping cart.");

            migrationBuilder.AlterColumn<int>(
                name: "Count",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The quantity of the selected Tiffany lamp in the shopping cart.");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "ShoppingCarts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldComment: "Foreign key to the user who owns the shopping cart item.");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Primary key of the shopping cart item.")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldComment: "Foreign key to the user who placed the order.");

            migrationBuilder.AlterColumn<int>(
                name: "TiffanyLampId",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Foreign key to the ordered Tiffany lamp.");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "The quantity of lamps included in the order.");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "The unit price of the lamp at the time of the order.");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "The date and time when the order was created.");

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComment: "The discount percentage applied to the order.");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Primary key of the order.")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "ManufacturerName",
                table: "Manufacturers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldComment: "The full name of the manufacturer or designer.");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Manufacturers",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Primary key of the manufacturer.")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "LampStyleName",
                table: "LampStyles",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldComment: "The name of the lamp style category.");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "LampStyles",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Primary key of the lamp style.")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldComment: "The last name of the user.");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldComment: "The first name of the user.");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "The delivery or contact address of the user.");
        }
    }
}
