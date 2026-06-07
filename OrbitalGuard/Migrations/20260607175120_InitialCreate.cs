using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrbitalGuard.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_REGIOES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Pais = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Latitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    Longitude = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    AreaKm2 = table.Column<double>(type: "BINARY_DOUBLE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_REGIOES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_SATELITES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    AltitudeKm = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    TipoOrbita = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CoberturaDegraus = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Fabricante = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DataLancamento = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Ativo = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_SATELITES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_LEITURAS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Timestamp = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    TemperaturaC = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    UmidadePercent = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    PressaoHpa = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    VelocidadeVentoKmh = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    IndiceRisco = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    SateliteId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    RegiaoMonitoradaId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_LEITURAS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_LEITURAS_TB_REGIOES_RegiaoMonitoradaId",
                        column: x => x.RegiaoMonitoradaId,
                        principalTable: "TB_REGIOES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_LEITURAS_TB_SATELITES_SateliteId",
                        column: x => x.SateliteId,
                        principalTable: "TB_SATELITES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_ALERTAS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TipoDesastre = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    NivelAlerta = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    DataHoraAlerta = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    Resolvido = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    DataHoraResolucao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    LeituraClimaticaId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    RegiaoMonitoradaId = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ALERTAS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_ALERTAS_TB_LEITURAS_LeituraClimaticaId",
                        column: x => x.LeituraClimaticaId,
                        principalTable: "TB_LEITURAS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_ALERTAS_TB_REGIOES_RegiaoMonitoradaId",
                        column: x => x.RegiaoMonitoradaId,
                        principalTable: "TB_REGIOES",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_ALERTAS_LeituraClimaticaId",
                table: "TB_ALERTAS",
                column: "LeituraClimaticaId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_ALERTAS_RegiaoMonitoradaId",
                table: "TB_ALERTAS",
                column: "RegiaoMonitoradaId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_LEITURAS_RegiaoMonitoradaId",
                table: "TB_LEITURAS",
                column: "RegiaoMonitoradaId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_LEITURAS_SateliteId",
                table: "TB_LEITURAS",
                column: "SateliteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_ALERTAS");

            migrationBuilder.DropTable(
                name: "TB_LEITURAS");

            migrationBuilder.DropTable(
                name: "TB_REGIOES");

            migrationBuilder.DropTable(
                name: "TB_SATELITES");
        }
    }
}
