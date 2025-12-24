using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartCraft.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "smartcraft");

            migrationBuilder.CreateSequence(
                name: "company_seq",
                schema: "smartcraft",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "contacts_seq",
                schema: "smartcraft",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "interactions_seq",
                schema: "smartcraft",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "company",
                schema: "smartcraft",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    industry = table.Column<string>(type: "text", nullable: false),
                    website = table.Column<string>(type: "text", nullable: false),
                    size = table.Column<string>(type: "text", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_company", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "contacts",
                schema: "smartcraft",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    company_id = table.Column<int>(type: "integer", nullable: true),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: false),
                    job_title = table.Column<string>(type: "text", nullable: false),
                    last_contacted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contacts", x => x.id);
                    table.ForeignKey(
                        name: "fk_contacts_companies_company_id",
                        column: x => x.company_id,
                        principalSchema: "smartcraft",
                        principalTable: "company",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "interactions",
                schema: "smartcraft",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    contact_id = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: false),
                    subject = table.Column<string>(type: "text", nullable: false),
                    outcome = table.Column<string>(type: "text", nullable: false),
                    location = table.Column<string>(type: "text", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_interactions", x => x.id);
                    table.ForeignKey(
                        name: "fk_interactions_contacts_contact_id",
                        column: x => x.contact_id,
                        principalSchema: "smartcraft",
                        principalTable: "contacts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_contacts_company_id",
                schema: "smartcraft",
                table: "contacts",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ix_interactions_contact_id",
                schema: "smartcraft",
                table: "interactions",
                column: "contact_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "interactions",
                schema: "smartcraft");

            migrationBuilder.DropTable(
                name: "contacts",
                schema: "smartcraft");

            migrationBuilder.DropTable(
                name: "company",
                schema: "smartcraft");

            migrationBuilder.DropSequence(
                name: "company_seq",
                schema: "smartcraft");

            migrationBuilder.DropSequence(
                name: "contacts_seq",
                schema: "smartcraft");

            migrationBuilder.DropSequence(
                name: "interactions_seq",
                schema: "smartcraft");
        }
    }
}
