using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InsureTrust.API.Migrations
{
    /// <inheritdoc />
    public partial class InsureTrust : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PolicyTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    BaseMonthlyPremium = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MinTenureMonths = table.Column<int>(type: "int", nullable: false),
                    MaxTenureMonths = table.Column<int>(type: "int", nullable: false),
                    CoverageDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PanCard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KycDocumentPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KycStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PolicyRequiredFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyTypeId = table.Column<int>(type: "int", nullable: false),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FieldType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMandatory = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyRequiredFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyRequiredFields_PolicyTypes_PolicyTypeId",
                        column: x => x.PolicyTypeId,
                        principalTable: "PolicyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PolicyTerms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyTypeId = table.Column<int>(type: "int", nullable: false),
                    TermText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyTerms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyTerms_PolicyTypes_PolicyTypeId",
                        column: x => x.PolicyTypeId,
                        principalTable: "PolicyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ColorCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RelatedFeature = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupportQueries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttachmentPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportQueries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupportQueries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPolicies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PolicyTypeId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tenure = table.Column<int>(type: "int", nullable: false),
                    PackageAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    AdminRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DynamicFieldsJson = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPolicies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPolicies_PolicyTypes_PolicyTypeId",
                        column: x => x.PolicyTypeId,
                        principalTable: "PolicyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPolicies_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserPolicyId = table.Column<int>(type: "int", nullable: false),
                    ClaimStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaimDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaturityAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DocumentsSubmitted = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Claims_UserPolicies_UserPolicyId",
                        column: x => x.UserPolicyId,
                        principalTable: "UserPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserPolicyId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_UserPolicies_UserPolicyId",
                        column: x => x.UserPolicyId,
                        principalTable: "UserPolicies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PolicyTypes",
                columns: new[] { "Id", "BaseMonthlyPremium", "Category", "CoverageDetails", "CreatedAt", "Description", "Icon", "IsActive", "MaxTenureMonths", "MinTenureMonths", "Name" },
                values: new object[,]
                {
                    { 1, 5000m, "Personal", "Death benefit, Terminal illness rider, Accidental death benefit", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Comprehensive life coverage protecting your family's financial future.", "shield", true, 360, 12, "Term Life" },
                    { 2, 3000m, "Personal", "Hospitalization, Day care procedures, Pre & post hospitalization", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Complete medical coverage for hospitalization, surgeries, and outpatient care.", "heart", true, 60, 12, "Health" },
                    { 3, 2000m, "Personal", "Own damage, Third party liability, Personal accident cover", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Full coverage for cars and bikes against accidents, theft, and damage.", "car", true, 36, 12, "Vehicle" },
                    { 4, 1500m, "Personal", "Structure damage, Contents, Liability protection", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Protect your home and belongings against fire, flood, and burglary.", "home", true, 120, 12, "Home" },
                    { 5, 8000m, "Business", "Building damage, Business interruption, Liability", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Commercial property insurance for businesses and investment properties.", "building", true, 120, 12, "Property" },
                    { 6, 15000m, "Business", "Group health, Group life, Disability coverage", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Group insurance plans covering all employees under a single policy.", "users", true, 60, 12, "Employee Group Benefits" },
                    { 7, 12000m, "Business", "Equipment breakdown, Contractors risk, Erection all risks", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Specialized coverage for construction projects, equipment, and machinery.", "settings", true, 60, 12, "Engineering" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Balance", "CreatedAt", "Email", "KycDocumentPath", "KycStatus", "Name", "PanCard", "PasswordHash", "PhoneNo", "Role", "UserNumber" },
                values: new object[] { 1, 0m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin@insuretrust.com", "", "Verified", "System Admin", "ADMIN1234A", "$2a$11$4X5LLa2RkUKBlzu./VhVFecpVV4yhVw6/5pHzzcMmvJetO68wCozq", "9999999999", "Admin", "ADMIN001" });

            migrationBuilder.InsertData(
                table: "PolicyRequiredFields",
                columns: new[] { "Id", "FieldName", "FieldType", "IsMandatory", "PolicyTypeId" },
                values: new object[,]
                {
                    { 1, "Nominee Name", "text", true, 1 },
                    { 2, "Nominee Relation", "text", true, 1 },
                    { 3, "Date of Birth", "date", true, 1 },
                    { 4, "Date of Birth", "date", true, 2 },
                    { 5, "Existing Medical Conditions", "text", false, 2 },
                    { 6, "Vehicle Registration Number", "text", true, 3 },
                    { 7, "Vehicle Make & Model", "text", true, 3 },
                    { 8, "Property Address", "text", true, 4 },
                    { 9, "Property Value (₹)", "text", true, 4 }
                });

            migrationBuilder.InsertData(
                table: "PolicyTerms",
                columns: new[] { "Id", "PolicyTypeId", "TermText" },
                values: new object[,]
                {
                    { 1, 1, "Policy is valid for the tenure period specified at purchase." },
                    { 2, 1, "Premium must be paid monthly without grace period exceeding 30 days." },
                    { 3, 1, "Death claims require submission within 90 days of the event." },
                    { 4, 2, "Pre-existing diseases are covered after a 2-year waiting period." },
                    { 5, 2, "Cashless treatment available at 5000+ network hospitals." },
                    { 6, 3, "Vehicle must be registered in India and in roadworthy condition." },
                    { 7, 3, "Claims must be filed within 7 days of the incident." }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Claims_ClaimNumber",
                table: "Claims",
                column: "ClaimNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Claims_UserPolicyId",
                table: "Claims",
                column: "UserPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentNumber",
                table: "Payments",
                column: "PaymentNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserPolicyId",
                table: "Payments",
                column: "UserPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyRequiredFields_PolicyTypeId",
                table: "PolicyRequiredFields",
                column: "PolicyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyTerms_PolicyTypeId",
                table: "PolicyTerms",
                column: "PolicyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportQueries_TicketNumber",
                table: "SupportQueries",
                column: "TicketNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupportQueries_UserId",
                table: "SupportQueries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPolicies_PolicyNumber",
                table: "UserPolicies",
                column: "PolicyNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPolicies_PolicyTypeId",
                table: "UserPolicies",
                column: "PolicyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPolicies_UserId",
                table: "UserPolicies",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserNumber",
                table: "Users",
                column: "UserNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "PolicyRequiredFields");

            migrationBuilder.DropTable(
                name: "PolicyTerms");

            migrationBuilder.DropTable(
                name: "SupportQueries");

            migrationBuilder.DropTable(
                name: "UserPolicies");

            migrationBuilder.DropTable(
                name: "PolicyTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
