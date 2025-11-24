using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RRHH.Migrations
{
    /// <inheritdoc />
    public partial class M1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    DepartamentoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.DepartamentoId);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    EmailId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Correo = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.EmailId);
                });

            migrationBuilder.CreateTable(
                name: "EmpleadoCurriculums",
                columns: table => new
                {
                    CurriculumId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmpleadoId = table.Column<int>(type: "integer", nullable: false),
                    Cod_Emp = table.Column<string>(type: "text", nullable: false),
                    Perfil = table.Column<string>(type: "text", nullable: false),
                    FechaRecepcion = table.Column<DateTime>(type: "date", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpleadoCurriculums", x => x.CurriculumId);
                });

            migrationBuilder.CreateTable(
                name: "EmpleadosActivosViews",
                columns: table => new
                {
                    CodigoEmpleado = table.Column<string>(type: "text", nullable: false),
                    CI = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "text", nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "text", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "date", nullable: false),
                    Sexo = table.Column<string>(type: "text", nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "EmpleadosSalariosPuestosViews",
                columns: table => new
                {
                    EmpleadoId = table.Column<int>(type: "integer", nullable: false),
                    CodigoEmpleado = table.Column<string>(type: "text", nullable: false),
                    NombreEmpleado = table.Column<string>(type: "text", nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "text", nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "text", nullable: false),
                    SalarioBase = table.Column<decimal>(type: "numeric", nullable: false),
                    NombrePuesto = table.Column<string>(type: "text", nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "date", nullable: false),
                    EstadoEmpleado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "HistorialDepartamentos",
                columns: table => new
                {
                    EmpleadoId = table.Column<int>(type: "integer", nullable: false),
                    DepartamentoId = table.Column<int>(type: "integer", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    PuestoId = table.Column<int>(type: "integer", nullable: false),
                    Codigo_Emp = table.Column<string>(type: "text", nullable: false),
                    Codigo_Puesto = table.Column<string>(type: "text", nullable: false),
                    Codigo_Dep = table.Column<string>(type: "text", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: true),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialDepartamentos", x => new { x.EmpleadoId, x.DepartamentoId, x.FechaInicio });
                });

            migrationBuilder.CreateTable(
                name: "HistorialDepartamentosViews",
                columns: table => new
                {
                    EmpleadoId = table.Column<int>(type: "integer", nullable: false),
                    CodigoEmpleado = table.Column<string>(type: "text", nullable: false),
                    CodigoDepartamento = table.Column<string>(type: "text", nullable: false),
                    NombreDepartamento = table.Column<string>(type: "text", nullable: false),
                    CodigoPuesto = table.Column<string>(type: "text", nullable: false),
                    NombrePuesto = table.Column<string>(type: "text", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: true),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Nominas",
                columns: table => new
                {
                    NominaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cod_Nom = table.Column<string>(type: "text", nullable: false),
                    EmpleadoId = table.Column<int>(type: "integer", nullable: false),
                    Cod_Emp = table.Column<string>(type: "text", nullable: false),
                    PeriodoInicio = table.Column<DateTime>(type: "date", nullable: false),
                    PeriodoFin = table.Column<DateTime>(type: "date", nullable: false),
                    SalarioBase = table.Column<decimal>(type: "numeric", nullable: false),
                    Bonos = table.Column<decimal>(type: "numeric", nullable: false),
                    Descuentos = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalNeto = table.Column<decimal>(type: "numeric", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nominas", x => x.NominaId);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    PersonaId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CI = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "text", nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "text", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "date", nullable: false),
                    Sexo = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.PersonaId);
                });

            migrationBuilder.CreateTable(
                name: "Puestos",
                columns: table => new
                {
                    PuestoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Puestos", x => x.PuestoId);
                });

            migrationBuilder.CreateTable(
                name: "ReportesEmpleados",
                columns: table => new
                {
                    ReporteId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmpleadoReportadoId = table.Column<int>(type: "integer", nullable: false),
                    DepartamentoEmisorId = table.Column<int>(type: "integer", nullable: false),
                    Cod_Emp = table.Column<string>(type: "text", nullable: false),
                    Cod_Dep = table.Column<string>(type: "text", nullable: false),
                    Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportesEmpleados", x => x.ReporteId);
                });

            migrationBuilder.CreateTable(
                name: "ReportesEmpleadosViews",
                columns: table => new
                {
                    ReporteId = table.Column<int>(type: "integer", nullable: false),
                    CodigoEmpleadoReportado = table.Column<string>(type: "text", nullable: false),
                    CodigoDepartamentoEmisor = table.Column<string>(type: "text", nullable: false),
                    Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    EstadoReporte = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "ResumenNominaEmpleadoViews",
                columns: table => new
                {
                    NominaId = table.Column<int>(type: "integer", nullable: false),
                    CodigoEmpleado = table.Column<string>(type: "text", nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "date", nullable: false),
                    PeriodoInicio = table.Column<DateTime>(type: "date", nullable: false),
                    PeriodoFin = table.Column<DateTime>(type: "date", nullable: false),
                    SalarioBase = table.Column<decimal>(type: "numeric", nullable: false),
                    Bonos = table.Column<decimal>(type: "numeric", nullable: false),
                    Descuentos = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalNeto = table.Column<decimal>(type: "numeric", nullable: false),
                    EstadoNomina = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Telefonos",
                columns: table => new
                {
                    TelefonoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Numero = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefonos", x => x.TelefonoId);
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    EmpleadoId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "text", nullable: false),
                    PersonaId = table.Column<int>(type: "integer", nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "date", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.EmpleadoId);
                    table.ForeignKey(
                        name: "FK_Empleados_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonaEmails",
                columns: table => new
                {
                    PersonaId = table.Column<int>(type: "integer", nullable: false),
                    EmailId = table.Column<int>(type: "integer", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: true),
                    EsPrincipal = table.Column<bool>(type: "boolean", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaEmails", x => new { x.PersonaId, x.EmailId, x.FechaInicio });
                    table.ForeignKey(
                        name: "FK_PersonaEmails_Emails_EmailId",
                        column: x => x.EmailId,
                        principalTable: "Emails",
                        principalColumn: "EmailId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonaEmails_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonaTelefonos",
                columns: table => new
                {
                    PersonaId = table.Column<int>(type: "integer", nullable: false),
                    TelefonoId = table.Column<int>(type: "integer", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "date", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "date", nullable: true),
                    EsPrincipal = table.Column<bool>(type: "boolean", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonaTelefonos", x => new { x.PersonaId, x.TelefonoId, x.FechaInicio });
                    table.ForeignKey(
                        name: "FK_PersonaTelefonos_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonaTelefonos_Telefonos_TelefonoId",
                        column: x => x.TelefonoId,
                        principalTable: "Telefonos",
                        principalColumn: "TelefonoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departamentos_Codigo",
                table: "Departamentos",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Emails_Correo",
                table: "Emails",
                column: "Correo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmpleadoCurriculums_Cod_Emp",
                table: "EmpleadoCurriculums",
                column: "Cod_Emp",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_Codigo",
                table: "Empleados",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_PersonaId",
                table: "Empleados",
                column: "PersonaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistorialDepartamentos_EmpleadoId_PuestoId_DepartamentoId_F~",
                table: "HistorialDepartamentos",
                columns: new[] { "EmpleadoId", "PuestoId", "DepartamentoId", "FechaInicio" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nominas_EmpleadoId_PeriodoInicio_PeriodoFin",
                table: "Nominas",
                columns: new[] { "EmpleadoId", "PeriodoInicio", "PeriodoFin" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonaEmails_EmailId",
                table: "PersonaEmails",
                column: "EmailId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonaEmails_PersonaId_EmailId_FechaInicio",
                table: "PersonaEmails",
                columns: new[] { "PersonaId", "EmailId", "FechaInicio" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personas_CI",
                table: "Personas",
                column: "CI",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonaTelefonos_PersonaId_TelefonoId_FechaInicio",
                table: "PersonaTelefonos",
                columns: new[] { "PersonaId", "TelefonoId", "FechaInicio" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonaTelefonos_TelefonoId",
                table: "PersonaTelefonos",
                column: "TelefonoId");

            migrationBuilder.CreateIndex(
                name: "IX_Puestos_Codigo",
                table: "Puestos",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReportesEmpleados_EmpleadoReportadoId_DepartamentoEmisorId_~",
                table: "ReportesEmpleados",
                columns: new[] { "EmpleadoReportadoId", "DepartamentoEmisorId", "Fecha" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departamentos");

            migrationBuilder.DropTable(
                name: "EmpleadoCurriculums");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "EmpleadosActivosViews");

            migrationBuilder.DropTable(
                name: "EmpleadosSalariosPuestosViews");

            migrationBuilder.DropTable(
                name: "HistorialDepartamentos");

            migrationBuilder.DropTable(
                name: "HistorialDepartamentosViews");

            migrationBuilder.DropTable(
                name: "Nominas");

            migrationBuilder.DropTable(
                name: "PersonaEmails");

            migrationBuilder.DropTable(
                name: "PersonaTelefonos");

            migrationBuilder.DropTable(
                name: "Puestos");

            migrationBuilder.DropTable(
                name: "ReportesEmpleados");

            migrationBuilder.DropTable(
                name: "ReportesEmpleadosViews");

            migrationBuilder.DropTable(
                name: "ResumenNominaEmpleadoViews");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropTable(
                name: "Telefonos");
        }
    }
}
