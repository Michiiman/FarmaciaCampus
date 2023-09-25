using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "rol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 3, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rol", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tipoDocumento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipoDocumento", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TipoPersona",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tipoPersona = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPersona", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoDeDocumentoIdFk = table.Column<int>(type: "int", nullable: false),
                    NumeroDocumento = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Direccion = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoPersonaIdFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persona_TipoPersona_TipoPersonaIdFk",
                        column: x => x.TipoPersonaIdFk,
                        principalTable: "TipoPersona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Persona_tipoDocumento_TipoDeDocumentoIdFk",
                        column: x => x.TipoDeDocumentoIdFk,
                        principalTable: "tipoDocumento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "compra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FechaCompra = table.Column<DateTime>(type: "date", nullable: false),
                    ProveedorIdFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_compra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_compra_Persona_ProveedorIdFk",
                        column: x => x.ProveedorIdFk,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Medicamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Precio = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    FechaExpiracion = table.Column<DateTime>(type: "date", nullable: false),
                    TipoMedicamento = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProveedorIdFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicamento_Persona_ProveedorIdFk",
                        column: x => x.ProveedorIdFk,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Receta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FechaExpedicion = table.Column<DateTime>(type: "date", nullable: false),
                    PacienteIdFk = table.Column<int>(type: "int", nullable: false),
                    DoctorIdFk = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receta_Persona_DoctorIdFk",
                        column: x => x.DoctorIdFk,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receta_Persona_PacienteIdFk",
                        column: x => x.PacienteIdFk,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Telefono",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PersonaIdFk = table.Column<int>(type: "int", nullable: false),
                    TipoTelefono = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Numero = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefono", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Telefono_Persona_PersonaIdFk",
                        column: x => x.PersonaIdFk,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 3, nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmpleadoIdfk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_Persona_EmpleadoIdfk",
                        column: x => x.EmpleadoIdfk,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MedicamentoComprado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CompraIdFk = table.Column<int>(type: "int", nullable: false),
                    MedicamentoIdFk = table.Column<int>(type: "int", nullable: false),
                    CantidadComprada = table.Column<int>(type: "int", nullable: false),
                    PrecioCompra = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicamentoComprado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicamentoComprado_Medicamento_MedicamentoIdFk",
                        column: x => x.MedicamentoIdFk,
                        principalTable: "Medicamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicamentoComprado_compra_CompraIdFk",
                        column: x => x.CompraIdFk,
                        principalTable: "compra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FacturaVenta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FechaFactura = table.Column<DateTime>(type: "date", nullable: false),
                    PacienteIdFk = table.Column<int>(type: "int", nullable: false),
                    EmpleadoIdFk = table.Column<int>(type: "int", nullable: false),
                    RecetaIdFk = table.Column<int>(type: "int", nullable: false),
                    PrecioTotal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturaVenta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacturaVenta_Persona_EmpleadoIdFk",
                        column: x => x.EmpleadoIdFk,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacturaVenta_Persona_PacienteIdFk",
                        column: x => x.PacienteIdFk,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacturaVenta_Receta_RecetaIdFk",
                        column: x => x.RecetaIdFk,
                        principalTable: "Receta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MedicamentosReceta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MedicamentosIdfk = table.Column<int>(type: "int", nullable: false),
                    RecetaIdFk = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicamentosReceta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicamentosReceta_Medicamento_MedicamentosIdfk",
                        column: x => x.MedicamentosIdfk,
                        principalTable: "Medicamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicamentosReceta_Receta_RecetaIdFk",
                        column: x => x.RecetaIdFk,
                        principalTable: "Receta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "userRol",
                columns: table => new
                {
                    UserIdFk = table.Column<int>(type: "int", nullable: false),
                    RolIdFk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userRol", x => new { x.UserIdFk, x.RolIdFk });
                    table.ForeignKey(
                        name: "FK_userRol_rol_RolIdFk",
                        column: x => x.RolIdFk,
                        principalTable: "rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userRol_user_UserIdFk",
                        column: x => x.UserIdFk,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MedicamentoVendido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FacturaVentaIdFk = table.Column<int>(type: "int", nullable: false),
                    MedicamentoIdFk = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicamentoVendido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicamentoVendido_FacturaVenta_FacturaVentaIdFk",
                        column: x => x.FacturaVentaIdFk,
                        principalTable: "FacturaVenta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicamentoVendido_Medicamento_MedicamentoIdFk",
                        column: x => x.MedicamentoIdFk,
                        principalTable: "Medicamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_compra_ProveedorIdFk",
                table: "compra",
                column: "ProveedorIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaVenta_EmpleadoIdFk",
                table: "FacturaVenta",
                column: "EmpleadoIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaVenta_PacienteIdFk",
                table: "FacturaVenta",
                column: "PacienteIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaVenta_RecetaIdFk",
                table: "FacturaVenta",
                column: "RecetaIdFk",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medicamento_ProveedorIdFk",
                table: "Medicamento",
                column: "ProveedorIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_MedicamentoComprado_CompraIdFk",
                table: "MedicamentoComprado",
                column: "CompraIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_MedicamentoComprado_MedicamentoIdFk",
                table: "MedicamentoComprado",
                column: "MedicamentoIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_MedicamentosReceta_MedicamentosIdfk",
                table: "MedicamentosReceta",
                column: "MedicamentosIdfk");

            migrationBuilder.CreateIndex(
                name: "IX_MedicamentosReceta_RecetaIdFk",
                table: "MedicamentosReceta",
                column: "RecetaIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_MedicamentoVendido_FacturaVentaIdFk",
                table: "MedicamentoVendido",
                column: "FacturaVentaIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_MedicamentoVendido_MedicamentoIdFk",
                table: "MedicamentoVendido",
                column: "MedicamentoIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_TipoDeDocumentoIdFk",
                table: "Persona",
                column: "TipoDeDocumentoIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_Persona_TipoPersonaIdFk",
                table: "Persona",
                column: "TipoPersonaIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_Receta_DoctorIdFk",
                table: "Receta",
                column: "DoctorIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_Receta_PacienteIdFk",
                table: "Receta",
                column: "PacienteIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_Telefono_PersonaIdFk",
                table: "Telefono",
                column: "PersonaIdFk");

            migrationBuilder.CreateIndex(
                name: "IX_user_EmpleadoIdfk",
                table: "user",
                column: "EmpleadoIdfk",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_userRol_RolIdFk",
                table: "userRol",
                column: "RolIdFk");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicamentoComprado");

            migrationBuilder.DropTable(
                name: "MedicamentosReceta");

            migrationBuilder.DropTable(
                name: "MedicamentoVendido");

            migrationBuilder.DropTable(
                name: "Telefono");

            migrationBuilder.DropTable(
                name: "userRol");

            migrationBuilder.DropTable(
                name: "compra");

            migrationBuilder.DropTable(
                name: "FacturaVenta");

            migrationBuilder.DropTable(
                name: "Medicamento");

            migrationBuilder.DropTable(
                name: "rol");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "Receta");

            migrationBuilder.DropTable(
                name: "Persona");

            migrationBuilder.DropTable(
                name: "TipoPersona");

            migrationBuilder.DropTable(
                name: "tipoDocumento");
        }
    }
}
