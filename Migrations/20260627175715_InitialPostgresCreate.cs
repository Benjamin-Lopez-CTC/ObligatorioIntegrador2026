using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ObligatorioIntegrador2026.Migrations
{
    /// <inheritdoc />
    public partial class InitialPostgresCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Analisis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analisis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Apiarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    StringIdentificador = table.Column<string>(type: "text", nullable: false),
                    UbicacionTexto = table.Column<string>(type: "text", nullable: false),
                    UbicacionCoordenadas = table.Column<string>(type: "text", nullable: false),
                    Departamento = table.Column<string>(type: "text", nullable: true),
                    SeccionPolicial = table.Column<string>(type: "text", nullable: true),
                    Paraje = table.Column<string>(type: "text", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UltimaInspeccion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Responsable = table.Column<string>(type: "text", nullable: false),
                    NotasEstado = table.Column<string>(type: "text", nullable: false),
                    UltimaEdicionNota = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    HumedadInterna = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apiarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Declaraciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaEntrega = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Declaraciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Stock = table.Column<int>(type: "integer", nullable: false),
                    Category = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    LowThreshold = table.Column<int>(type: "integer", nullable: false),
                    MediumThreshold = table.Column<int>(type: "integer", nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    UnitPrice = table.Column<double>(type: "double precision", nullable: false),
                    Currency = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoginRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AttemptDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IpAddress = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DeviceBrowser = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Location = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsSuccess = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ganancias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AnalisisId = table.Column<int>(type: "integer", nullable: false),
                    Titulo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Monto = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ganancias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ganancias_Analisis_AnalisisId",
                        column: x => x.AnalisisId,
                        principalTable: "Analisis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inversiones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AnalisisId = table.Column<int>(type: "integer", nullable: false),
                    Titulo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Nota = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Precio = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inversiones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inversiones_Analisis_AnalisisId",
                        column: x => x.AnalisisId,
                        principalTable: "Analisis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Colmenas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Identificador = table.Column<string>(type: "text", nullable: true),
                    CodigoEscaneo = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    ApiarioId = table.Column<int>(type: "integer", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    PesoKg = table.Column<double>(type: "double precision", nullable: false),
                    TemperaturaInterna = table.Column<double>(type: "double precision", nullable: false),
                    HumedadInterna = table.Column<double>(type: "double precision", nullable: false),
                    ProduccionMielKg = table.Column<double>(type: "double precision", nullable: false),
                    EsPiloto = table.Column<bool>(type: "boolean", nullable: false),
                    EsNucleo = table.Column<bool>(type: "boolean", nullable: false),
                    Alzas = table.Column<int>(type: "integer", nullable: false),
                    MediasAlzas = table.Column<int>(type: "integer", nullable: false),
                    AlzasTresCuartos = table.Column<int>(type: "integer", nullable: false),
                    CantidadAbejas = table.Column<int>(type: "integer", nullable: false),
                    UbicacionIntraApiario = table.Column<string>(type: "text", nullable: false),
                    EstadoReina = table.Column<string>(type: "text", nullable: false),
                    ComportamientoAbejas = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colmenas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Colmenas_Apiarios_ApiarioId",
                        column: x => x.ApiarioId,
                        principalTable: "Apiarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Extracciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    KilosTotales = table.Column<double>(type: "double precision", nullable: false),
                    CantidadColmenasCosechadas = table.Column<int>(type: "integer", nullable: false),
                    GananciaId = table.Column<int>(type: "integer", nullable: true),
                    Notas = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extracciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Extracciones_Ganancias_GananciaId",
                        column: x => x.GananciaId,
                        principalTable: "Ganancias",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Movimientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ColmenaId = table.Column<int>(type: "integer", nullable: false),
                    ApiarioOrigenId = table.Column<int>(type: "integer", nullable: false),
                    ApiarioDestinoId = table.Column<int>(type: "integer", nullable: false),
                    Razon = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    FechaSalida = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaRegreso = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movimientos_Apiarios_ApiarioDestinoId",
                        column: x => x.ApiarioDestinoId,
                        principalTable: "Apiarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movimientos_Apiarios_ApiarioOrigenId",
                        column: x => x.ApiarioOrigenId,
                        principalTable: "Apiarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movimientos_Colmenas_ColmenaId",
                        column: x => x.ColmenaId,
                        principalTable: "Colmenas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Treatments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ColmenaId = table.Column<int>(type: "integer", nullable: false),
                    Titulo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Tipo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Nota = table.Column<string>(type: "text", nullable: true),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Treatments_Colmenas_ColmenaId",
                        column: x => x.ColmenaId,
                        principalTable: "Colmenas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotasTecnicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ColmenaId = table.Column<int>(type: "integer", nullable: false),
                    Detalles = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    EstadoReina = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    EstadoColmena = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Temperatura = table.Column<double>(type: "double precision", nullable: true),
                    Humedad = table.Column<double>(type: "double precision", nullable: true),
                    AlzasCosechadas = table.Column<int>(type: "integer", nullable: false),
                    MediasAlzasCosechadas = table.Column<int>(type: "integer", nullable: false),
                    AlzasTresCuartosCosechadas = table.Column<int>(type: "integer", nullable: false),
                    KilosCosechados = table.Column<double>(type: "double precision", nullable: false),
                    ExtraccionId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotasTecnicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotasTecnicas_Colmenas_ColmenaId",
                        column: x => x.ColmenaId,
                        principalTable: "Colmenas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotasTecnicas_Extracciones_ExtraccionId",
                        column: x => x.ExtraccionId,
                        principalTable: "Extracciones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TreatmentEquipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TreatmentId = table.Column<int>(type: "integer", nullable: false),
                    EquipmentName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Cantidad = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentEquipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreatmentEquipments_Treatments_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Analisis",
                columns: new[] { "Id", "FechaFin", "FechaInicio" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, null, new DateTime(2026, 5, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2025, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2024, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Apiarios",
                columns: new[] { "Id", "Departamento", "FechaCreacion", "HumedadInterna", "Nombre", "NotasEstado", "Paraje", "Responsable", "SeccionPolicial", "StringIdentificador", "UbicacionCoordenadas", "UbicacionTexto", "UltimaEdicionNota", "UltimaInspeccion" },
                values: new object[,]
                {
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0, "Apiario Norte", "Acceso en buen estado.", null, "Benjamin Lopez", null, "AP-001", "-34.123, -56.456", "Ruta 5, Km 42.5", null, null },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0, "Apiario Sur", "Requiere desmalezado.", null, "Felipe Alvarez", null, "AP-002", "-34.890, -56.123", "Camino Vecinal 14", null, null },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0, "Apiario Este", "Todo normal.", null, "Matías Verges", null, "AP-003", "-33.567, -55.890", "Estancia La Paz", null, null },
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0, "Apiario Oeste", "Tranquera rota.", null, "Benjamin Lopez", null, "AP-004", "-34.456, -57.123", "Ruta 3, Km 112", null, null },
                    { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0, "Apiario Central", "Base operativa.", null, "Felipe Alvarez", null, "AP-005", "-34.567, -56.789", "Predio Principal", null, null }
                });

            migrationBuilder.InsertData(
                table: "Declaraciones",
                columns: new[] { "Id", "FechaEntrega" },
                values: new object[] { 1, new DateTime(2025, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Equipments",
                columns: new[] { "Id", "Category", "Currency", "DisplayOrder", "LowThreshold", "MediumThreshold", "Name", "Stock", "Type", "UnitPrice" },
                values: new object[,]
                {
                    { 1, "Herramienta", "USD", 1, 5, 15, "Ahumador Inoxidable", 12, "Ahumador estándar 10x25cm", 45.0 },
                    { 2, "Herramienta", "USD", 2, 10, 20, "Palanca de Manejo", 24, "Pinza y palanca universal", 12.0 },
                    { 3, "Medicamento", "USD", 3, 15, 40, "Ácido Oxálico (Glicerina)", 50, "Tratamiento Varroa", 25.0 },
                    { 4, "Medicamento", "USD", 4, 10, 25, "Amitraz (Tiras)", 15, "Tratamiento Varroa", 30.0 },
                    { 5, "Material", "UYU", 5, 30, 80, "Alzas Melarias (Media)", 120, "Madera", 250.0 },
                    { 6, "Material", "UYU", 6, 100, 200, "Marcos Alambrados", 450, "Madera/Alambre", 80.0 },
                    { 7, "Material", "UYU", 7, 10, 25, "Cera Estampada", 5, "Cera Orgánica", 120.0 },
                    { 8, "Material", "UYU", 8, 44, 93, "Piso 73", 472, "Tipo 10", 388.73000000000002 },
                    { 9, "Medicamento", "USD", 9, 32, 109, "Fluvalinato 87", 273, "Tipo 4", 56.630000000000003 },
                    { 10, "Herramienta", "USD", 10, 31, 108, "Pinza 44", 241, "Tipo 5", 432.61000000000001 },
                    { 11, "Herramienta", "USD", 11, 29, 77, "Ahumador 44", 242, "Tipo 5", 283.14999999999998 },
                    { 12, "Herramienta", "UYU", 12, 22, 116, "Guantes 45", 126, "Tipo 4", 383.42000000000002 },
                    { 13, "Material", "USD", 13, 20, 36, "Cera 89", 483, "Tipo 5", 92.260000000000005 },
                    { 14, "Herramienta", "USD", 14, 37, 118, "Ahumador 30", 80, "Tipo 9", 177.53 },
                    { 15, "Material", "UYU", 15, 11, 68, "Alimentador 51", 136, "Tipo 2", 69.569999999999993 },
                    { 16, "Material", "USD", 16, 11, 76, "Alza 59", 62, "Tipo 10", 489.99000000000001 },
                    { 17, "Material", "UYU", 17, 35, 60, "Techo 55", 478, "Tipo 6", 54.93 },
                    { 18, "Herramienta", "USD", 18, 41, 87, "Guantes 21", 159, "Tipo 3", 41.640000000000001 },
                    { 19, "Herramienta", "USD", 19, 49, 95, "Cepillo 67", 435, "Tipo 9", 252.84999999999999 },
                    { 20, "Material", "UYU", 20, 22, 32, "Alza 76", 5, "Tipo 6", 494.91000000000003 },
                    { 21, "Material", "USD", 21, 28, 63, "Piso 45", 53, "Tipo 10", 440.13 },
                    { 22, "Medicamento", "UYU", 22, 12, 51, "Amitraz 4", 81, "Tipo 2", 130.30000000000001 },
                    { 23, "Herramienta", "USD", 23, 31, 128, "Pinza 35", 440, "Tipo 8", 11.02 },
                    { 24, "Material", "USD", 24, 5, 71, "Techo 89", 160, "Tipo 2", 467.95999999999998 },
                    { 25, "Material", "UYU", 25, 45, 69, "Piso 50", 478, "Tipo 5", 364.36000000000001 }
                });

            migrationBuilder.InsertData(
                table: "Extracciones",
                columns: new[] { "Id", "CantidadColmenasCosechadas", "Fecha", "GananciaId", "KilosTotales", "Notas" },
                values: new object[,]
                {
                    { 1, 19, new DateTime(2026, 6, 15, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(1141), null, 302.5, "Cosecha mensual general" },
                    { 2, 6, new DateTime(2026, 5, 15, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(1737), null, 103.8, "Cosecha mensual general" },
                    { 3, 12, new DateTime(2026, 4, 15, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(1756), null, 214.69999999999999, "Cosecha mensual general" },
                    { 4, 6, new DateTime(2026, 3, 15, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(1759), null, 210.69999999999999, "Cosecha mensual general" },
                    { 5, 16, new DateTime(2026, 2, 15, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(1761), null, 213.0, "Cosecha mensual general" },
                    { 6, 19, new DateTime(2026, 1, 15, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(1763), null, 274.30000000000001, "Cosecha mensual general" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FullName", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "Benjamin Lopez", "Admin123!", "Admin", "b.lopez" },
                    { 2, "Felipe Alvarez", "Admin123!", "Admin", "f.alvarez" },
                    { 3, "Matías Verges", "Matias123!", "Beekeeper", "m.verges" }
                });

            migrationBuilder.InsertData(
                table: "Colmenas",
                columns: new[] { "Id", "Alzas", "AlzasTresCuartos", "ApiarioId", "CantidadAbejas", "CodigoEscaneo", "ComportamientoAbejas", "EsNucleo", "EsPiloto", "Estado", "EstadoReina", "HumedadInterna", "Identificador", "MediasAlzas", "PesoKg", "ProduccionMielKg", "TemperaturaInterna", "UbicacionIntraApiario" },
                values: new object[,]
                {
                    { 1, 0, 0, 1, 45000, "100001", "Dócil", false, true, "Óptimo", "Presente", 55.0, "#HIVE-0042", 0, 45.200000000000003, 0.0, 34.5, "Fila 1, Pos 1" },
                    { 2, 1, 1, 1, 38000, "100002", "Defensivo", true, false, "Alerta", "Presente", 0.0, "#HIVE-0089", 0, 42.799999999999997, 39.0, 32.0, "Fila 1, Pos 2" },
                    { 3, 1, 0, 2, 15000, "100003", "Agresivo", false, true, "Crítico", "Ausente", 60.0, "#HIVE-0112", 0, 31.0, 22.0, 36.5, "Fila 2, Pos 1" },
                    { 4, 0, 0, 2, 50000, "100004", "Dócil", true, true, "Óptimo", "Presente", 58.0, "#HIVE-0045", 0, 48.100000000000001, 0.0, 34.200000000000003, "Fila 2, Pos 2" },
                    { 5, 1, 1, 3, 42000, "100005", "Dócil", false, true, "Óptimo", "Presente", 52.0, "#HIVE-0001", 0, 40.0, 39.0, 35.100000000000001, "Fila 1, Pos 1" },
                    { 6, 0, 0, 4, 41000, "100006", "Dócil", false, false, "Óptimo", "Presente", 0.0, "#HIVE-0002", 0, 39.5, 0.0, 34.799999999999997, "Fila 1, Pos 2" },
                    { 7, 1, 0, 5, 12000, "100007", "Agresivo", true, true, "Crítico", "Presente", 82.0, "#HIVE-0003", 0, 25.0, 22.0, 30.0, "Única" },
                    { 8, 0, 0, 4, 11201, "100008", "Agresivo", false, false, "Alerta", "Cambiando", 53.200000000000003, "#HIVE-0008", 0, 52.0, 0.0, 35.600000000000001, "Fila 1, Pos 10" },
                    { 9, 1, 0, 3, 55923, "100009", "Agresivo", false, false, "Alerta", "Ausente", 41.0, "#HIVE-0009", 0, 56.5, 22.0, 35.5, "Fila 1, Pos 10" },
                    { 10, 0, 0, 3, 55082, "100010", "Defensivo", false, false, "Óptimo", "Ausente", 52.200000000000003, "#HIVE-0010", 0, 43.700000000000003, 0.0, 31.0, "Fila 3, Pos 7" },
                    { 11, 1, 0, 4, 35199, "100011", "Dócil", false, false, "Crítico", "Ausente", 60.700000000000003, "#HIVE-0011", 0, 32.399999999999999, 22.0, 34.200000000000003, "Fila 4, Pos 2" },
                    { 12, 1, 0, 5, 27403, "100012", "Dócil", false, false, "Crítico", "Ausente", 56.799999999999997, "#HIVE-0012", 0, 28.300000000000001, 22.0, 34.700000000000003, "Fila 2, Pos 10" },
                    { 13, 0, 1, 5, 55403, "100013", "Defensivo", false, false, "Óptimo", "Presente", 61.5, "#HIVE-0013", 0, 47.799999999999997, 17.0, 35.600000000000001, "Fila 3, Pos 5" },
                    { 14, 0, 0, 3, 21796, "100014", "Dócil", false, false, "Crítico", "Ausente", 61.700000000000003, "#HIVE-0014", 0, 25.300000000000001, 0.0, 35.600000000000001, "Fila 4, Pos 1" },
                    { 15, 0, 0, 1, 44489, "100015", "Agresivo", false, false, "Óptimo", "Ausente", 43.5, "#HIVE-0015", 0, 43.799999999999997, 0.0, 33.600000000000001, "Fila 1, Pos 9" },
                    { 16, 1, 0, 3, 57003, "100016", "Agresivo", true, false, "Alerta", "Presente", 55.799999999999997, "#HIVE-0016", 0, 21.699999999999999, 22.0, 34.899999999999999, "Fila 2, Pos 7" },
                    { 17, 1, 0, 2, 51710, "100017", "Defensivo", false, false, "Crítico", "Ausente", 46.100000000000001, "#HIVE-0017", 0, 40.799999999999997, 22.0, 36.799999999999997, "Fila 1, Pos 6" },
                    { 18, 0, 0, 1, 30317, "100018", "Agresivo", false, false, "Alerta", "Cambiando", 69.799999999999997, "#HIVE-0018", 0, 20.600000000000001, 0.0, 30.300000000000001, "Fila 2, Pos 3" },
                    { 19, 0, 0, 4, 12828, "100019", "Dócil", false, false, "Crítico", "Ausente", 67.799999999999997, "#HIVE-0019", 0, 58.799999999999997, 0.0, 31.800000000000001, "Fila 2, Pos 5" },
                    { 20, 0, 0, 3, 36856, "100020", "Dócil", false, false, "Crítico", "Cambiando", 40.100000000000001, "#HIVE-0020", 0, 39.299999999999997, 0.0, 37.5, "Fila 4, Pos 8" },
                    { 21, 0, 0, 1, 52958, "100021", "Dócil", false, false, "Crítico", "Presente", 48.399999999999999, "#HIVE-0021", 0, 51.100000000000001, 0.0, 33.899999999999999, "Fila 4, Pos 1" },
                    { 22, 1, 0, 1, 26012, "100022", "Dócil", false, false, "Crítico", "Cambiando", 55.799999999999997, "#HIVE-0022", 0, 29.300000000000001, 22.0, 31.800000000000001, "Fila 1, Pos 5" },
                    { 23, 1, 0, 2, 40475, "100023", "Defensivo", false, false, "Crítico", "Ausente", 53.600000000000001, "#HIVE-0023", 2, 36.399999999999999, 46.0, 32.100000000000001, "Fila 4, Pos 1" },
                    { 24, 0, 1, 5, 13287, "100024", "Agresivo", false, false, "Óptimo", "Ausente", 42.799999999999997, "#HIVE-0024", 2, 49.0, 41.0, 37.700000000000003, "Fila 3, Pos 7" },
                    { 25, 1, 0, 5, 55720, "100025", "Defensivo", false, true, "Óptimo", "Presente", 43.200000000000003, "#HIVE-0025", 0, 59.299999999999997, 22.0, 31.399999999999999, "Fila 1, Pos 2" },
                    { 26, 0, 1, 2, 37187, "100026", "Dócil", false, false, "Alerta", "Cambiando", 54.700000000000003, "#HIVE-0026", 1, 57.5, 29.0, 35.100000000000001, "Fila 3, Pos 2" },
                    { 27, 1, 0, 2, 15033, "100027", "Agresivo", true, true, "Alerta", "Cambiando", 67.900000000000006, "#HIVE-0027", 0, 46.0, 22.0, 32.299999999999997, "Fila 3, Pos 9" },
                    { 28, 1, 1, 5, 49191, "100028", "Defensivo", false, true, "Óptimo", "Ausente", 59.399999999999999, "#HIVE-0028", 0, 32.399999999999999, 39.0, 34.700000000000003, "Fila 4, Pos 9" },
                    { 29, 0, 0, 4, 20521, "100029", "Dócil", true, false, "Crítico", "Presente", 66.5, "#HIVE-0029", 0, 59.100000000000001, 0.0, 34.700000000000003, "Fila 2, Pos 8" },
                    { 30, 1, 0, 2, 49262, "100030", "Defensivo", true, true, "Crítico", "Presente", 42.200000000000003, "#HIVE-0030", 0, 38.200000000000003, 22.0, 35.5, "Fila 1, Pos 5" },
                    { 31, 0, 0, 3, 58853, "100031", "Defensivo", true, true, "Alerta", "Presente", 48.399999999999999, "#HIVE-0031", 1, 42.899999999999999, 12.0, 36.600000000000001, "Fila 2, Pos 7" },
                    { 32, 0, 0, 3, 18926, "100032", "Agresivo", false, true, "Óptimo", "Presente", 53.399999999999999, "#HIVE-0032", 0, 27.199999999999999, 0.0, 36.600000000000001, "Fila 4, Pos 7" },
                    { 33, 1, 0, 4, 28734, "100033", "Defensivo", true, false, "Alerta", "Cambiando", 58.799999999999997, "#HIVE-0033", 1, 23.800000000000001, 34.0, 35.5, "Fila 2, Pos 6" },
                    { 34, 1, 0, 4, 48819, "100034", "Defensivo", false, true, "Óptimo", "Ausente", 68.400000000000006, "#HIVE-0034", 0, 32.600000000000001, 22.0, 32.600000000000001, "Fila 3, Pos 1" },
                    { 35, 0, 1, 4, 54686, "100035", "Defensivo", true, true, "Alerta", "Cambiando", 41.799999999999997, "#HIVE-0035", 1, 32.5, 29.0, 37.600000000000001, "Fila 4, Pos 2" },
                    { 36, 0, 0, 1, 16738, "100036", "Agresivo", false, true, "Alerta", "Ausente", 46.899999999999999, "#HIVE-0036", 0, 21.100000000000001, 0.0, 35.100000000000001, "Fila 2, Pos 7" },
                    { 37, 0, 1, 4, 19321, "100037", "Dócil", false, false, "Óptimo", "Cambiando", 44.600000000000001, "#HIVE-0037", 0, 50.100000000000001, 17.0, 37.399999999999999, "Fila 5, Pos 10" },
                    { 38, 1, 0, 1, 36239, "100038", "Agresivo", false, true, "Óptimo", "Ausente", 52.600000000000001, "#HIVE-0038", 2, 30.600000000000001, 46.0, 33.5, "Fila 1, Pos 8" },
                    { 39, 0, 0, 3, 51989, "100039", "Dócil", false, false, "Crítico", "Ausente", 50.700000000000003, "#HIVE-0039", 0, 21.199999999999999, 0.0, 33.600000000000001, "Fila 4, Pos 1" },
                    { 40, 0, 0, 3, 24633, "100040", "Defensivo", false, false, "Alerta", "Ausente", 68.5, "#HIVE-0040", 2, 36.0, 24.0, 36.700000000000003, "Fila 4, Pos 6" },
                    { 41, 0, 1, 5, 46293, "100041", "Defensivo", true, false, "Óptimo", "Presente", 49.799999999999997, "#HIVE-0041", 2, 42.799999999999997, 41.0, 35.200000000000003, "Fila 2, Pos 4" },
                    { 42, 1, 1, 1, 12573, "100042", "Dócil", false, false, "Alerta", "Ausente", 55.899999999999999, "#HIVE-0042", 0, 41.0, 39.0, 32.5, "Fila 2, Pos 8" },
                    { 43, 1, 0, 4, 22604, "100043", "Dócil", false, false, "Crítico", "Ausente", 44.299999999999997, "#HIVE-0043", 0, 54.200000000000003, 22.0, 37.0, "Fila 5, Pos 10" },
                    { 44, 1, 0, 4, 59848, "100044", "Dócil", false, true, "Crítico", "Cambiando", 41.100000000000001, "#HIVE-0044", 2, 24.0, 46.0, 32.700000000000003, "Fila 1, Pos 8" },
                    { 45, 0, 0, 1, 44767, "100045", "Dócil", false, true, "Alerta", "Ausente", 59.700000000000003, "#HIVE-0045", 1, 22.100000000000001, 12.0, 37.700000000000003, "Fila 3, Pos 2" },
                    { 46, 1, 0, 4, 56454, "100046", "Defensivo", true, false, "Crítico", "Presente", 59.899999999999999, "#HIVE-0046", 0, 23.899999999999999, 22.0, 33.299999999999997, "Fila 2, Pos 4" },
                    { 47, 0, 0, 4, 40516, "100047", "Dócil", true, false, "Crítico", "Ausente", 63.200000000000003, "#HIVE-0047", 0, 57.200000000000003, 0.0, 35.200000000000003, "Fila 2, Pos 6" },
                    { 48, 1, 0, 1, 36180, "100048", "Agresivo", false, false, "Alerta", "Cambiando", 64.299999999999997, "#HIVE-0048", 0, 38.299999999999997, 22.0, 35.600000000000001, "Fila 3, Pos 4" },
                    { 49, 1, 0, 4, 16233, "100049", "Defensivo", false, true, "Crítico", "Presente", 48.899999999999999, "#HIVE-0049", 1, 30.5, 34.0, 34.299999999999997, "Fila 3, Pos 3" },
                    { 50, 1, 0, 5, 20332, "100050", "Defensivo", false, false, "Óptimo", "Ausente", 43.899999999999999, "#HIVE-0050", 1, 47.600000000000001, 34.0, 36.5, "Fila 3, Pos 3" }
                });

            migrationBuilder.InsertData(
                table: "Ganancias",
                columns: new[] { "Id", "AnalisisId", "Descripcion", "Monto", "Titulo" },
                values: new object[,]
                {
                    { 1, 1, "Precio: $35/kg", 29750.0, "Venta de Miel (850 kg)" },
                    { 2, 1, "Precio: $350/u.", 7000.0, "Venta de Núcleos (20 u.)" },
                    { 3, 1, "Precio: $35/kg", 1750.0, "Venta de Polen (50 kg)" },
                    { 4, 2, "Reserva de lote", 3500.0, "Venta anticipada de propóleo" },
                    { 5, 3, "Ingreso operativo", 29717.779999999999, "Polinización" },
                    { 6, 2, "Ingreso operativo", 18368.509999999998, "Venta Tambor Miel" },
                    { 7, 1, "Ingreso operativo", 7355.3800000000001, "Venta Núcleos" },
                    { 8, 4, "Ingreso operativo", 22978.860000000001, "Propóleo" },
                    { 9, 4, "Ingreso operativo", 5258.6199999999999, "Polinización" },
                    { 10, 4, "Ingreso operativo", 26007.049999999999, "Polinización" },
                    { 11, 3, "Ingreso operativo", 4582.2299999999996, "Polinización" },
                    { 12, 1, "Ingreso operativo", 14708.469999999999, "Venta Tambor Miel" },
                    { 13, 3, "Ingreso operativo", 16672.27, "Venta Tambor Miel" },
                    { 14, 1, "Ingreso operativo", 5093.1700000000001, "Propóleo" },
                    { 15, 2, "Ingreso operativo", 29355.810000000001, "Propóleo" },
                    { 16, 4, "Ingreso operativo", 27453.57, "Polinización" },
                    { 17, 1, "Ingreso operativo", 25781.18, "Cera" },
                    { 18, 3, "Ingreso operativo", 11894.809999999999, "Propóleo" },
                    { 19, 2, "Ingreso operativo", 9508.5699999999997, "Polinización" },
                    { 20, 4, "Ingreso operativo", 7210.9700000000003, "Venta Núcleos" },
                    { 21, 1, "Ingreso operativo", 14765.190000000001, "Cera" },
                    { 22, 3, "Ingreso operativo", 2098.0999999999999, "Propóleo" },
                    { 23, 3, "Ingreso operativo", 10154.120000000001, "Cera" },
                    { 24, 2, "Ingreso operativo", 12874.49, "Propóleo" },
                    { 25, 1, "Ingreso operativo", 14028.940000000001, "Venta Núcleos" },
                    { 26, 4, "Ingreso operativo", 22364.18, "Cera" },
                    { 27, 2, "Ingreso operativo", 5601.8800000000001, "Propóleo" },
                    { 28, 1, "Ingreso operativo", 4108.5699999999997, "Venta Tambor Miel" },
                    { 29, 3, "Ingreso operativo", 10000.809999999999, "Venta Núcleos" },
                    { 30, 2, "Ingreso operativo", 17510.400000000001, "Propóleo" }
                });

            migrationBuilder.InsertData(
                table: "Inversiones",
                columns: new[] { "Id", "AnalisisId", "Nota", "Precio", "Titulo" },
                values: new object[,]
                {
                    { 1, 1, "Logística", 2400.0, "Combustible por viaje" },
                    { 2, 1, "Ahumadores, trajes", 3150.0, "Equipamiento nuevo" },
                    { 3, 1, "Suministros Médicos", 4200.0, "Tratamientos Varroa" },
                    { 4, 1, "Materiales", 4500.0, "Mantenimiento de Cajas" },
                    { 5, 2, "Insumo inicial", 1200.0, "Compra de cera estampada" },
                    { 6, 4, "Gasto operativo", 4761.6400000000003, "Herramientas" },
                    { 7, 1, "Gasto operativo", 5940.0600000000004, "Alimentación" },
                    { 8, 3, "Gasto operativo", 5137.0, "Suministros" },
                    { 9, 1, "Gasto operativo", 6838.9200000000001, "Compra Reinas" },
                    { 10, 1, "Gasto operativo", 7185.3400000000001, "Herramientas" },
                    { 11, 2, "Gasto operativo", 6073.7600000000002, "Combustible" },
                    { 12, 3, "Gasto operativo", 1367.8, "Combustible" },
                    { 13, 3, "Gasto operativo", 7023.7700000000004, "Herramientas" },
                    { 14, 1, "Gasto operativo", 8054.2700000000004, "Suministros" },
                    { 15, 1, "Gasto operativo", 690.89999999999998, "Herramientas" },
                    { 16, 1, "Gasto operativo", 3315.3200000000002, "Suministros" },
                    { 17, 1, "Gasto operativo", 543.96000000000004, "Compra Reinas" },
                    { 18, 1, "Gasto operativo", 6132.8599999999997, "Herramientas" },
                    { 19, 3, "Gasto operativo", 4244.9799999999996, "Compra Reinas" },
                    { 20, 2, "Gasto operativo", 4966.3100000000004, "Reparación Camioneta" },
                    { 21, 4, "Gasto operativo", 6615.5299999999997, "Compra Reinas" },
                    { 22, 3, "Gasto operativo", 9982.6100000000006, "Suministros" },
                    { 23, 3, "Gasto operativo", 1594.28, "Reparación Camioneta" },
                    { 24, 1, "Gasto operativo", 9027.1499999999996, "Herramientas" },
                    { 25, 3, "Gasto operativo", 3028.0100000000002, "Suministros" },
                    { 26, 4, "Gasto operativo", 8671.2099999999991, "Suministros" },
                    { 27, 4, "Gasto operativo", 1428.0799999999999, "Combustible" },
                    { 28, 2, "Gasto operativo", 6436.7200000000003, "Suministros" },
                    { 29, 2, "Gasto operativo", 1610.3199999999999, "Alimentación" },
                    { 30, 1, "Gasto operativo", 3310.8299999999999, "Combustible" },
                    { 31, 3, "Gasto operativo", 2293.5799999999999, "Combustible" },
                    { 32, 2, "Gasto operativo", 9485.0599999999995, "Herramientas" },
                    { 33, 4, "Gasto operativo", 4430.5100000000002, "Alimentación" },
                    { 34, 3, "Gasto operativo", 6422.2600000000002, "Suministros" },
                    { 35, 1, "Gasto operativo", 7585.9499999999998, "Combustible" },
                    { 36, 1, "Gasto operativo", 8684.9500000000007, "Suministros" },
                    { 37, 4, "Gasto operativo", 1464.48, "Suministros" },
                    { 38, 1, "Gasto operativo", 7458.54, "Compra Reinas" },
                    { 39, 2, "Gasto operativo", 1525.53, "Compra Reinas" },
                    { 40, 2, "Gasto operativo", 6379.5699999999997, "Suministros" }
                });

            migrationBuilder.InsertData(
                table: "Movimientos",
                columns: new[] { "Id", "ApiarioDestinoId", "ApiarioOrigenId", "ColmenaId", "Estado", "FechaRegreso", "FechaSalida", "Razon" },
                values: new object[,]
                {
                    { 1, 2, 1, 1, "Vigente", new DateTime(2026, 7, 12, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6141), new DateTime(2026, 6, 22, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6056), "Polinización Alfalfa" },
                    { 2, 3, 1, 2, "Vigente", new DateTime(2026, 6, 29, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6298), new DateTime(2026, 6, 7, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6297), "Refugio Invernal" },
                    { 3, 2, 1, 3, "Vigente", new DateTime(2026, 6, 25, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6302), new DateTime(2026, 6, 17, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6301), "Prueba de campo" },
                    { 4, 1, 2, 5, "Completado", new DateTime(2026, 6, 17, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6314), new DateTime(2026, 5, 18, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6304), "Floración temprana" },
                    { 5, 3, 2, 6, "Cancelado", new DateTime(2026, 7, 2, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6318), new DateTime(2026, 6, 22, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6317), "Error de registro" },
                    { 6, 5, 2, 29, "Vigente", new DateTime(2026, 5, 28, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6320), new DateTime(2026, 5, 30, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6319), "Refugio Invernal" },
                    { 7, 2, 4, 29, "Vigente", new DateTime(2026, 5, 9, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6324), new DateTime(2026, 5, 2, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6323), "Floración Pradera" },
                    { 8, 3, 5, 10, "Completado", new DateTime(2026, 5, 20, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6327), new DateTime(2026, 5, 22, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6326), "Venta/Préstamo" },
                    { 9, 3, 5, 13, "Cancelado", new DateTime(2026, 6, 9, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6330), new DateTime(2026, 6, 10, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6329), "Cuarentena" },
                    { 10, 2, 1, 19, "Cancelado", new DateTime(2026, 5, 6, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6333), new DateTime(2026, 4, 19, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6333), "Venta/Préstamo" },
                    { 11, 2, 1, 1, "Completado", new DateTime(2026, 4, 28, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6335), new DateTime(2026, 4, 17, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6335), "Cuarentena" },
                    { 12, 2, 1, 34, "Completado", new DateTime(2026, 5, 3, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6337), new DateTime(2026, 4, 26, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6337), "Refugio Invernal" },
                    { 13, 4, 1, 2, "Completado", new DateTime(2026, 5, 18, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6340), new DateTime(2026, 5, 22, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6339), "Refugio Invernal" },
                    { 14, 1, 3, 27, "Completado", new DateTime(2026, 4, 25, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6342), new DateTime(2026, 5, 5, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6342), "Venta/Préstamo" },
                    { 15, 3, 4, 24, "Cancelado", new DateTime(2026, 3, 17, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6345), new DateTime(2026, 4, 6, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6344), "Cuarentena" },
                    { 16, 1, 2, 9, "Completado", new DateTime(2026, 4, 10, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6356), new DateTime(2026, 4, 2, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6355), "Venta/Préstamo" },
                    { 17, 4, 2, 27, "Vigente", new DateTime(2026, 6, 17, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6358), new DateTime(2026, 5, 29, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6357), "Floración Eucalyptus" },
                    { 18, 5, 1, 32, "Completado", new DateTime(2026, 5, 6, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6360), new DateTime(2026, 5, 26, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6359), "Cuarentena" },
                    { 19, 4, 2, 20, "Vigente", new DateTime(2026, 5, 1, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6362), new DateTime(2026, 5, 20, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6361), "Floración Pradera" },
                    { 20, 5, 1, 3, "Cancelado", new DateTime(2026, 5, 12, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6364), new DateTime(2026, 5, 25, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6363), "Cuarentena" },
                    { 21, 1, 4, 9, "Cancelado", new DateTime(2026, 5, 11, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6366), new DateTime(2026, 5, 12, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6365), "Cuarentena" },
                    { 22, 1, 2, 8, "Vigente", new DateTime(2026, 6, 7, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6368), new DateTime(2026, 5, 29, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6367), "Refugio Invernal" },
                    { 23, 3, 1, 1, "Completado", new DateTime(2026, 5, 11, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6370), new DateTime(2026, 4, 28, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6369), "Venta/Préstamo" },
                    { 24, 5, 2, 14, "Completado", new DateTime(2026, 4, 2, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6372), new DateTime(2026, 3, 30, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6371), "Cuarentena" },
                    { 25, 5, 1, 26, "Completado", new DateTime(2026, 5, 26, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6374), new DateTime(2026, 6, 10, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6373), "Floración Eucalyptus" },
                    { 26, 5, 2, 40, "Completado", new DateTime(2026, 4, 10, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6376), new DateTime(2026, 4, 24, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6375), "Refugio Invernal" },
                    { 27, 3, 2, 30, "Completado", new DateTime(2026, 5, 19, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6378), new DateTime(2026, 5, 22, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6377), "Floración Eucalyptus" },
                    { 28, 4, 2, 4, "Completado", new DateTime(2026, 3, 19, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6388), new DateTime(2026, 3, 23, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6387), "Cuarentena" },
                    { 29, 1, 5, 11, "Cancelado", new DateTime(2026, 3, 10, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6390), new DateTime(2026, 3, 27, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6390), "Venta/Préstamo" },
                    { 30, 4, 2, 6, "Cancelado", new DateTime(2026, 3, 11, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6392), new DateTime(2026, 3, 24, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(6392), "Refugio Invernal" }
                });

            migrationBuilder.InsertData(
                table: "NotasTecnicas",
                columns: new[] { "Id", "AlzasCosechadas", "AlzasTresCuartosCosechadas", "ColmenaId", "Detalles", "EstadoColmena", "EstadoReina", "ExtraccionId", "Fecha", "Humedad", "KilosCosechados", "MediasAlzasCosechadas", "Temperatura" },
                values: new object[,]
                {
                    { 1, 0, 0, 1, "Revisión general, todo normal.", "Óptimo", "Presente", null, new DateTime(2026, 6, 17, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(2805), 55.0, 0.0, 0, 34.5 },
                    { 2, 0, 0, 2, "Abejas defensivas, observar.", "Alerta", "Presente", null, new DateTime(2026, 6, 12, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3156), 60.0, 0.0, 0, 35.0 },
                    { 3, 0, 0, 3, "Reina no avistada. Posible enjambrazón.", "Crítico", "Ausente", null, new DateTime(2026, 6, 25, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3158), 50.0, 0.0, 0, 33.200000000000003 },
                    { 4, 0, 0, 4, "Excelente producción.", "Óptimo", "Presente", null, new DateTime(2026, 6, 22, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3160), 56.0, 0.0, 0, 34.799999999999997 },
                    { 5, 2, 0, 5, "Cosecha registrada en masa.", "Óptimo", "Presente", 1, new DateTime(2026, 6, 20, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3163), null, 44.0, 0, null },
                    { 6, 0, 0, 6, "Normal.", "Óptimo", "Presente", null, new DateTime(2026, 6, 7, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3403), 58.0, 0.0, 0, 35.100000000000001 },
                    { 7, 0, 0, 7, "Humedad alta.", "Crítico", "Presente", null, new DateTime(2026, 5, 23, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3417), 80.0, 0.0, 0, 32.0 },
                    { 8, 0, 0, 7, "Se alimentó", "Crítico", "Cambiando", null, new DateTime(2026, 5, 28, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3419), 61.399999999999999, 0.0, 0, 33.100000000000001 },
                    { 9, 0, 0, 4, "Revisión de rutina", "Crítico", "Cambiando", null, new DateTime(2026, 3, 26, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3421), 48.700000000000003, 0.0, 0, 31.399999999999999 },
                    { 10, 0, 0, 36, "Revisión de rutina", "Crítico", "Cambiando", null, new DateTime(2026, 5, 25, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3424), 63.200000000000003, 0.0, 0, 33.600000000000001 },
                    { 11, 0, 0, 16, "Se limpió piso", "Óptimo", "Presente", null, new DateTime(2026, 4, 1, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3426), 61.200000000000003, 0.0, 0, 37.700000000000003 },
                    { 12, 0, 0, 23, "Reina joven", "Óptimo", "Presente", null, new DateTime(2026, 4, 23, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3428), 45.100000000000001, 0.0, 0, 33.799999999999997 },
                    { 13, 0, 0, 3, "Sin novedades", "Crítico", "Cambiando", null, new DateTime(2026, 6, 18, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3430), 68.099999999999994, 0.0, 0, 36.700000000000003 },
                    { 14, 0, 0, 45, "Alta presencia de zánganos", "Óptimo", "Presente", null, new DateTime(2026, 6, 2, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3433), 56.200000000000003, 0.0, 0, 35.700000000000003 },
                    { 15, 0, 0, 11, "Se agregó cera", "Crítico", "Cambiando", null, new DateTime(2026, 4, 24, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3435), 45.100000000000001, 0.0, 0, 31.699999999999999 },
                    { 16, 0, 0, 22, "Alta presencia de zánganos", "Alerta", "Ausente", null, new DateTime(2026, 5, 20, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3437), 54.200000000000003, 0.0, 0, 35.100000000000001 },
                    { 17, 0, 0, 1, "Sin novedades", "Óptimo", "Presente", null, new DateTime(2026, 5, 5, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3449), 51.600000000000001, 0.0, 0, 34.899999999999999 },
                    { 18, 0, 0, 42, "Reina joven", "Óptimo", "Cambiando", null, new DateTime(2026, 5, 9, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3451), 53.100000000000001, 0.0, 0, 32.299999999999997 },
                    { 19, 0, 0, 6, "Alta presencia de zánganos", "Crítico", "Presente", null, new DateTime(2026, 6, 24, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3453), 55.0, 0.0, 0, 38.0 },
                    { 20, 0, 0, 24, "Reina joven", "Crítico", "Presente", null, new DateTime(2026, 5, 13, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3456), 63.299999999999997, 0.0, 0, 31.399999999999999 },
                    { 21, 0, 0, 42, "Revisión de rutina", "Óptimo", "Cambiando", null, new DateTime(2026, 5, 7, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3458), 66.700000000000003, 0.0, 0, 36.0 },
                    { 22, 0, 0, 11, "Sin novedades", "Alerta", "Cambiando", null, new DateTime(2026, 5, 23, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3460), 41.899999999999999, 0.0, 0, 30.899999999999999 },
                    { 23, 0, 0, 31, "Se alimentó", "Crítico", "Ausente", null, new DateTime(2026, 6, 15, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3462), 45.600000000000001, 0.0, 0, 36.100000000000001 },
                    { 24, 0, 0, 22, "Sin novedades", "Óptimo", "Cambiando", null, new DateTime(2026, 3, 24, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3464), 65.200000000000003, 0.0, 0, 34.600000000000001 },
                    { 25, 0, 0, 25, "Se agregó cera", "Alerta", "Ausente", null, new DateTime(2026, 6, 4, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3466), 67.0, 0.0, 0, 31.899999999999999 },
                    { 26, 0, 0, 21, "Se alimentó", "Óptimo", "Cambiando", null, new DateTime(2026, 6, 16, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3476), 50.200000000000003, 0.0, 0, 36.0 },
                    { 27, 0, 0, 40, "Se alimentó", "Crítico", "Ausente", null, new DateTime(2026, 5, 10, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3489), 58.600000000000001, 0.0, 0, 30.899999999999999 },
                    { 28, 0, 0, 50, "Se limpió piso", "Crítico", "Presente", null, new DateTime(2026, 4, 15, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3491), 47.899999999999999, 0.0, 0, 34.100000000000001 },
                    { 29, 0, 0, 35, "Se alimentó", "Óptimo", "Cambiando", null, new DateTime(2026, 6, 10, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3493), 46.899999999999999, 0.0, 0, 32.600000000000001 },
                    { 30, 0, 0, 13, "Alta presencia de zánganos", "Crítico", "Presente", null, new DateTime(2026, 6, 26, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3495), 55.5, 0.0, 0, 30.399999999999999 },
                    { 31, 0, 0, 45, "Revisión de rutina", "Crítico", "Ausente", null, new DateTime(2026, 3, 20, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3497), 59.799999999999997, 0.0, 0, 31.199999999999999 },
                    { 32, 0, 0, 44, "Reina joven", "Óptimo", "Presente", null, new DateTime(2026, 4, 15, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3499), 63.200000000000003, 0.0, 0, 35.0 },
                    { 33, 0, 0, 27, "Sin novedades", "Alerta", "Presente", null, new DateTime(2026, 5, 16, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3501), 53.100000000000001, 0.0, 0, 37.899999999999999 },
                    { 34, 0, 0, 9, "Alta presencia de zánganos", "Óptimo", "Cambiando", null, new DateTime(2026, 6, 12, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3504), 57.0, 0.0, 0, 30.399999999999999 },
                    { 35, 0, 0, 13, "Sin novedades", "Alerta", "Presente", null, new DateTime(2026, 4, 20, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3506), 46.299999999999997, 0.0, 0, 32.0 },
                    { 36, 0, 0, 34, "Se limpió piso", "Alerta", "Presente", null, new DateTime(2026, 3, 28, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3508), 56.5, 0.0, 0, 36.299999999999997 },
                    { 37, 0, 0, 26, "Se limpió piso", "Crítico", "Ausente", null, new DateTime(2026, 4, 18, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3519), 63.5, 0.0, 0, 36.899999999999999 },
                    { 38, 0, 0, 7, "Reina joven", "Alerta", "Cambiando", null, new DateTime(2026, 5, 8, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3521), 60.299999999999997, 0.0, 0, 33.5 },
                    { 39, 0, 0, 49, "Alta presencia de zánganos", "Alerta", "Cambiando", null, new DateTime(2026, 4, 27, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3523), 64.0, 0.0, 0, 34.799999999999997 },
                    { 40, 0, 0, 10, "Se alimentó", "Óptimo", "Presente", null, new DateTime(2026, 5, 20, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3525), 60.899999999999999, 0.0, 0, 34.700000000000003 },
                    { 41, 0, 0, 13, "Se limpió piso", "Alerta", "Ausente", null, new DateTime(2026, 3, 20, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3527), 61.0, 0.0, 0, 36.0 },
                    { 42, 0, 0, 22, "Alta presencia de zánganos", "Crítico", "Cambiando", null, new DateTime(2026, 6, 8, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3530), 47.299999999999997, 0.0, 0, 33.600000000000001 },
                    { 43, 0, 0, 49, "Reina joven", "Óptimo", "Presente", null, new DateTime(2026, 5, 23, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3532), 69.799999999999997, 0.0, 0, 37.200000000000003 },
                    { 44, 0, 0, 44, "Se limpió piso", "Alerta", "Presente", null, new DateTime(2026, 3, 29, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3534), 67.900000000000006, 0.0, 0, 36.600000000000001 },
                    { 45, 0, 0, 50, "Sin novedades", "Óptimo", "Cambiando", null, new DateTime(2026, 4, 24, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3536), 62.5, 0.0, 0, 37.0 },
                    { 46, 0, 0, 31, "Alta presencia de zánganos", "Crítico", "Cambiando", null, new DateTime(2026, 6, 25, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3538), 65.0, 0.0, 0, 34.299999999999997 },
                    { 47, 0, 0, 10, "Reina joven", "Óptimo", "Cambiando", null, new DateTime(2026, 6, 9, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3547), 47.200000000000003, 0.0, 0, 37.5 },
                    { 48, 0, 0, 19, "Revisión de rutina", "Alerta", "Presente", null, new DateTime(2026, 4, 26, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3549), 48.0, 0.0, 0, 30.699999999999999 },
                    { 49, 0, 0, 37, "Revisión de rutina", "Óptimo", "Ausente", null, new DateTime(2026, 4, 18, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3551), 60.899999999999999, 0.0, 0, 31.800000000000001 },
                    { 50, 0, 0, 24, "Se alimentó", "Óptimo", "Cambiando", null, new DateTime(2026, 3, 29, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3553), 41.899999999999999, 0.0, 0, 37.5 },
                    { 51, 0, 0, 7, "Se alimentó", "Óptimo", "Ausente", null, new DateTime(2026, 5, 7, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3555), 48.200000000000003, 0.0, 0, 37.299999999999997 },
                    { 52, 0, 0, 9, "Se agregó cera", "Alerta", "Ausente", null, new DateTime(2026, 6, 5, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3557), 51.5, 0.0, 0, 31.5 },
                    { 53, 0, 0, 3, "Se alimentó", "Óptimo", "Presente", null, new DateTime(2026, 4, 23, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3559), 64.799999999999997, 0.0, 0, 33.899999999999999 },
                    { 54, 0, 0, 39, "Reina joven", "Alerta", "Cambiando", null, new DateTime(2026, 4, 25, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3562), 62.700000000000003, 0.0, 0, 37.799999999999997 },
                    { 55, 0, 0, 28, "Revisión de rutina", "Alerta", "Ausente", null, new DateTime(2026, 4, 26, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3564), 57.399999999999999, 0.0, 0, 37.100000000000001 },
                    { 56, 0, 0, 17, "Se alimentó", "Crítico", "Cambiando", null, new DateTime(2026, 4, 23, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3567), 55.5, 0.0, 0, 36.600000000000001 },
                    { 57, 0, 0, 26, "Se alimentó", "Crítico", "Presente", null, new DateTime(2026, 5, 6, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3578), 66.400000000000006, 0.0, 0, 31.300000000000001 },
                    { 58, 0, 0, 47, "Revisión de rutina", "Óptimo", "Presente", null, new DateTime(2026, 4, 10, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3581), 45.600000000000001, 0.0, 0, 36.5 },
                    { 59, 0, 0, 26, "Alta presencia de zánganos", "Crítico", "Ausente", null, new DateTime(2026, 5, 25, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3583), 58.299999999999997, 0.0, 0, 32.299999999999997 },
                    { 60, 0, 0, 37, "Se agregó cera", "Alerta", "Ausente", null, new DateTime(2026, 5, 7, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3585), 46.799999999999997, 0.0, 0, 31.300000000000001 },
                    { 61, 0, 0, 41, "Revisión de rutina", "Óptimo", "Cambiando", null, new DateTime(2026, 5, 16, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3588), 64.299999999999997, 0.0, 0, 31.100000000000001 },
                    { 62, 0, 0, 22, "Alta presencia de zánganos", "Crítico", "Ausente", null, new DateTime(2026, 6, 18, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3590), 41.399999999999999, 0.0, 0, 36.600000000000001 },
                    { 63, 0, 0, 16, "Se limpió piso", "Alerta", "Presente", null, new DateTime(2026, 4, 13, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3594), 61.600000000000001, 0.0, 0, 33.299999999999997 },
                    { 64, 0, 0, 29, "Se limpió piso", "Óptimo", "Cambiando", null, new DateTime(2026, 6, 17, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3596), 41.200000000000003, 0.0, 0, 36.600000000000001 },
                    { 65, 0, 0, 5, "Se agregó cera", "Alerta", "Presente", null, new DateTime(2026, 5, 29, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3598), 62.799999999999997, 0.0, 0, 32.700000000000003 },
                    { 66, 0, 0, 18, "Se alimentó", "Alerta", "Cambiando", null, new DateTime(2026, 4, 8, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3601), 53.899999999999999, 0.0, 0, 35.5 },
                    { 67, 0, 0, 34, "Revisión de rutina", "Crítico", "Ausente", null, new DateTime(2026, 4, 10, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3611), 65.099999999999994, 0.0, 0, 32.5 },
                    { 68, 0, 0, 50, "Se alimentó", "Crítico", "Presente", null, new DateTime(2026, 6, 17, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3614), 41.0, 0.0, 0, 34.899999999999999 },
                    { 69, 0, 0, 27, "Se limpió piso", "Alerta", "Cambiando", null, new DateTime(2026, 4, 22, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3617), 68.599999999999994, 0.0, 0, 31.300000000000001 },
                    { 70, 0, 0, 19, "Revisión de rutina", "Alerta", "Cambiando", null, new DateTime(2026, 5, 22, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3620), 67.700000000000003, 0.0, 0, 33.0 },
                    { 71, 0, 0, 36, "Revisión de rutina", "Óptimo", "Presente", null, new DateTime(2026, 4, 8, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3624), 68.099999999999994, 0.0, 0, 32.5 },
                    { 72, 0, 0, 7, "Reina joven", "Crítico", "Ausente", null, new DateTime(2026, 3, 23, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3627), 48.0, 0.0, 0, 33.0 },
                    { 73, 0, 0, 14, "Reina joven", "Crítico", "Presente", null, new DateTime(2026, 5, 14, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3630), 60.700000000000003, 0.0, 0, 36.399999999999999 },
                    { 74, 0, 0, 36, "Alta presencia de zánganos", "Crítico", "Cambiando", null, new DateTime(2026, 3, 30, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3633), 54.200000000000003, 0.0, 0, 35.600000000000001 },
                    { 75, 0, 0, 28, "Sin novedades", "Alerta", "Ausente", null, new DateTime(2026, 5, 5, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3636), 52.399999999999999, 0.0, 0, 34.899999999999999 },
                    { 76, 0, 0, 25, "Se limpió piso", "Crítico", "Presente", null, new DateTime(2026, 5, 7, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3640), 63.0, 0.0, 0, 32.799999999999997 },
                    { 77, 0, 0, 39, "Reina joven", "Crítico", "Presente", null, new DateTime(2026, 4, 26, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3651), 48.799999999999997, 0.0, 0, 33.299999999999997 },
                    { 78, 0, 0, 14, "Alta presencia de zánganos", "Crítico", "Presente", null, new DateTime(2026, 5, 17, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3654), 67.700000000000003, 0.0, 0, 34.5 },
                    { 79, 0, 0, 14, "Alta presencia de zánganos", "Crítico", "Ausente", null, new DateTime(2026, 4, 24, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3657), 63.899999999999999, 0.0, 0, 32.399999999999999 },
                    { 80, 0, 0, 34, "Se alimentó", "Alerta", "Presente", null, new DateTime(2026, 5, 3, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(3660), 62.700000000000003, 0.0, 0, 31.899999999999999 }
                });

            migrationBuilder.InsertData(
                table: "Treatments",
                columns: new[] { "Id", "ColmenaId", "Fecha", "Nota", "Tipo", "Titulo" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 10, 12, 14, 30, 0, 0, DateTimeKind.Unspecified), "Tratamiento por goteo. Dosis estándar 50ml por colmena. Temperatura ambiente 18°C.", "Medicinal", "Aplicación Ácido Oxálico" },
                    { 2, 1, new DateTime(2025, 8, 28, 9, 15, 0, 0, DateTimeKind.Unspecified), "Jarabe de azúcar 2:1. 2 Litros suministrados en alimentador de techo.", "Mantenimiento", "Alimentación de Soporte" },
                    { 3, 43, new DateTime(2026, 5, 18, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4700), "Tratamiento aplicado correctamente", "Medicinal", "Vitaminas" },
                    { 4, 9, new DateTime(2025, 12, 10, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4703), "Tratamiento aplicado correctamente", "Medicinal", "Aplicación Amitraz" },
                    { 5, 40, new DateTime(2026, 3, 18, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4704), "Tratamiento aplicado correctamente", "Medicinal", "Alimentación Proteica" },
                    { 6, 19, new DateTime(2026, 1, 5, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4706), "Tratamiento aplicado correctamente", "Preventivo", "Vitaminas" },
                    { 7, 1, new DateTime(2026, 1, 11, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4708), "Tratamiento aplicado correctamente", "Medicinal", "Alimentación Proteica" },
                    { 8, 45, new DateTime(2026, 4, 15, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4711), "Tratamiento aplicado correctamente", "Mantenimiento", "Revisión Sanitaria" },
                    { 9, 12, new DateTime(2026, 1, 25, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4724), "Tratamiento aplicado correctamente", "Medicinal", "Alimentación Proteica" },
                    { 10, 20, new DateTime(2026, 3, 23, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4726), "Tratamiento aplicado correctamente", "Medicinal", "Goteo Ácido" },
                    { 11, 5, new DateTime(2026, 5, 20, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4728), "Tratamiento aplicado correctamente", "Medicinal", "Alimentación Proteica" },
                    { 12, 15, new DateTime(2025, 12, 12, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4730), "Tratamiento aplicado correctamente", "Mantenimiento", "Aplicación Amitraz" },
                    { 13, 14, new DateTime(2026, 1, 4, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4731), "Tratamiento aplicado correctamente", "Preventivo", "Alimentación Proteica" },
                    { 14, 47, new DateTime(2026, 2, 20, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4734), "Tratamiento aplicado correctamente", "Mantenimiento", "Aplicación Amitraz" },
                    { 15, 48, new DateTime(2026, 5, 21, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4736), "Tratamiento aplicado correctamente", "Medicinal", "Goteo Ácido" },
                    { 16, 24, new DateTime(2026, 2, 3, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4739), "Tratamiento aplicado correctamente", "Medicinal", "Vitaminas" },
                    { 17, 28, new DateTime(2026, 1, 19, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4741), "Tratamiento aplicado correctamente", "Medicinal", "Vitaminas" },
                    { 18, 28, new DateTime(2026, 3, 31, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4743), "Tratamiento aplicado correctamente", "Preventivo", "Alimentación Proteica" },
                    { 19, 44, new DateTime(2026, 2, 8, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4745), "Tratamiento aplicado correctamente", "Mantenimiento", "Alimentación Proteica" },
                    { 20, 34, new DateTime(2026, 1, 8, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4747), "Tratamiento aplicado correctamente", "Preventivo", "Aplicación Amitraz" },
                    { 21, 4, new DateTime(2025, 12, 21, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4749), "Tratamiento aplicado correctamente", "Preventivo", "Goteo Ácido" },
                    { 22, 38, new DateTime(2026, 1, 10, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4751), "Tratamiento aplicado correctamente", "Preventivo", "Revisión Sanitaria" },
                    { 23, 10, new DateTime(2026, 5, 29, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4753), "Tratamiento aplicado correctamente", "Mantenimiento", "Goteo Ácido" },
                    { 24, 38, new DateTime(2026, 6, 10, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4755), "Tratamiento aplicado correctamente", "Medicinal", "Alimentación Proteica" },
                    { 25, 15, new DateTime(2026, 5, 27, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4765), "Tratamiento aplicado correctamente", "Mantenimiento", "Revisión Sanitaria" },
                    { 26, 4, new DateTime(2026, 2, 4, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4767), "Tratamiento aplicado correctamente", "Medicinal", "Revisión Sanitaria" },
                    { 27, 28, new DateTime(2026, 4, 28, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4769), "Tratamiento aplicado correctamente", "Mantenimiento", "Goteo Ácido" },
                    { 28, 47, new DateTime(2026, 2, 27, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4772), "Tratamiento aplicado correctamente", "Preventivo", "Revisión Sanitaria" },
                    { 29, 6, new DateTime(2026, 6, 7, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4773), "Tratamiento aplicado correctamente", "Preventivo", "Alimentación Proteica" },
                    { 30, 2, new DateTime(2026, 4, 16, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4775), "Tratamiento aplicado correctamente", "Medicinal", "Alimentación Proteica" },
                    { 31, 6, new DateTime(2026, 5, 30, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4778), "Tratamiento aplicado correctamente", "Medicinal", "Goteo Ácido" },
                    { 32, 47, new DateTime(2025, 12, 27, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4780), "Tratamiento aplicado correctamente", "Mantenimiento", "Goteo Ácido" },
                    { 33, 33, new DateTime(2026, 1, 20, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4782), "Tratamiento aplicado correctamente", "Mantenimiento", "Aplicación Amitraz" },
                    { 34, 10, new DateTime(2026, 4, 18, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4784), "Tratamiento aplicado correctamente", "Mantenimiento", "Aplicación Amitraz" },
                    { 35, 48, new DateTime(2026, 3, 11, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4785), "Tratamiento aplicado correctamente", "Medicinal", "Aplicación Amitraz" },
                    { 36, 32, new DateTime(2026, 6, 4, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4787), "Tratamiento aplicado correctamente", "Medicinal", "Revisión Sanitaria" },
                    { 37, 7, new DateTime(2026, 6, 11, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4789), "Tratamiento aplicado correctamente", "Medicinal", "Revisión Sanitaria" },
                    { 38, 27, new DateTime(2026, 5, 29, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4790), "Tratamiento aplicado correctamente", "Preventivo", "Revisión Sanitaria" },
                    { 39, 21, new DateTime(2026, 1, 27, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4793), "Tratamiento aplicado correctamente", "Medicinal", "Vitaminas" },
                    { 40, 41, new DateTime(2026, 5, 12, 14, 57, 15, 258, DateTimeKind.Local).AddTicks(4803), "Tratamiento aplicado correctamente", "Medicinal", "Alimentación Proteica" }
                });

            migrationBuilder.InsertData(
                table: "TreatmentEquipments",
                columns: new[] { "Id", "Cantidad", "EquipmentName", "TreatmentId" },
                values: new object[,]
                {
                    { 1, 1, "Ácido Oxálico (Glicerina)", 1 },
                    { 2, 1, "Tratamiento Vario", 2 },
                    { 3, 1, "Tratamiento Vario", 3 },
                    { 4, 1, "Tratamiento Vario", 4 },
                    { 5, 1, "Tratamiento Vario", 5 },
                    { 6, 1, "Tratamiento Vario", 6 },
                    { 7, 1, "Tratamiento Vario", 7 },
                    { 8, 1, "Tratamiento Vario", 8 },
                    { 9, 1, "Tratamiento Vario", 9 },
                    { 10, 1, "Tratamiento Vario", 10 },
                    { 11, 1, "Tratamiento Vario", 11 },
                    { 12, 1, "Tratamiento Vario", 12 },
                    { 13, 1, "Tratamiento Vario", 13 },
                    { 14, 1, "Tratamiento Vario", 14 },
                    { 15, 1, "Tratamiento Vario", 15 },
                    { 16, 1, "Tratamiento Vario", 16 },
                    { 17, 1, "Tratamiento Vario", 17 },
                    { 18, 1, "Tratamiento Vario", 18 },
                    { 19, 1, "Tratamiento Vario", 19 },
                    { 20, 1, "Tratamiento Vario", 20 },
                    { 21, 1, "Tratamiento Vario", 21 },
                    { 22, 1, "Tratamiento Vario", 22 },
                    { 23, 1, "Tratamiento Vario", 23 },
                    { 24, 1, "Tratamiento Vario", 24 },
                    { 25, 1, "Tratamiento Vario", 25 },
                    { 26, 1, "Tratamiento Vario", 26 },
                    { 27, 1, "Tratamiento Vario", 27 },
                    { 28, 1, "Tratamiento Vario", 28 },
                    { 29, 1, "Tratamiento Vario", 29 },
                    { 30, 1, "Tratamiento Vario", 30 },
                    { 31, 1, "Tratamiento Vario", 31 },
                    { 32, 1, "Tratamiento Vario", 32 },
                    { 33, 1, "Tratamiento Vario", 33 },
                    { 34, 1, "Tratamiento Vario", 34 },
                    { 35, 1, "Tratamiento Vario", 35 },
                    { 36, 1, "Tratamiento Vario", 36 },
                    { 37, 1, "Tratamiento Vario", 37 },
                    { 38, 1, "Tratamiento Vario", 38 },
                    { 39, 1, "Tratamiento Vario", 39 },
                    { 40, 1, "Tratamiento Vario", 40 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Colmenas_ApiarioId",
                table: "Colmenas",
                column: "ApiarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Extracciones_GananciaId",
                table: "Extracciones",
                column: "GananciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Ganancias_AnalisisId",
                table: "Ganancias",
                column: "AnalisisId");

            migrationBuilder.CreateIndex(
                name: "IX_Inversiones_AnalisisId",
                table: "Inversiones",
                column: "AnalisisId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_ApiarioDestinoId",
                table: "Movimientos",
                column: "ApiarioDestinoId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_ApiarioOrigenId",
                table: "Movimientos",
                column: "ApiarioOrigenId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_ColmenaId",
                table: "Movimientos",
                column: "ColmenaId");

            migrationBuilder.CreateIndex(
                name: "IX_NotasTecnicas_ColmenaId",
                table: "NotasTecnicas",
                column: "ColmenaId");

            migrationBuilder.CreateIndex(
                name: "IX_NotasTecnicas_ExtraccionId",
                table: "NotasTecnicas",
                column: "ExtraccionId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentEquipments_TreatmentId",
                table: "TreatmentEquipments",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_ColmenaId",
                table: "Treatments",
                column: "ColmenaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Declaraciones");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "Inversiones");

            migrationBuilder.DropTable(
                name: "LoginRecords");

            migrationBuilder.DropTable(
                name: "Movimientos");

            migrationBuilder.DropTable(
                name: "NotasTecnicas");

            migrationBuilder.DropTable(
                name: "TreatmentEquipments");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Extracciones");

            migrationBuilder.DropTable(
                name: "Treatments");

            migrationBuilder.DropTable(
                name: "Ganancias");

            migrationBuilder.DropTable(
                name: "Colmenas");

            migrationBuilder.DropTable(
                name: "Analisis");

            migrationBuilder.DropTable(
                name: "Apiarios");
        }
    }
}
