using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    public partial class initialCatalog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaloriesLimit = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Calories = table.Column<int>(type: "int", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meals_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Meals_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCheatingFoods",
                columns: table => new
                {
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCheatingFoods", x => new { x.UserId, x.FoodId });
                    table.ForeignKey(
                        name: "FK_UserCheatingFoods_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCheatingFoods_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "Meat" },
                    { 2, "Chicken" },
                    { 3, "Apple" },
                    { 4, "Fish" },
                    { 5, "Banana" },
                    { 6, "Chocolate" },
                    { 7, "Candy" },
                    { 8, "Potato" },
                    { 9, "Pizza" },
                    { 10, "Egg" },
                    { 11, "Hot dogs" },
                    { 12, "Sushi" },
                    { 13, "Ice cream" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CaloriesLimit", "Role", "Username" },
                values: new object[,]
                {
                    { 1, 2100, 1, "Jose" },
                    { 2, 2100, 1, "Jorge" },
                    { 3, 2100, 1, "Maxi" },
                    { 4, 2100, 1, "Diego" },
                    { 5, 2100, 2, "Martin" }
                });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "Calories", "Date", "FoodId", "UserId" },
                values: new object[,]
                {
                    { 1, 1164, new DateTime(2022, 4, 24, 0, 39, 0, 0, DateTimeKind.Local), 11, 1 },
                    { 2, 877, new DateTime(2022, 4, 28, 13, 6, 0, 0, DateTimeKind.Local), 6, 1 },
                    { 3, 562, new DateTime(2022, 4, 23, 14, 25, 0, 0, DateTimeKind.Local), 12, 5 },
                    { 4, 437, new DateTime(2022, 4, 19, 0, 37, 0, 0, DateTimeKind.Local), 11, 3 },
                    { 5, 895, new DateTime(2022, 4, 5, 2, 43, 0, 0, DateTimeKind.Local), 7, 3 },
                    { 6, 674, new DateTime(2022, 4, 7, 9, 11, 0, 0, DateTimeKind.Local), 7, 1 },
                    { 7, 507, new DateTime(2022, 4, 29, 19, 22, 0, 0, DateTimeKind.Local), 11, 2 },
                    { 8, 422, new DateTime(2022, 4, 27, 15, 26, 0, 0, DateTimeKind.Local), 3, 3 },
                    { 9, 596, new DateTime(2022, 5, 3, 10, 46, 0, 0, DateTimeKind.Local), 8, 3 },
                    { 10, 1131, new DateTime(2022, 4, 7, 7, 12, 0, 0, DateTimeKind.Local), 8, 2 },
                    { 11, 1074, new DateTime(2022, 4, 7, 13, 26, 0, 0, DateTimeKind.Local), 12, 1 },
                    { 12, 614, new DateTime(2022, 4, 3, 17, 9, 0, 0, DateTimeKind.Local), 1, 4 },
                    { 13, 1061, new DateTime(2022, 4, 7, 4, 18, 0, 0, DateTimeKind.Local), 4, 1 },
                    { 14, 908, new DateTime(2022, 4, 7, 23, 19, 0, 0, DateTimeKind.Local), 9, 3 },
                    { 15, 854, new DateTime(2022, 4, 6, 14, 38, 0, 0, DateTimeKind.Local), 4, 3 },
                    { 16, 975, new DateTime(2022, 5, 13, 11, 23, 0, 0, DateTimeKind.Local), 6, 1 },
                    { 17, 1022, new DateTime(2022, 4, 7, 17, 8, 0, 0, DateTimeKind.Local), 1, 5 },
                    { 18, 877, new DateTime(2022, 4, 29, 11, 13, 0, 0, DateTimeKind.Local), 3, 1 },
                    { 19, 712, new DateTime(2022, 4, 21, 10, 50, 0, 0, DateTimeKind.Local), 12, 2 },
                    { 20, 1102, new DateTime(2022, 4, 19, 11, 34, 0, 0, DateTimeKind.Local), 10, 4 },
                    { 21, 690, new DateTime(2022, 4, 8, 2, 49, 0, 0, DateTimeKind.Local), 5, 3 },
                    { 22, 1179, new DateTime(2022, 5, 24, 12, 17, 0, 0, DateTimeKind.Local), 11, 4 },
                    { 23, 1199, new DateTime(2022, 4, 6, 2, 5, 0, 0, DateTimeKind.Local), 2, 3 },
                    { 24, 1051, new DateTime(2022, 4, 24, 21, 46, 0, 0, DateTimeKind.Local), 9, 4 },
                    { 25, 527, new DateTime(2022, 4, 9, 19, 26, 0, 0, DateTimeKind.Local), 1, 4 },
                    { 26, 972, new DateTime(2022, 4, 21, 8, 34, 0, 0, DateTimeKind.Local), 11, 2 },
                    { 27, 995, new DateTime(2022, 4, 2, 10, 18, 0, 0, DateTimeKind.Local), 5, 1 },
                    { 28, 914, new DateTime(2022, 4, 20, 8, 36, 0, 0, DateTimeKind.Local), 7, 5 },
                    { 29, 1138, new DateTime(2022, 5, 15, 18, 37, 0, 0, DateTimeKind.Local), 7, 1 },
                    { 30, 1192, new DateTime(2022, 5, 12, 18, 59, 0, 0, DateTimeKind.Local), 13, 2 },
                    { 31, 828, new DateTime(2022, 4, 12, 16, 39, 0, 0, DateTimeKind.Local), 1, 2 },
                    { 32, 486, new DateTime(2022, 4, 21, 9, 16, 0, 0, DateTimeKind.Local), 8, 1 },
                    { 33, 429, new DateTime(2022, 4, 26, 20, 59, 0, 0, DateTimeKind.Local), 5, 4 },
                    { 34, 1144, new DateTime(2022, 4, 20, 19, 38, 0, 0, DateTimeKind.Local), 6, 5 },
                    { 35, 450, new DateTime(2022, 5, 13, 2, 52, 0, 0, DateTimeKind.Local), 6, 5 },
                    { 36, 853, new DateTime(2022, 4, 17, 14, 26, 0, 0, DateTimeKind.Local), 1, 1 },
                    { 37, 873, new DateTime(2022, 4, 17, 5, 1, 0, 0, DateTimeKind.Local), 1, 3 },
                    { 38, 1041, new DateTime(2022, 5, 14, 18, 0, 0, 0, DateTimeKind.Local), 13, 1 },
                    { 39, 834, new DateTime(2022, 3, 27, 3, 16, 0, 0, DateTimeKind.Local), 11, 3 },
                    { 40, 760, new DateTime(2022, 5, 4, 7, 58, 0, 0, DateTimeKind.Local), 5, 3 },
                    { 41, 1019, new DateTime(2022, 5, 14, 9, 48, 0, 0, DateTimeKind.Local), 10, 3 },
                    { 42, 734, new DateTime(2022, 5, 17, 11, 11, 0, 0, DateTimeKind.Local), 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "Calories", "Date", "FoodId", "UserId" },
                values: new object[,]
                {
                    { 43, 580, new DateTime(2022, 4, 11, 0, 30, 0, 0, DateTimeKind.Local), 5, 2 },
                    { 44, 939, new DateTime(2022, 4, 20, 14, 41, 0, 0, DateTimeKind.Local), 10, 5 },
                    { 45, 1092, new DateTime(2022, 4, 28, 11, 24, 0, 0, DateTimeKind.Local), 7, 3 },
                    { 46, 557, new DateTime(2022, 4, 21, 17, 43, 0, 0, DateTimeKind.Local), 5, 1 },
                    { 47, 408, new DateTime(2022, 5, 10, 21, 0, 0, 0, DateTimeKind.Local), 6, 1 },
                    { 48, 881, new DateTime(2022, 4, 7, 15, 29, 0, 0, DateTimeKind.Local), 10, 2 },
                    { 49, 928, new DateTime(2022, 5, 17, 0, 5, 0, 0, DateTimeKind.Local), 13, 1 },
                    { 50, 496, new DateTime(2022, 5, 15, 2, 58, 0, 0, DateTimeKind.Local), 2, 4 },
                    { 51, 488, new DateTime(2022, 4, 20, 15, 17, 0, 0, DateTimeKind.Local), 2, 4 },
                    { 52, 490, new DateTime(2022, 5, 18, 2, 7, 0, 0, DateTimeKind.Local), 2, 5 },
                    { 53, 1148, new DateTime(2022, 5, 4, 20, 18, 0, 0, DateTimeKind.Local), 5, 3 },
                    { 54, 783, new DateTime(2022, 4, 8, 19, 52, 0, 0, DateTimeKind.Local), 11, 2 },
                    { 55, 781, new DateTime(2022, 4, 20, 14, 26, 0, 0, DateTimeKind.Local), 11, 4 },
                    { 56, 527, new DateTime(2022, 5, 9, 2, 11, 0, 0, DateTimeKind.Local), 9, 5 },
                    { 57, 482, new DateTime(2022, 4, 11, 16, 22, 0, 0, DateTimeKind.Local), 4, 1 },
                    { 58, 648, new DateTime(2022, 3, 30, 21, 57, 0, 0, DateTimeKind.Local), 11, 3 },
                    { 59, 617, new DateTime(2022, 4, 3, 10, 13, 0, 0, DateTimeKind.Local), 13, 2 },
                    { 60, 893, new DateTime(2022, 4, 16, 14, 50, 0, 0, DateTimeKind.Local), 3, 1 },
                    { 61, 989, new DateTime(2022, 5, 18, 19, 34, 0, 0, DateTimeKind.Local), 6, 2 },
                    { 62, 940, new DateTime(2022, 5, 20, 8, 0, 0, 0, DateTimeKind.Local), 7, 5 },
                    { 63, 1174, new DateTime(2022, 5, 2, 3, 39, 0, 0, DateTimeKind.Local), 1, 3 },
                    { 64, 719, new DateTime(2022, 4, 11, 21, 18, 0, 0, DateTimeKind.Local), 5, 1 },
                    { 65, 1172, new DateTime(2022, 4, 11, 0, 10, 0, 0, DateTimeKind.Local), 8, 4 },
                    { 66, 891, new DateTime(2022, 5, 21, 8, 23, 0, 0, DateTimeKind.Local), 2, 2 },
                    { 67, 1151, new DateTime(2022, 4, 9, 2, 8, 0, 0, DateTimeKind.Local), 6, 2 },
                    { 68, 912, new DateTime(2022, 4, 23, 12, 58, 0, 0, DateTimeKind.Local), 9, 5 },
                    { 69, 970, new DateTime(2022, 5, 4, 7, 49, 0, 0, DateTimeKind.Local), 9, 5 },
                    { 70, 976, new DateTime(2022, 4, 16, 8, 45, 0, 0, DateTimeKind.Local), 3, 5 },
                    { 71, 1145, new DateTime(2022, 3, 28, 7, 22, 0, 0, DateTimeKind.Local), 2, 1 },
                    { 72, 1040, new DateTime(2022, 5, 13, 14, 4, 0, 0, DateTimeKind.Local), 4, 2 },
                    { 73, 867, new DateTime(2022, 4, 21, 7, 4, 0, 0, DateTimeKind.Local), 12, 4 },
                    { 74, 1014, new DateTime(2022, 3, 27, 6, 16, 0, 0, DateTimeKind.Local), 7, 1 },
                    { 75, 826, new DateTime(2022, 4, 26, 7, 59, 0, 0, DateTimeKind.Local), 4, 5 },
                    { 76, 747, new DateTime(2022, 4, 13, 3, 25, 0, 0, DateTimeKind.Local), 2, 5 },
                    { 77, 1075, new DateTime(2022, 3, 26, 17, 22, 0, 0, DateTimeKind.Local), 4, 5 },
                    { 78, 422, new DateTime(2022, 4, 2, 6, 17, 0, 0, DateTimeKind.Local), 7, 3 },
                    { 79, 573, new DateTime(2022, 4, 29, 10, 19, 0, 0, DateTimeKind.Local), 11, 5 },
                    { 80, 961, new DateTime(2022, 3, 26, 19, 3, 0, 0, DateTimeKind.Local), 10, 2 },
                    { 81, 1031, new DateTime(2022, 4, 20, 10, 35, 0, 0, DateTimeKind.Local), 3, 3 },
                    { 82, 1157, new DateTime(2022, 5, 16, 0, 30, 0, 0, DateTimeKind.Local), 6, 1 },
                    { 83, 417, new DateTime(2022, 5, 24, 18, 35, 0, 0, DateTimeKind.Local), 1, 1 },
                    { 84, 1132, new DateTime(2022, 4, 22, 10, 44, 0, 0, DateTimeKind.Local), 7, 1 }
                });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "Calories", "Date", "FoodId", "UserId" },
                values: new object[,]
                {
                    { 85, 655, new DateTime(2022, 5, 16, 19, 7, 0, 0, DateTimeKind.Local), 6, 1 },
                    { 86, 735, new DateTime(2022, 4, 10, 7, 13, 0, 0, DateTimeKind.Local), 11, 4 },
                    { 87, 1088, new DateTime(2022, 5, 16, 8, 48, 0, 0, DateTimeKind.Local), 7, 1 },
                    { 88, 617, new DateTime(2022, 5, 17, 6, 8, 0, 0, DateTimeKind.Local), 3, 1 },
                    { 89, 920, new DateTime(2022, 4, 3, 17, 5, 0, 0, DateTimeKind.Local), 7, 1 },
                    { 90, 1017, new DateTime(2022, 4, 9, 1, 45, 0, 0, DateTimeKind.Local), 4, 4 },
                    { 91, 427, new DateTime(2022, 5, 22, 1, 26, 0, 0, DateTimeKind.Local), 3, 3 },
                    { 92, 953, new DateTime(2022, 5, 14, 8, 58, 0, 0, DateTimeKind.Local), 12, 3 },
                    { 93, 841, new DateTime(2022, 4, 3, 23, 36, 0, 0, DateTimeKind.Local), 8, 5 },
                    { 94, 833, new DateTime(2022, 4, 8, 15, 43, 0, 0, DateTimeKind.Local), 3, 3 },
                    { 95, 847, new DateTime(2022, 4, 12, 17, 46, 0, 0, DateTimeKind.Local), 5, 4 },
                    { 96, 661, new DateTime(2022, 4, 2, 10, 15, 0, 0, DateTimeKind.Local), 3, 1 },
                    { 97, 936, new DateTime(2022, 3, 29, 20, 7, 0, 0, DateTimeKind.Local), 6, 4 },
                    { 98, 580, new DateTime(2022, 4, 26, 19, 5, 0, 0, DateTimeKind.Local), 11, 5 },
                    { 99, 687, new DateTime(2022, 4, 19, 11, 46, 0, 0, DateTimeKind.Local), 12, 1 },
                    { 100, 1106, new DateTime(2022, 4, 30, 12, 42, 0, 0, DateTimeKind.Local), 13, 2 },
                    { 101, 768, new DateTime(2022, 5, 6, 3, 55, 0, 0, DateTimeKind.Local), 3, 3 },
                    { 102, 722, new DateTime(2022, 5, 17, 11, 26, 0, 0, DateTimeKind.Local), 2, 3 },
                    { 103, 774, new DateTime(2022, 4, 21, 13, 2, 0, 0, DateTimeKind.Local), 3, 5 },
                    { 104, 1154, new DateTime(2022, 4, 6, 19, 57, 0, 0, DateTimeKind.Local), 12, 1 },
                    { 105, 705, new DateTime(2022, 5, 11, 0, 2, 0, 0, DateTimeKind.Local), 6, 4 },
                    { 106, 529, new DateTime(2022, 4, 27, 0, 3, 0, 0, DateTimeKind.Local), 9, 5 },
                    { 107, 969, new DateTime(2022, 4, 26, 20, 25, 0, 0, DateTimeKind.Local), 8, 1 },
                    { 108, 804, new DateTime(2022, 4, 23, 9, 51, 0, 0, DateTimeKind.Local), 13, 5 },
                    { 109, 535, new DateTime(2022, 3, 26, 21, 48, 0, 0, DateTimeKind.Local), 12, 1 },
                    { 110, 987, new DateTime(2022, 5, 9, 9, 46, 0, 0, DateTimeKind.Local), 3, 5 },
                    { 111, 484, new DateTime(2022, 4, 3, 2, 8, 0, 0, DateTimeKind.Local), 6, 5 },
                    { 112, 962, new DateTime(2022, 3, 28, 21, 59, 0, 0, DateTimeKind.Local), 10, 2 },
                    { 113, 729, new DateTime(2022, 5, 1, 4, 30, 0, 0, DateTimeKind.Local), 11, 4 },
                    { 114, 1005, new DateTime(2022, 5, 13, 21, 29, 0, 0, DateTimeKind.Local), 3, 5 },
                    { 115, 610, new DateTime(2022, 4, 8, 23, 22, 0, 0, DateTimeKind.Local), 6, 1 },
                    { 116, 1094, new DateTime(2022, 5, 18, 4, 39, 0, 0, DateTimeKind.Local), 5, 3 },
                    { 117, 1132, new DateTime(2022, 4, 10, 22, 44, 0, 0, DateTimeKind.Local), 9, 3 },
                    { 118, 955, new DateTime(2022, 5, 19, 13, 55, 0, 0, DateTimeKind.Local), 12, 2 },
                    { 119, 968, new DateTime(2022, 5, 3, 11, 0, 0, 0, DateTimeKind.Local), 7, 1 },
                    { 120, 758, new DateTime(2022, 5, 6, 22, 56, 0, 0, DateTimeKind.Local), 13, 5 },
                    { 121, 1031, new DateTime(2022, 4, 26, 9, 46, 0, 0, DateTimeKind.Local), 1, 3 },
                    { 122, 579, new DateTime(2022, 3, 30, 21, 42, 0, 0, DateTimeKind.Local), 6, 3 },
                    { 123, 931, new DateTime(2022, 4, 16, 19, 3, 0, 0, DateTimeKind.Local), 7, 1 },
                    { 124, 425, new DateTime(2022, 4, 7, 22, 22, 0, 0, DateTimeKind.Local), 3, 3 },
                    { 125, 907, new DateTime(2022, 4, 14, 19, 7, 0, 0, DateTimeKind.Local), 1, 5 },
                    { 126, 570, new DateTime(2022, 3, 27, 15, 42, 0, 0, DateTimeKind.Local), 8, 1 }
                });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "Calories", "Date", "FoodId", "UserId" },
                values: new object[,]
                {
                    { 127, 1171, new DateTime(2022, 4, 20, 20, 5, 0, 0, DateTimeKind.Local), 12, 2 },
                    { 128, 583, new DateTime(2022, 4, 3, 12, 24, 0, 0, DateTimeKind.Local), 12, 4 },
                    { 129, 904, new DateTime(2022, 5, 10, 6, 30, 0, 0, DateTimeKind.Local), 4, 3 },
                    { 130, 1098, new DateTime(2022, 5, 2, 5, 53, 0, 0, DateTimeKind.Local), 8, 2 },
                    { 131, 1002, new DateTime(2022, 4, 9, 21, 7, 0, 0, DateTimeKind.Local), 4, 5 },
                    { 132, 440, new DateTime(2022, 4, 30, 5, 12, 0, 0, DateTimeKind.Local), 13, 5 },
                    { 133, 810, new DateTime(2022, 5, 10, 23, 33, 0, 0, DateTimeKind.Local), 10, 1 },
                    { 134, 795, new DateTime(2022, 4, 7, 7, 45, 0, 0, DateTimeKind.Local), 13, 4 },
                    { 135, 681, new DateTime(2022, 4, 27, 19, 2, 0, 0, DateTimeKind.Local), 3, 3 },
                    { 136, 816, new DateTime(2022, 4, 3, 0, 46, 0, 0, DateTimeKind.Local), 11, 2 },
                    { 137, 452, new DateTime(2022, 5, 19, 10, 52, 0, 0, DateTimeKind.Local), 4, 3 },
                    { 138, 490, new DateTime(2022, 4, 10, 4, 41, 0, 0, DateTimeKind.Local), 4, 1 },
                    { 139, 1034, new DateTime(2022, 5, 4, 20, 4, 0, 0, DateTimeKind.Local), 5, 5 },
                    { 140, 651, new DateTime(2022, 5, 7, 15, 27, 0, 0, DateTimeKind.Local), 11, 3 },
                    { 141, 509, new DateTime(2022, 4, 19, 5, 26, 0, 0, DateTimeKind.Local), 12, 5 },
                    { 142, 624, new DateTime(2022, 4, 24, 18, 25, 0, 0, DateTimeKind.Local), 3, 3 },
                    { 143, 541, new DateTime(2022, 4, 17, 5, 2, 0, 0, DateTimeKind.Local), 7, 3 },
                    { 144, 608, new DateTime(2022, 4, 20, 8, 48, 0, 0, DateTimeKind.Local), 12, 3 },
                    { 145, 738, new DateTime(2022, 4, 4, 3, 52, 0, 0, DateTimeKind.Local), 12, 4 },
                    { 146, 1088, new DateTime(2022, 5, 17, 14, 32, 0, 0, DateTimeKind.Local), 1, 1 },
                    { 147, 695, new DateTime(2022, 5, 4, 8, 11, 0, 0, DateTimeKind.Local), 5, 1 },
                    { 148, 492, new DateTime(2022, 4, 29, 21, 39, 0, 0, DateTimeKind.Local), 1, 4 },
                    { 149, 953, new DateTime(2022, 4, 24, 3, 1, 0, 0, DateTimeKind.Local), 5, 5 },
                    { 150, 787, new DateTime(2022, 4, 26, 7, 51, 0, 0, DateTimeKind.Local), 7, 2 },
                    { 151, 931, new DateTime(2022, 5, 1, 19, 4, 0, 0, DateTimeKind.Local), 13, 5 },
                    { 152, 699, new DateTime(2022, 4, 11, 11, 9, 0, 0, DateTimeKind.Local), 2, 2 },
                    { 153, 666, new DateTime(2022, 4, 2, 12, 8, 0, 0, DateTimeKind.Local), 13, 4 },
                    { 154, 638, new DateTime(2022, 4, 26, 6, 34, 0, 0, DateTimeKind.Local), 13, 1 },
                    { 155, 597, new DateTime(2022, 5, 15, 11, 26, 0, 0, DateTimeKind.Local), 8, 4 },
                    { 156, 862, new DateTime(2022, 4, 18, 16, 8, 0, 0, DateTimeKind.Local), 6, 3 },
                    { 157, 797, new DateTime(2022, 3, 26, 22, 46, 0, 0, DateTimeKind.Local), 4, 2 },
                    { 158, 817, new DateTime(2022, 5, 22, 9, 5, 0, 0, DateTimeKind.Local), 1, 4 },
                    { 159, 654, new DateTime(2022, 5, 4, 6, 45, 0, 0, DateTimeKind.Local), 2, 4 },
                    { 160, 640, new DateTime(2022, 3, 27, 7, 7, 0, 0, DateTimeKind.Local), 9, 4 },
                    { 161, 794, new DateTime(2022, 4, 19, 8, 39, 0, 0, DateTimeKind.Local), 6, 3 },
                    { 162, 619, new DateTime(2022, 3, 30, 0, 40, 0, 0, DateTimeKind.Local), 9, 3 },
                    { 163, 723, new DateTime(2022, 4, 23, 12, 45, 0, 0, DateTimeKind.Local), 1, 5 },
                    { 164, 1069, new DateTime(2022, 3, 30, 3, 26, 0, 0, DateTimeKind.Local), 4, 2 },
                    { 165, 1017, new DateTime(2022, 5, 9, 19, 8, 0, 0, DateTimeKind.Local), 11, 3 },
                    { 166, 1193, new DateTime(2022, 4, 7, 13, 56, 0, 0, DateTimeKind.Local), 2, 5 },
                    { 167, 531, new DateTime(2022, 4, 27, 21, 15, 0, 0, DateTimeKind.Local), 4, 2 },
                    { 168, 647, new DateTime(2022, 4, 13, 3, 43, 0, 0, DateTimeKind.Local), 6, 5 }
                });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "Calories", "Date", "FoodId", "UserId" },
                values: new object[,]
                {
                    { 169, 836, new DateTime(2022, 3, 31, 22, 48, 0, 0, DateTimeKind.Local), 8, 3 },
                    { 170, 807, new DateTime(2022, 3, 30, 23, 17, 0, 0, DateTimeKind.Local), 9, 1 },
                    { 171, 438, new DateTime(2022, 3, 27, 15, 41, 0, 0, DateTimeKind.Local), 3, 4 },
                    { 172, 981, new DateTime(2022, 4, 12, 15, 1, 0, 0, DateTimeKind.Local), 13, 5 },
                    { 173, 742, new DateTime(2022, 5, 17, 13, 29, 0, 0, DateTimeKind.Local), 9, 4 },
                    { 174, 1120, new DateTime(2022, 4, 26, 12, 6, 0, 0, DateTimeKind.Local), 4, 1 },
                    { 175, 423, new DateTime(2022, 5, 11, 1, 59, 0, 0, DateTimeKind.Local), 13, 5 },
                    { 176, 722, new DateTime(2022, 4, 6, 23, 17, 0, 0, DateTimeKind.Local), 13, 1 },
                    { 177, 772, new DateTime(2022, 4, 27, 0, 48, 0, 0, DateTimeKind.Local), 13, 2 },
                    { 178, 407, new DateTime(2022, 4, 24, 18, 17, 0, 0, DateTimeKind.Local), 5, 1 },
                    { 179, 578, new DateTime(2022, 5, 3, 16, 19, 0, 0, DateTimeKind.Local), 11, 2 },
                    { 180, 487, new DateTime(2022, 4, 17, 0, 35, 0, 0, DateTimeKind.Local), 4, 2 },
                    { 181, 526, new DateTime(2022, 4, 25, 4, 50, 0, 0, DateTimeKind.Local), 3, 1 },
                    { 182, 500, new DateTime(2022, 3, 27, 17, 33, 0, 0, DateTimeKind.Local), 2, 1 },
                    { 183, 604, new DateTime(2022, 4, 20, 8, 17, 0, 0, DateTimeKind.Local), 8, 3 },
                    { 184, 872, new DateTime(2022, 4, 6, 14, 45, 0, 0, DateTimeKind.Local), 10, 2 },
                    { 185, 963, new DateTime(2022, 5, 4, 20, 30, 0, 0, DateTimeKind.Local), 3, 2 },
                    { 186, 1124, new DateTime(2022, 5, 7, 14, 40, 0, 0, DateTimeKind.Local), 3, 2 },
                    { 187, 540, new DateTime(2022, 4, 15, 4, 34, 0, 0, DateTimeKind.Local), 8, 1 },
                    { 188, 1185, new DateTime(2022, 5, 16, 6, 24, 0, 0, DateTimeKind.Local), 13, 2 },
                    { 189, 655, new DateTime(2022, 5, 8, 21, 13, 0, 0, DateTimeKind.Local), 4, 3 },
                    { 190, 448, new DateTime(2022, 4, 25, 11, 53, 0, 0, DateTimeKind.Local), 2, 3 },
                    { 191, 728, new DateTime(2022, 4, 23, 2, 3, 0, 0, DateTimeKind.Local), 1, 2 },
                    { 192, 975, new DateTime(2022, 5, 2, 23, 15, 0, 0, DateTimeKind.Local), 2, 4 },
                    { 193, 464, new DateTime(2022, 5, 16, 14, 39, 0, 0, DateTimeKind.Local), 3, 3 },
                    { 194, 702, new DateTime(2022, 4, 14, 11, 50, 0, 0, DateTimeKind.Local), 3, 4 },
                    { 195, 1122, new DateTime(2022, 5, 2, 14, 37, 0, 0, DateTimeKind.Local), 9, 4 },
                    { 196, 899, new DateTime(2022, 4, 30, 16, 6, 0, 0, DateTimeKind.Local), 10, 4 },
                    { 197, 474, new DateTime(2022, 5, 20, 13, 16, 0, 0, DateTimeKind.Local), 3, 2 },
                    { 198, 934, new DateTime(2022, 4, 4, 18, 25, 0, 0, DateTimeKind.Local), 5, 1 },
                    { 199, 844, new DateTime(2022, 5, 11, 0, 8, 0, 0, DateTimeKind.Local), 2, 4 },
                    { 200, 496, new DateTime(2022, 4, 16, 9, 49, 0, 0, DateTimeKind.Local), 7, 3 },
                    { 201, 1066, new DateTime(2022, 5, 18, 5, 12, 0, 0, DateTimeKind.Local), 4, 5 },
                    { 202, 882, new DateTime(2022, 5, 7, 6, 56, 0, 0, DateTimeKind.Local), 6, 2 },
                    { 203, 813, new DateTime(2022, 4, 27, 23, 27, 0, 0, DateTimeKind.Local), 2, 3 },
                    { 204, 408, new DateTime(2022, 4, 15, 22, 9, 0, 0, DateTimeKind.Local), 11, 2 },
                    { 205, 978, new DateTime(2022, 5, 1, 23, 37, 0, 0, DateTimeKind.Local), 9, 5 },
                    { 206, 787, new DateTime(2022, 4, 22, 12, 41, 0, 0, DateTimeKind.Local), 10, 1 },
                    { 207, 942, new DateTime(2022, 4, 27, 12, 24, 0, 0, DateTimeKind.Local), 4, 1 },
                    { 208, 992, new DateTime(2022, 4, 10, 13, 45, 0, 0, DateTimeKind.Local), 7, 1 },
                    { 209, 1128, new DateTime(2022, 3, 31, 5, 12, 0, 0, DateTimeKind.Local), 4, 3 },
                    { 210, 546, new DateTime(2022, 5, 3, 22, 44, 0, 0, DateTimeKind.Local), 10, 4 }
                });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "Calories", "Date", "FoodId", "UserId" },
                values: new object[,]
                {
                    { 211, 871, new DateTime(2022, 4, 22, 10, 31, 0, 0, DateTimeKind.Local), 8, 3 },
                    { 212, 1176, new DateTime(2022, 5, 5, 6, 11, 0, 0, DateTimeKind.Local), 13, 4 },
                    { 213, 1135, new DateTime(2022, 5, 14, 5, 57, 0, 0, DateTimeKind.Local), 8, 5 },
                    { 214, 524, new DateTime(2022, 4, 17, 9, 31, 0, 0, DateTimeKind.Local), 1, 4 },
                    { 215, 412, new DateTime(2022, 4, 19, 16, 25, 0, 0, DateTimeKind.Local), 1, 2 },
                    { 216, 840, new DateTime(2022, 5, 3, 6, 53, 0, 0, DateTimeKind.Local), 13, 1 },
                    { 217, 1141, new DateTime(2022, 4, 29, 9, 56, 0, 0, DateTimeKind.Local), 9, 2 },
                    { 218, 1141, new DateTime(2022, 5, 14, 14, 59, 0, 0, DateTimeKind.Local), 1, 1 },
                    { 219, 938, new DateTime(2022, 4, 4, 17, 4, 0, 0, DateTimeKind.Local), 4, 3 },
                    { 220, 1181, new DateTime(2022, 4, 5, 20, 7, 0, 0, DateTimeKind.Local), 4, 3 },
                    { 221, 1179, new DateTime(2022, 4, 2, 19, 10, 0, 0, DateTimeKind.Local), 2, 1 },
                    { 222, 1040, new DateTime(2022, 4, 5, 3, 14, 0, 0, DateTimeKind.Local), 10, 1 },
                    { 223, 539, new DateTime(2022, 4, 1, 18, 3, 0, 0, DateTimeKind.Local), 10, 5 },
                    { 224, 879, new DateTime(2022, 4, 8, 11, 41, 0, 0, DateTimeKind.Local), 10, 3 },
                    { 225, 723, new DateTime(2022, 4, 1, 16, 12, 0, 0, DateTimeKind.Local), 1, 3 },
                    { 226, 588, new DateTime(2022, 3, 31, 8, 27, 0, 0, DateTimeKind.Local), 13, 2 },
                    { 227, 402, new DateTime(2022, 3, 27, 14, 27, 0, 0, DateTimeKind.Local), 8, 2 },
                    { 228, 897, new DateTime(2022, 4, 21, 3, 12, 0, 0, DateTimeKind.Local), 6, 4 },
                    { 229, 1071, new DateTime(2022, 3, 30, 3, 20, 0, 0, DateTimeKind.Local), 9, 3 },
                    { 230, 726, new DateTime(2022, 5, 22, 12, 6, 0, 0, DateTimeKind.Local), 10, 3 },
                    { 231, 689, new DateTime(2022, 4, 11, 10, 44, 0, 0, DateTimeKind.Local), 11, 4 },
                    { 232, 759, new DateTime(2022, 4, 17, 19, 1, 0, 0, DateTimeKind.Local), 12, 2 },
                    { 233, 730, new DateTime(2022, 5, 7, 2, 45, 0, 0, DateTimeKind.Local), 3, 1 },
                    { 234, 846, new DateTime(2022, 5, 22, 5, 0, 0, 0, DateTimeKind.Local), 1, 4 },
                    { 235, 707, new DateTime(2022, 4, 25, 3, 24, 0, 0, DateTimeKind.Local), 9, 5 },
                    { 236, 990, new DateTime(2022, 5, 12, 13, 14, 0, 0, DateTimeKind.Local), 8, 3 },
                    { 237, 532, new DateTime(2022, 5, 5, 14, 19, 0, 0, DateTimeKind.Local), 3, 4 },
                    { 238, 1084, new DateTime(2022, 3, 29, 6, 3, 0, 0, DateTimeKind.Local), 11, 5 },
                    { 239, 1062, new DateTime(2022, 5, 7, 20, 26, 0, 0, DateTimeKind.Local), 8, 5 },
                    { 240, 958, new DateTime(2022, 4, 10, 2, 21, 0, 0, DateTimeKind.Local), 1, 2 },
                    { 241, 953, new DateTime(2022, 4, 11, 2, 53, 0, 0, DateTimeKind.Local), 2, 4 },
                    { 242, 973, new DateTime(2022, 5, 9, 21, 42, 0, 0, DateTimeKind.Local), 8, 1 },
                    { 243, 1087, new DateTime(2022, 5, 2, 12, 41, 0, 0, DateTimeKind.Local), 9, 2 },
                    { 244, 752, new DateTime(2022, 4, 22, 22, 36, 0, 0, DateTimeKind.Local), 9, 4 },
                    { 245, 487, new DateTime(2022, 3, 26, 11, 42, 0, 0, DateTimeKind.Local), 2, 3 },
                    { 246, 1122, new DateTime(2022, 4, 5, 7, 44, 0, 0, DateTimeKind.Local), 7, 2 },
                    { 247, 906, new DateTime(2022, 4, 28, 22, 58, 0, 0, DateTimeKind.Local), 11, 4 },
                    { 248, 512, new DateTime(2022, 4, 14, 1, 45, 0, 0, DateTimeKind.Local), 7, 3 },
                    { 249, 1075, new DateTime(2022, 3, 29, 1, 26, 0, 0, DateTimeKind.Local), 6, 1 },
                    { 250, 821, new DateTime(2022, 5, 9, 15, 43, 0, 0, DateTimeKind.Local), 8, 5 },
                    { 251, 595, new DateTime(2022, 5, 8, 20, 35, 0, 0, DateTimeKind.Local), 11, 1 },
                    { 252, 598, new DateTime(2022, 5, 7, 19, 10, 0, 0, DateTimeKind.Local), 5, 2 }
                });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "Calories", "Date", "FoodId", "UserId" },
                values: new object[,]
                {
                    { 253, 1030, new DateTime(2022, 4, 23, 7, 11, 0, 0, DateTimeKind.Local), 1, 5 },
                    { 254, 676, new DateTime(2022, 4, 3, 17, 34, 0, 0, DateTimeKind.Local), 6, 3 },
                    { 255, 706, new DateTime(2022, 3, 27, 6, 28, 0, 0, DateTimeKind.Local), 11, 2 },
                    { 256, 425, new DateTime(2022, 4, 30, 0, 18, 0, 0, DateTimeKind.Local), 9, 2 },
                    { 257, 1080, new DateTime(2022, 4, 11, 15, 9, 0, 0, DateTimeKind.Local), 10, 4 },
                    { 258, 1176, new DateTime(2022, 5, 17, 13, 44, 0, 0, DateTimeKind.Local), 11, 1 },
                    { 259, 566, new DateTime(2022, 5, 5, 11, 13, 0, 0, DateTimeKind.Local), 4, 4 },
                    { 260, 644, new DateTime(2022, 5, 11, 15, 39, 0, 0, DateTimeKind.Local), 8, 5 },
                    { 261, 624, new DateTime(2022, 5, 18, 5, 1, 0, 0, DateTimeKind.Local), 8, 4 },
                    { 262, 869, new DateTime(2022, 3, 30, 17, 46, 0, 0, DateTimeKind.Local), 11, 4 },
                    { 263, 1193, new DateTime(2022, 3, 28, 14, 24, 0, 0, DateTimeKind.Local), 1, 1 },
                    { 264, 975, new DateTime(2022, 4, 12, 18, 31, 0, 0, DateTimeKind.Local), 10, 3 },
                    { 265, 458, new DateTime(2022, 4, 23, 1, 10, 0, 0, DateTimeKind.Local), 9, 1 },
                    { 266, 461, new DateTime(2022, 4, 21, 11, 3, 0, 0, DateTimeKind.Local), 2, 5 },
                    { 267, 1120, new DateTime(2022, 5, 18, 22, 36, 0, 0, DateTimeKind.Local), 1, 2 },
                    { 268, 1111, new DateTime(2022, 4, 16, 19, 19, 0, 0, DateTimeKind.Local), 7, 2 },
                    { 269, 1012, new DateTime(2022, 5, 4, 18, 35, 0, 0, DateTimeKind.Local), 5, 1 },
                    { 270, 750, new DateTime(2022, 5, 14, 23, 51, 0, 0, DateTimeKind.Local), 1, 1 },
                    { 271, 997, new DateTime(2022, 4, 12, 3, 30, 0, 0, DateTimeKind.Local), 12, 3 },
                    { 272, 710, new DateTime(2022, 4, 1, 21, 11, 0, 0, DateTimeKind.Local), 13, 5 },
                    { 273, 593, new DateTime(2022, 3, 30, 3, 38, 0, 0, DateTimeKind.Local), 6, 1 },
                    { 274, 795, new DateTime(2022, 3, 29, 3, 21, 0, 0, DateTimeKind.Local), 8, 2 },
                    { 275, 973, new DateTime(2022, 3, 30, 18, 49, 0, 0, DateTimeKind.Local), 9, 4 },
                    { 276, 1079, new DateTime(2022, 4, 3, 9, 54, 0, 0, DateTimeKind.Local), 7, 4 },
                    { 277, 953, new DateTime(2022, 4, 9, 19, 33, 0, 0, DateTimeKind.Local), 8, 1 },
                    { 278, 816, new DateTime(2022, 5, 9, 23, 16, 0, 0, DateTimeKind.Local), 5, 2 },
                    { 279, 642, new DateTime(2022, 3, 26, 13, 3, 0, 0, DateTimeKind.Local), 3, 3 },
                    { 280, 972, new DateTime(2022, 4, 1, 14, 10, 0, 0, DateTimeKind.Local), 2, 4 },
                    { 281, 467, new DateTime(2022, 3, 31, 23, 23, 0, 0, DateTimeKind.Local), 13, 2 },
                    { 282, 524, new DateTime(2022, 3, 28, 1, 28, 0, 0, DateTimeKind.Local), 4, 2 },
                    { 283, 497, new DateTime(2022, 3, 31, 15, 18, 0, 0, DateTimeKind.Local), 7, 1 },
                    { 284, 1137, new DateTime(2022, 5, 9, 23, 43, 0, 0, DateTimeKind.Local), 12, 3 },
                    { 285, 505, new DateTime(2022, 3, 31, 7, 11, 0, 0, DateTimeKind.Local), 13, 4 },
                    { 286, 791, new DateTime(2022, 4, 26, 21, 33, 0, 0, DateTimeKind.Local), 1, 1 },
                    { 287, 1009, new DateTime(2022, 5, 7, 3, 52, 0, 0, DateTimeKind.Local), 9, 3 },
                    { 288, 549, new DateTime(2022, 4, 15, 23, 19, 0, 0, DateTimeKind.Local), 3, 4 },
                    { 289, 1108, new DateTime(2022, 5, 7, 6, 37, 0, 0, DateTimeKind.Local), 2, 2 },
                    { 290, 485, new DateTime(2022, 5, 10, 9, 12, 0, 0, DateTimeKind.Local), 10, 1 },
                    { 291, 643, new DateTime(2022, 5, 12, 20, 29, 0, 0, DateTimeKind.Local), 13, 5 },
                    { 292, 1063, new DateTime(2022, 4, 28, 12, 7, 0, 0, DateTimeKind.Local), 3, 3 },
                    { 293, 936, new DateTime(2022, 4, 7, 6, 6, 0, 0, DateTimeKind.Local), 12, 3 },
                    { 294, 804, new DateTime(2022, 5, 12, 21, 8, 0, 0, DateTimeKind.Local), 7, 2 }
                });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "Calories", "Date", "FoodId", "UserId" },
                values: new object[,]
                {
                    { 295, 793, new DateTime(2022, 5, 16, 20, 40, 0, 0, DateTimeKind.Local), 3, 2 },
                    { 296, 827, new DateTime(2022, 5, 22, 6, 9, 0, 0, DateTimeKind.Local), 7, 2 },
                    { 297, 651, new DateTime(2022, 4, 2, 16, 33, 0, 0, DateTimeKind.Local), 8, 3 },
                    { 298, 401, new DateTime(2022, 5, 3, 6, 5, 0, 0, DateTimeKind.Local), 7, 5 },
                    { 299, 1181, new DateTime(2022, 5, 17, 13, 6, 0, 0, DateTimeKind.Local), 10, 1 },
                    { 300, 1054, new DateTime(2022, 4, 5, 6, 36, 0, 0, DateTimeKind.Local), 8, 3 },
                    { 301, 557, new DateTime(2022, 4, 8, 0, 55, 0, 0, DateTimeKind.Local), 1, 3 },
                    { 302, 917, new DateTime(2022, 5, 13, 2, 8, 0, 0, DateTimeKind.Local), 12, 1 },
                    { 303, 661, new DateTime(2022, 3, 31, 7, 45, 0, 0, DateTimeKind.Local), 12, 4 },
                    { 304, 776, new DateTime(2022, 5, 5, 20, 21, 0, 0, DateTimeKind.Local), 6, 5 },
                    { 305, 896, new DateTime(2022, 4, 5, 10, 34, 0, 0, DateTimeKind.Local), 1, 5 },
                    { 306, 455, new DateTime(2022, 4, 30, 17, 35, 0, 0, DateTimeKind.Local), 1, 1 },
                    { 307, 474, new DateTime(2022, 4, 8, 16, 58, 0, 0, DateTimeKind.Local), 2, 4 },
                    { 308, 811, new DateTime(2022, 4, 13, 1, 16, 0, 0, DateTimeKind.Local), 8, 5 },
                    { 309, 464, new DateTime(2022, 5, 22, 20, 56, 0, 0, DateTimeKind.Local), 2, 4 },
                    { 310, 994, new DateTime(2022, 4, 13, 4, 0, 0, 0, DateTimeKind.Local), 10, 2 },
                    { 311, 732, new DateTime(2022, 4, 25, 6, 43, 0, 0, DateTimeKind.Local), 7, 4 },
                    { 312, 1131, new DateTime(2022, 4, 5, 2, 37, 0, 0, DateTimeKind.Local), 5, 1 },
                    { 313, 975, new DateTime(2022, 5, 20, 23, 57, 0, 0, DateTimeKind.Local), 6, 4 },
                    { 314, 727, new DateTime(2022, 3, 30, 12, 44, 0, 0, DateTimeKind.Local), 2, 2 },
                    { 315, 962, new DateTime(2022, 4, 9, 20, 36, 0, 0, DateTimeKind.Local), 5, 4 },
                    { 316, 1009, new DateTime(2022, 4, 10, 2, 52, 0, 0, DateTimeKind.Local), 4, 2 },
                    { 317, 1115, new DateTime(2022, 5, 2, 16, 8, 0, 0, DateTimeKind.Local), 7, 4 },
                    { 318, 570, new DateTime(2022, 3, 30, 4, 9, 0, 0, DateTimeKind.Local), 8, 4 },
                    { 319, 998, new DateTime(2022, 3, 28, 5, 55, 0, 0, DateTimeKind.Local), 3, 3 },
                    { 320, 517, new DateTime(2022, 4, 22, 11, 38, 0, 0, DateTimeKind.Local), 11, 2 },
                    { 321, 963, new DateTime(2022, 5, 16, 21, 16, 0, 0, DateTimeKind.Local), 1, 4 },
                    { 322, 904, new DateTime(2022, 3, 29, 9, 1, 0, 0, DateTimeKind.Local), 13, 5 },
                    { 323, 868, new DateTime(2022, 4, 10, 9, 4, 0, 0, DateTimeKind.Local), 10, 2 },
                    { 324, 427, new DateTime(2022, 5, 4, 22, 18, 0, 0, DateTimeKind.Local), 4, 1 },
                    { 325, 1011, new DateTime(2022, 4, 10, 15, 39, 0, 0, DateTimeKind.Local), 10, 1 },
                    { 326, 820, new DateTime(2022, 5, 23, 1, 54, 0, 0, DateTimeKind.Local), 7, 5 },
                    { 327, 835, new DateTime(2022, 3, 27, 8, 6, 0, 0, DateTimeKind.Local), 1, 4 },
                    { 328, 848, new DateTime(2022, 5, 17, 7, 4, 0, 0, DateTimeKind.Local), 1, 5 },
                    { 329, 686, new DateTime(2022, 4, 6, 21, 37, 0, 0, DateTimeKind.Local), 13, 4 },
                    { 330, 404, new DateTime(2022, 5, 6, 21, 57, 0, 0, DateTimeKind.Local), 8, 1 },
                    { 331, 1091, new DateTime(2022, 4, 4, 11, 49, 0, 0, DateTimeKind.Local), 8, 2 },
                    { 332, 512, new DateTime(2022, 5, 23, 12, 58, 0, 0, DateTimeKind.Local), 7, 2 },
                    { 333, 873, new DateTime(2022, 4, 1, 16, 32, 0, 0, DateTimeKind.Local), 6, 3 },
                    { 334, 860, new DateTime(2022, 4, 25, 10, 45, 0, 0, DateTimeKind.Local), 6, 5 },
                    { 335, 955, new DateTime(2022, 5, 23, 11, 27, 0, 0, DateTimeKind.Local), 13, 2 },
                    { 336, 667, new DateTime(2022, 5, 9, 3, 57, 0, 0, DateTimeKind.Local), 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "Calories", "Date", "FoodId", "UserId" },
                values: new object[,]
                {
                    { 337, 1125, new DateTime(2022, 4, 22, 14, 5, 0, 0, DateTimeKind.Local), 1, 5 },
                    { 338, 1011, new DateTime(2022, 5, 9, 22, 30, 0, 0, DateTimeKind.Local), 10, 3 },
                    { 339, 729, new DateTime(2022, 4, 12, 21, 40, 0, 0, DateTimeKind.Local), 6, 4 },
                    { 340, 513, new DateTime(2022, 4, 20, 14, 30, 0, 0, DateTimeKind.Local), 11, 5 },
                    { 341, 1145, new DateTime(2022, 5, 23, 17, 0, 0, 0, DateTimeKind.Local), 7, 4 },
                    { 342, 720, new DateTime(2022, 4, 4, 12, 9, 0, 0, DateTimeKind.Local), 5, 2 },
                    { 343, 702, new DateTime(2022, 4, 21, 6, 27, 0, 0, DateTimeKind.Local), 12, 5 },
                    { 344, 904, new DateTime(2022, 4, 9, 9, 11, 0, 0, DateTimeKind.Local), 1, 5 },
                    { 345, 701, new DateTime(2022, 5, 13, 15, 28, 0, 0, DateTimeKind.Local), 9, 2 },
                    { 346, 1144, new DateTime(2022, 4, 9, 11, 20, 0, 0, DateTimeKind.Local), 13, 4 },
                    { 347, 732, new DateTime(2022, 5, 11, 2, 27, 0, 0, DateTimeKind.Local), 9, 1 },
                    { 348, 655, new DateTime(2022, 3, 30, 7, 38, 0, 0, DateTimeKind.Local), 8, 5 },
                    { 349, 728, new DateTime(2022, 4, 19, 17, 58, 0, 0, DateTimeKind.Local), 2, 4 },
                    { 350, 934, new DateTime(2022, 4, 3, 20, 16, 0, 0, DateTimeKind.Local), 6, 4 },
                    { 351, 979, new DateTime(2022, 4, 19, 7, 23, 0, 0, DateTimeKind.Local), 9, 4 },
                    { 352, 1147, new DateTime(2022, 4, 29, 3, 17, 0, 0, DateTimeKind.Local), 4, 3 },
                    { 353, 893, new DateTime(2022, 5, 4, 9, 34, 0, 0, DateTimeKind.Local), 1, 3 },
                    { 354, 751, new DateTime(2022, 3, 30, 0, 20, 0, 0, DateTimeKind.Local), 6, 5 },
                    { 355, 625, new DateTime(2022, 4, 29, 8, 27, 0, 0, DateTimeKind.Local), 6, 3 },
                    { 356, 701, new DateTime(2022, 4, 13, 5, 6, 0, 0, DateTimeKind.Local), 5, 3 },
                    { 357, 1042, new DateTime(2022, 4, 11, 23, 46, 0, 0, DateTimeKind.Local), 2, 2 },
                    { 358, 1003, new DateTime(2022, 4, 26, 3, 37, 0, 0, DateTimeKind.Local), 13, 1 },
                    { 359, 613, new DateTime(2022, 4, 20, 16, 55, 0, 0, DateTimeKind.Local), 8, 3 },
                    { 360, 537, new DateTime(2022, 5, 13, 7, 52, 0, 0, DateTimeKind.Local), 13, 1 },
                    { 361, 468, new DateTime(2022, 4, 2, 4, 1, 0, 0, DateTimeKind.Local), 2, 5 },
                    { 362, 1192, new DateTime(2022, 5, 16, 15, 44, 0, 0, DateTimeKind.Local), 5, 1 },
                    { 363, 1050, new DateTime(2022, 4, 1, 0, 5, 0, 0, DateTimeKind.Local), 3, 3 },
                    { 364, 1036, new DateTime(2022, 4, 10, 15, 2, 0, 0, DateTimeKind.Local), 7, 2 },
                    { 365, 1092, new DateTime(2022, 4, 27, 5, 22, 0, 0, DateTimeKind.Local), 10, 1 },
                    { 366, 1168, new DateTime(2022, 4, 5, 3, 43, 0, 0, DateTimeKind.Local), 8, 5 },
                    { 367, 497, new DateTime(2022, 5, 16, 21, 11, 0, 0, DateTimeKind.Local), 3, 2 },
                    { 368, 998, new DateTime(2022, 4, 21, 18, 49, 0, 0, DateTimeKind.Local), 7, 4 },
                    { 369, 1153, new DateTime(2022, 5, 20, 3, 38, 0, 0, DateTimeKind.Local), 5, 2 },
                    { 370, 582, new DateTime(2022, 5, 9, 22, 36, 0, 0, DateTimeKind.Local), 8, 1 },
                    { 371, 1061, new DateTime(2022, 5, 21, 5, 15, 0, 0, DateTimeKind.Local), 8, 5 },
                    { 372, 1040, new DateTime(2022, 5, 10, 11, 3, 0, 0, DateTimeKind.Local), 13, 2 },
                    { 373, 444, new DateTime(2022, 4, 21, 11, 16, 0, 0, DateTimeKind.Local), 8, 1 },
                    { 374, 1167, new DateTime(2022, 4, 8, 5, 33, 0, 0, DateTimeKind.Local), 10, 5 },
                    { 375, 777, new DateTime(2022, 4, 20, 2, 6, 0, 0, DateTimeKind.Local), 9, 1 },
                    { 376, 500, new DateTime(2022, 4, 6, 5, 5, 0, 0, DateTimeKind.Local), 13, 1 },
                    { 377, 969, new DateTime(2022, 4, 15, 16, 4, 0, 0, DateTimeKind.Local), 6, 2 },
                    { 378, 901, new DateTime(2022, 5, 22, 6, 19, 0, 0, DateTimeKind.Local), 12, 2 }
                });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "Calories", "Date", "FoodId", "UserId" },
                values: new object[,]
                {
                    { 379, 465, new DateTime(2022, 4, 4, 8, 12, 0, 0, DateTimeKind.Local), 13, 1 },
                    { 380, 959, new DateTime(2022, 5, 14, 16, 49, 0, 0, DateTimeKind.Local), 10, 3 },
                    { 381, 724, new DateTime(2022, 4, 4, 4, 43, 0, 0, DateTimeKind.Local), 11, 5 },
                    { 382, 558, new DateTime(2022, 5, 20, 0, 55, 0, 0, DateTimeKind.Local), 7, 3 },
                    { 383, 771, new DateTime(2022, 5, 19, 10, 53, 0, 0, DateTimeKind.Local), 13, 4 },
                    { 384, 1142, new DateTime(2022, 3, 30, 1, 53, 0, 0, DateTimeKind.Local), 3, 3 },
                    { 385, 719, new DateTime(2022, 5, 3, 17, 58, 0, 0, DateTimeKind.Local), 3, 4 },
                    { 386, 532, new DateTime(2022, 5, 5, 22, 31, 0, 0, DateTimeKind.Local), 2, 3 },
                    { 387, 647, new DateTime(2022, 4, 1, 13, 16, 0, 0, DateTimeKind.Local), 4, 3 },
                    { 388, 989, new DateTime(2022, 4, 29, 18, 5, 0, 0, DateTimeKind.Local), 9, 3 },
                    { 389, 599, new DateTime(2022, 4, 11, 10, 43, 0, 0, DateTimeKind.Local), 12, 2 },
                    { 390, 930, new DateTime(2022, 4, 26, 4, 23, 0, 0, DateTimeKind.Local), 6, 3 },
                    { 391, 519, new DateTime(2022, 4, 15, 20, 25, 0, 0, DateTimeKind.Local), 7, 5 },
                    { 392, 726, new DateTime(2022, 4, 10, 18, 25, 0, 0, DateTimeKind.Local), 3, 4 },
                    { 393, 1001, new DateTime(2022, 5, 10, 7, 6, 0, 0, DateTimeKind.Local), 6, 3 },
                    { 394, 875, new DateTime(2022, 4, 4, 16, 34, 0, 0, DateTimeKind.Local), 5, 5 },
                    { 395, 653, new DateTime(2022, 5, 9, 17, 15, 0, 0, DateTimeKind.Local), 1, 5 },
                    { 396, 1009, new DateTime(2022, 5, 11, 12, 19, 0, 0, DateTimeKind.Local), 11, 2 },
                    { 397, 819, new DateTime(2022, 4, 4, 8, 57, 0, 0, DateTimeKind.Local), 11, 1 },
                    { 398, 691, new DateTime(2022, 3, 29, 6, 20, 0, 0, DateTimeKind.Local), 6, 2 },
                    { 399, 697, new DateTime(2022, 5, 24, 15, 13, 0, 0, DateTimeKind.Local), 4, 1 },
                    { 400, 1074, new DateTime(2022, 4, 13, 0, 35, 0, 0, DateTimeKind.Local), 9, 2 },
                    { 401, 514, new DateTime(2022, 4, 3, 14, 9, 0, 0, DateTimeKind.Local), 1, 1 },
                    { 402, 1154, new DateTime(2022, 4, 12, 2, 56, 0, 0, DateTimeKind.Local), 4, 4 },
                    { 403, 1016, new DateTime(2022, 5, 21, 8, 53, 0, 0, DateTimeKind.Local), 13, 5 },
                    { 404, 1088, new DateTime(2022, 4, 6, 23, 11, 0, 0, DateTimeKind.Local), 5, 4 },
                    { 405, 1113, new DateTime(2022, 5, 14, 23, 23, 0, 0, DateTimeKind.Local), 9, 4 },
                    { 406, 457, new DateTime(2022, 3, 28, 12, 36, 0, 0, DateTimeKind.Local), 12, 1 },
                    { 407, 721, new DateTime(2022, 5, 22, 12, 46, 0, 0, DateTimeKind.Local), 2, 4 },
                    { 408, 1081, new DateTime(2022, 3, 27, 4, 37, 0, 0, DateTimeKind.Local), 3, 4 },
                    { 409, 1007, new DateTime(2022, 4, 1, 6, 35, 0, 0, DateTimeKind.Local), 12, 2 },
                    { 410, 875, new DateTime(2022, 3, 30, 10, 13, 0, 0, DateTimeKind.Local), 10, 5 },
                    { 411, 1081, new DateTime(2022, 4, 21, 1, 37, 0, 0, DateTimeKind.Local), 7, 4 },
                    { 412, 698, new DateTime(2022, 5, 22, 22, 13, 0, 0, DateTimeKind.Local), 2, 1 },
                    { 413, 437, new DateTime(2022, 5, 4, 8, 52, 0, 0, DateTimeKind.Local), 12, 4 },
                    { 414, 898, new DateTime(2022, 4, 20, 8, 10, 0, 0, DateTimeKind.Local), 3, 1 },
                    { 415, 575, new DateTime(2022, 4, 25, 6, 16, 0, 0, DateTimeKind.Local), 5, 4 },
                    { 416, 625, new DateTime(2022, 4, 17, 0, 37, 0, 0, DateTimeKind.Local), 13, 4 },
                    { 417, 690, new DateTime(2022, 5, 19, 0, 18, 0, 0, DateTimeKind.Local), 7, 3 },
                    { 418, 820, new DateTime(2022, 5, 17, 21, 2, 0, 0, DateTimeKind.Local), 9, 5 },
                    { 419, 405, new DateTime(2022, 4, 3, 5, 51, 0, 0, DateTimeKind.Local), 11, 1 },
                    { 420, 1192, new DateTime(2022, 5, 11, 18, 35, 0, 0, DateTimeKind.Local), 8, 1 }
                });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "Calories", "Date", "FoodId", "UserId" },
                values: new object[,]
                {
                    { 421, 813, new DateTime(2022, 4, 1, 0, 54, 0, 0, DateTimeKind.Local), 11, 3 },
                    { 422, 1011, new DateTime(2022, 4, 23, 5, 39, 0, 0, DateTimeKind.Local), 13, 5 },
                    { 423, 502, new DateTime(2022, 5, 17, 3, 29, 0, 0, DateTimeKind.Local), 12, 3 },
                    { 424, 608, new DateTime(2022, 5, 24, 9, 24, 0, 0, DateTimeKind.Local), 11, 1 },
                    { 425, 603, new DateTime(2022, 5, 5, 14, 42, 0, 0, DateTimeKind.Local), 10, 5 },
                    { 426, 422, new DateTime(2022, 3, 26, 12, 58, 0, 0, DateTimeKind.Local), 4, 3 },
                    { 427, 1027, new DateTime(2022, 5, 18, 7, 42, 0, 0, DateTimeKind.Local), 11, 4 },
                    { 428, 949, new DateTime(2022, 3, 31, 5, 20, 0, 0, DateTimeKind.Local), 6, 4 },
                    { 429, 1192, new DateTime(2022, 3, 27, 7, 28, 0, 0, DateTimeKind.Local), 13, 4 },
                    { 430, 792, new DateTime(2022, 5, 17, 2, 2, 0, 0, DateTimeKind.Local), 1, 4 },
                    { 431, 503, new DateTime(2022, 5, 16, 23, 59, 0, 0, DateTimeKind.Local), 8, 1 },
                    { 432, 1139, new DateTime(2022, 3, 26, 2, 18, 0, 0, DateTimeKind.Local), 10, 2 },
                    { 433, 1197, new DateTime(2022, 5, 18, 21, 58, 0, 0, DateTimeKind.Local), 7, 4 },
                    { 434, 440, new DateTime(2022, 4, 22, 15, 49, 0, 0, DateTimeKind.Local), 5, 5 },
                    { 435, 515, new DateTime(2022, 5, 14, 3, 23, 0, 0, DateTimeKind.Local), 1, 4 },
                    { 436, 1173, new DateTime(2022, 4, 29, 3, 7, 0, 0, DateTimeKind.Local), 2, 2 },
                    { 437, 930, new DateTime(2022, 4, 27, 14, 28, 0, 0, DateTimeKind.Local), 5, 4 },
                    { 438, 886, new DateTime(2022, 3, 26, 19, 30, 0, 0, DateTimeKind.Local), 7, 3 },
                    { 439, 497, new DateTime(2022, 5, 24, 20, 9, 0, 0, DateTimeKind.Local), 13, 4 },
                    { 440, 1032, new DateTime(2022, 4, 28, 20, 56, 0, 0, DateTimeKind.Local), 5, 5 },
                    { 441, 689, new DateTime(2022, 4, 29, 13, 35, 0, 0, DateTimeKind.Local), 5, 2 },
                    { 442, 791, new DateTime(2022, 4, 22, 17, 42, 0, 0, DateTimeKind.Local), 13, 1 },
                    { 443, 839, new DateTime(2022, 5, 6, 17, 5, 0, 0, DateTimeKind.Local), 13, 1 },
                    { 444, 752, new DateTime(2022, 5, 10, 11, 35, 0, 0, DateTimeKind.Local), 9, 4 },
                    { 445, 742, new DateTime(2022, 3, 31, 14, 35, 0, 0, DateTimeKind.Local), 13, 2 },
                    { 446, 600, new DateTime(2022, 5, 17, 12, 22, 0, 0, DateTimeKind.Local), 6, 5 },
                    { 447, 427, new DateTime(2022, 5, 18, 4, 55, 0, 0, DateTimeKind.Local), 7, 4 },
                    { 448, 1115, new DateTime(2022, 4, 5, 9, 18, 0, 0, DateTimeKind.Local), 9, 1 },
                    { 449, 877, new DateTime(2022, 4, 4, 15, 25, 0, 0, DateTimeKind.Local), 3, 2 },
                    { 450, 1065, new DateTime(2022, 5, 5, 20, 59, 0, 0, DateTimeKind.Local), 3, 1 },
                    { 451, 1125, new DateTime(2022, 4, 1, 19, 31, 0, 0, DateTimeKind.Local), 11, 5 },
                    { 452, 1156, new DateTime(2022, 4, 18, 2, 18, 0, 0, DateTimeKind.Local), 2, 1 },
                    { 453, 415, new DateTime(2022, 3, 26, 0, 8, 0, 0, DateTimeKind.Local), 8, 4 },
                    { 454, 904, new DateTime(2022, 5, 19, 12, 22, 0, 0, DateTimeKind.Local), 7, 5 },
                    { 455, 1125, new DateTime(2022, 5, 18, 20, 23, 0, 0, DateTimeKind.Local), 5, 1 },
                    { 456, 867, new DateTime(2022, 5, 24, 5, 15, 0, 0, DateTimeKind.Local), 13, 4 },
                    { 457, 1040, new DateTime(2022, 4, 13, 22, 44, 0, 0, DateTimeKind.Local), 10, 4 },
                    { 458, 786, new DateTime(2022, 5, 3, 17, 28, 0, 0, DateTimeKind.Local), 10, 2 },
                    { 459, 770, new DateTime(2022, 4, 22, 8, 1, 0, 0, DateTimeKind.Local), 8, 4 },
                    { 460, 1025, new DateTime(2022, 4, 1, 12, 1, 0, 0, DateTimeKind.Local), 5, 2 },
                    { 461, 531, new DateTime(2022, 4, 2, 5, 31, 0, 0, DateTimeKind.Local), 11, 1 },
                    { 462, 693, new DateTime(2022, 3, 31, 3, 14, 0, 0, DateTimeKind.Local), 6, 5 }
                });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "Calories", "Date", "FoodId", "UserId" },
                values: new object[,]
                {
                    { 463, 920, new DateTime(2022, 4, 23, 18, 54, 0, 0, DateTimeKind.Local), 10, 5 },
                    { 464, 1017, new DateTime(2022, 4, 23, 1, 27, 0, 0, DateTimeKind.Local), 5, 4 },
                    { 465, 1171, new DateTime(2022, 5, 13, 9, 26, 0, 0, DateTimeKind.Local), 6, 1 },
                    { 466, 1181, new DateTime(2022, 5, 11, 13, 19, 0, 0, DateTimeKind.Local), 11, 3 },
                    { 467, 1185, new DateTime(2022, 4, 30, 2, 6, 0, 0, DateTimeKind.Local), 1, 1 },
                    { 468, 512, new DateTime(2022, 5, 6, 17, 50, 0, 0, DateTimeKind.Local), 6, 4 },
                    { 469, 717, new DateTime(2022, 4, 7, 8, 27, 0, 0, DateTimeKind.Local), 8, 4 },
                    { 470, 837, new DateTime(2022, 3, 26, 11, 0, 0, 0, DateTimeKind.Local), 3, 5 },
                    { 471, 672, new DateTime(2022, 4, 18, 4, 47, 0, 0, DateTimeKind.Local), 7, 1 },
                    { 472, 668, new DateTime(2022, 4, 28, 0, 9, 0, 0, DateTimeKind.Local), 5, 3 },
                    { 473, 1020, new DateTime(2022, 4, 12, 22, 49, 0, 0, DateTimeKind.Local), 7, 5 },
                    { 474, 1112, new DateTime(2022, 4, 25, 2, 42, 0, 0, DateTimeKind.Local), 12, 1 },
                    { 475, 1077, new DateTime(2022, 5, 18, 16, 25, 0, 0, DateTimeKind.Local), 1, 5 },
                    { 476, 1099, new DateTime(2022, 3, 31, 7, 49, 0, 0, DateTimeKind.Local), 3, 4 },
                    { 477, 833, new DateTime(2022, 3, 30, 4, 35, 0, 0, DateTimeKind.Local), 5, 4 },
                    { 478, 1077, new DateTime(2022, 5, 19, 3, 35, 0, 0, DateTimeKind.Local), 10, 1 },
                    { 479, 473, new DateTime(2022, 5, 20, 18, 24, 0, 0, DateTimeKind.Local), 4, 3 },
                    { 480, 691, new DateTime(2022, 5, 3, 12, 27, 0, 0, DateTimeKind.Local), 8, 5 },
                    { 481, 711, new DateTime(2022, 5, 16, 9, 46, 0, 0, DateTimeKind.Local), 9, 2 },
                    { 482, 599, new DateTime(2022, 4, 3, 6, 23, 0, 0, DateTimeKind.Local), 7, 2 },
                    { 483, 781, new DateTime(2022, 4, 21, 23, 51, 0, 0, DateTimeKind.Local), 6, 5 },
                    { 484, 977, new DateTime(2022, 4, 29, 1, 3, 0, 0, DateTimeKind.Local), 4, 1 },
                    { 485, 598, new DateTime(2022, 5, 11, 22, 54, 0, 0, DateTimeKind.Local), 4, 5 },
                    { 486, 611, new DateTime(2022, 4, 19, 14, 28, 0, 0, DateTimeKind.Local), 7, 5 },
                    { 487, 1122, new DateTime(2022, 5, 5, 17, 52, 0, 0, DateTimeKind.Local), 7, 4 },
                    { 488, 567, new DateTime(2022, 5, 2, 23, 56, 0, 0, DateTimeKind.Local), 9, 4 },
                    { 489, 1004, new DateTime(2022, 4, 14, 18, 13, 0, 0, DateTimeKind.Local), 11, 4 },
                    { 490, 908, new DateTime(2022, 4, 18, 7, 40, 0, 0, DateTimeKind.Local), 2, 3 },
                    { 491, 444, new DateTime(2022, 4, 19, 9, 44, 0, 0, DateTimeKind.Local), 7, 5 },
                    { 492, 584, new DateTime(2022, 4, 14, 13, 39, 0, 0, DateTimeKind.Local), 7, 1 },
                    { 493, 605, new DateTime(2022, 5, 6, 3, 3, 0, 0, DateTimeKind.Local), 5, 1 },
                    { 494, 723, new DateTime(2022, 5, 6, 6, 0, 0, 0, DateTimeKind.Local), 4, 1 },
                    { 495, 947, new DateTime(2022, 4, 4, 23, 51, 0, 0, DateTimeKind.Local), 1, 2 },
                    { 496, 1145, new DateTime(2022, 4, 8, 20, 45, 0, 0, DateTimeKind.Local), 10, 1 },
                    { 497, 1043, new DateTime(2022, 5, 11, 9, 43, 0, 0, DateTimeKind.Local), 2, 2 },
                    { 498, 1157, new DateTime(2022, 5, 11, 22, 5, 0, 0, DateTimeKind.Local), 9, 1 },
                    { 499, 844, new DateTime(2022, 4, 22, 14, 23, 0, 0, DateTimeKind.Local), 7, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Meals_FoodId",
                table: "Meals",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_UserId",
                table: "Meals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCheatingFoods_FoodId",
                table: "UserCheatingFoods",
                column: "FoodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "UserCheatingFoods");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
