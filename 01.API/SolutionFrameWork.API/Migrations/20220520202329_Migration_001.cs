using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolutionFrameWork.API.Migrations
{
    public partial class Migration_001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    IdUserCreator = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Description = table.Column<string>(type: "varchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    IdUserCreator = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "varchar(250)", nullable: false),
                    Nit = table.Column<string>(type: "varchar(20)", nullable: true),
                    LegalRepresentative = table.Column<string>(type: "varchar(70)", nullable: false),
                    Address = table.Column<string>(type: "varchar(50)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", nullable: false),
                    Fax = table.Column<string>(type: "varchar(20)", nullable: true),
                    City = table.Column<string>(type: "varchar(30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    IdUserCreator = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryName = table.Column<string>(type: "varchar(50)", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", nullable: true),
                    TrackingDays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    IdUserCreator = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Category = table.Column<string>(type: "varchar(50)", nullable: false),
                    HasTracking = table.Column<bool>(type: "bit", nullable: false),
                    RolResponsible = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Duration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    IdUserCreator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Period = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    IdUserCreator = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Code = table.Column<string>(type: "varchar(20)", nullable: false),
                    Content = table.Column<byte[]>(type: "image", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HolyDays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    IdUserCreator = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Holyday = table.Column<DateTime>(type: "datetime", nullable: false),
                    Description = table.Column<string>(type: "varchar(150)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolyDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    IdUserCreator = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdParent = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Title = table.Column<string>(type: "varchar(100)", nullable: false),
                    Icon = table.Column<string>(type: "varchar(100)", nullable: true),
                    Route = table.Column<string>(type: "varchar(250)", nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: false),
                    Module = table.Column<string>(type: "varchar(100)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OptionList",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    IdUserCreator = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Key = table.Column<string>(type: "varchar(100)", nullable: false),
                    Value = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    Description = table.Column<string>(type: "varchar(250)", nullable: true),
                    FieldType = table.Column<string>(type: "varchar(50)", nullable: true),
                    Private = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    IdUserCreator = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "varchar(150)", nullable: false),
                    BusinessName = table.Column<string>(type: "varchar(250)", nullable: false),
                    ClientCode = table.Column<string>(type: "varchar(30)", nullable: false),
                    Nit = table.Column<string>(type: "varchar(20)", nullable: false),
                    Address = table.Column<string>(type: "varchar(250)", nullable: false),
                    City = table.Column<string>(type: "varchar(50)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", nullable: false),
                    ContactName = table.Column<string>(type: "varchar(100)", nullable: true),
                    EmailAddress = table.Column<string>(type: "varchar(50)", nullable: false),
                    LegalRepresentative = table.Column<string>(type: "varchar(100)", nullable: false),
                    IdLegalRepresentative = table.Column<string>(type: "varchar(20)", nullable: false),
                    EmailLegalRepresentative = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    IdUserCreator = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Description = table.Column<string>(type: "varchar(250)", nullable: false),
                    IdManager = table.Column<string>(type: "varchar(128)", nullable: true),
                    IdCompany = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApprovalLimitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Areas_Companies_IdCompany",
                        column: x => x.IdCompany,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    IdUserCreator = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdContractType = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemplateName = table.Column<string>(type: "varchar(250)", nullable: false),
                    Content = table.Column<byte[]>(type: "image", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractTemplates_ContractTypes_IdContractType",
                        column: x => x.IdContractType,
                        principalTable: "ContractTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DurationDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    IdUserCreator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdDuration = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DurationDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DurationDetail_Duration_DurationId",
                        column: x => x.DurationId,
                        principalTable: "Duration",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OptionListDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    IdUserCreator = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdOptionList = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "varchar(100)", nullable: false),
                    Value = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionListDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OptionListDetail_OptionList_IdOptionList",
                        column: x => x.IdOptionList,
                        principalTable: "OptionList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApproverMatrix",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    IdUserCreator = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdArea = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdFirstApprover = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdSecondApprover = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApproverMatrix", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApproverMatrix_Areas_IdArea",
                        column: x => x.IdArea,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApproverMatrix_IdArea",
                table: "ApproverMatrix",
                column: "IdArea");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_IdCompany",
                table: "Areas",
                column: "IdCompany");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTemplates_IdContractType",
                table: "ContractTemplates",
                column: "IdContractType");

            migrationBuilder.CreateIndex(
                name: "IX_DurationDetail_DurationId",
                table: "DurationDetail",
                column: "DurationId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionListDetail_IdOptionList",
                table: "OptionListDetail",
                column: "IdOptionList");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "ApproverMatrix");

            migrationBuilder.DropTable(
                name: "ContractTemplates");

            migrationBuilder.DropTable(
                name: "DocumentTypes");

            migrationBuilder.DropTable(
                name: "DurationDetail");

            migrationBuilder.DropTable(
                name: "EmailTemplates");

            migrationBuilder.DropTable(
                name: "HolyDays");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "OptionListDetail");

            migrationBuilder.DropTable(
                name: "Providers");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "ContractTypes");

            migrationBuilder.DropTable(
                name: "Duration");

            migrationBuilder.DropTable(
                name: "OptionList");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
