using Microsoft.EntityFrameworkCore;
using ObligatorioIntegrador2026.Models;

namespace ObligatorioIntegrador2026.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<LoginRecord> LoginRecords { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Apiario> Apiarios { get; set; }
        public DbSet<Colmena> Colmenas { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<TreatmentEquipment> TreatmentEquipments { get; set; }
        public DbSet<NotaTecnica> NotasTecnicas { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }
        public DbSet<Analisis> Analisis { get; set; }
        public DbSet<Inversion> Inversiones { get; set; }
        public DbSet<Ganancia> Ganancias { get; set; }
        public DbSet<DeclaracionJurada> Declaraciones { get; set; }
        public DbSet<Extraccion> Extracciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed inicial de usuarios
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "b.lopez", Password = "Admin123!", FullName = "Benjamin Lopez", Role = "Admin" },
                new User { Id = 2, Username = "f.alvarez", Password = "Admin123!", FullName = "Felipe Alvarez", Role = "Admin" },
                new User { Id = 3, Username = "m.verges", Password = "Matias123!", FullName = "Matías Verges", Role = "Beekeeper" }
            );

            // Seed inicial de Apiarios
            modelBuilder.Entity<Apiario>().HasData(
                new Apiario { Id = 1, Nombre = "Apiario Norte", StringIdentificador = "AP-001", UbicacionTexto = "Ruta 5, Km 42.5", UbicacionCoordenadas = "-34.123, -56.456", Responsable = "Benjamin Lopez", NotasEstado = "Acceso en buen estado." },
                new Apiario { Id = 2, Nombre = "Apiario Sur", StringIdentificador = "AP-002", UbicacionTexto = "Camino Vecinal 14", UbicacionCoordenadas = "-34.890, -56.123", Responsable = "Felipe Alvarez", NotasEstado = "Requiere desmalezado." },
                new Apiario { Id = 3, Nombre = "Apiario Este", StringIdentificador = "AP-003", UbicacionTexto = "Estancia La Paz", UbicacionCoordenadas = "-33.567, -55.890", Responsable = "Matías Verges", NotasEstado = "Todo normal." },
                new Apiario { Id = 4, Nombre = "Apiario Oeste", StringIdentificador = "AP-004", UbicacionTexto = "Ruta 3, Km 112", UbicacionCoordenadas = "-34.456, -57.123", Responsable = "Benjamin Lopez", NotasEstado = "Tranquera rota." },
                new Apiario { Id = 5, Nombre = "Apiario Central", StringIdentificador = "AP-005", UbicacionTexto = "Predio Principal", UbicacionCoordenadas = "-34.567, -56.789", Responsable = "Felipe Alvarez", NotasEstado = "Base operativa." }
            );

            // Seed inicial de equipamiento
            modelBuilder.Entity<Equipment>().HasData(
                new Equipment { Id = 1, Name = "Ahumador Inoxidable", Type = "Ahumador estándar 10x25cm", Stock = 12, Category = "Herramienta", LowThreshold = 5, MediumThreshold = 15, DisplayOrder = 1, UnitPrice = 45.0, Currency = "USD" },
                new Equipment { Id = 2, Name = "Palanca de Manejo", Type = "Pinza y palanca universal", Stock = 24, Category = "Herramienta", LowThreshold = 10, MediumThreshold = 20, DisplayOrder = 2, UnitPrice = 12.0, Currency = "USD" },
                new Equipment { Id = 3, Name = "Ácido Oxálico (Glicerina)", Type = "Tratamiento Varroa", Stock = 50, Category = "Medicamento", LowThreshold = 15, MediumThreshold = 40, DisplayOrder = 3, UnitPrice = 25.0, Currency = "USD" },
                new Equipment { Id = 4, Name = "Amitraz (Tiras)", Type = "Tratamiento Varroa", Stock = 15, Category = "Medicamento", LowThreshold = 10, MediumThreshold = 25, DisplayOrder = 4, UnitPrice = 30.0, Currency = "USD" },
                new Equipment { Id = 5, Name = "Alzas Melarias (Media)", Type = "Madera", Stock = 120, Category = "Material", LowThreshold = 30, MediumThreshold = 80, DisplayOrder = 5, UnitPrice = 250.0, Currency = "UYU" },
                new Equipment { Id = 6, Name = "Marcos Alambrados", Type = "Madera/Alambre", Stock = 450, Category = "Material", LowThreshold = 100, MediumThreshold = 200, DisplayOrder = 6, UnitPrice = 80.0, Currency = "UYU" },
                new Equipment { Id = 7, Name = "Cera Estampada", Type = "Cera Orgánica", Stock = 5, Category = "Material", LowThreshold = 10, MediumThreshold = 25, DisplayOrder = 7, UnitPrice = 120.0, Currency = "UYU" },
                new Equipment { Id = 8, Name = "Piso 73", Type = "Tipo 10", Stock = 472, Category = "Material", LowThreshold = 44, MediumThreshold = 93, DisplayOrder = 8, UnitPrice = 388.73, Currency = "UYU" },
                new Equipment { Id = 9, Name = "Fluvalinato 87", Type = "Tipo 4", Stock = 273, Category = "Medicamento", LowThreshold = 32, MediumThreshold = 109, DisplayOrder = 9, UnitPrice = 56.63, Currency = "USD" },
                new Equipment { Id = 10, Name = "Pinza 44", Type = "Tipo 5", Stock = 241, Category = "Herramienta", LowThreshold = 31, MediumThreshold = 108, DisplayOrder = 10, UnitPrice = 432.61, Currency = "USD" },
                new Equipment { Id = 11, Name = "Ahumador 44", Type = "Tipo 5", Stock = 242, Category = "Herramienta", LowThreshold = 29, MediumThreshold = 77, DisplayOrder = 11, UnitPrice = 283.15, Currency = "USD" },
                new Equipment { Id = 12, Name = "Guantes 45", Type = "Tipo 4", Stock = 126, Category = "Herramienta", LowThreshold = 22, MediumThreshold = 116, DisplayOrder = 12, UnitPrice = 383.42, Currency = "UYU" },
                new Equipment { Id = 13, Name = "Cera 89", Type = "Tipo 5", Stock = 483, Category = "Material", LowThreshold = 20, MediumThreshold = 36, DisplayOrder = 13, UnitPrice = 92.26, Currency = "USD" },
                new Equipment { Id = 14, Name = "Ahumador 30", Type = "Tipo 9", Stock = 80, Category = "Herramienta", LowThreshold = 37, MediumThreshold = 118, DisplayOrder = 14, UnitPrice = 177.53, Currency = "USD" },
                new Equipment { Id = 15, Name = "Alimentador 51", Type = "Tipo 2", Stock = 136, Category = "Material", LowThreshold = 11, MediumThreshold = 68, DisplayOrder = 15, UnitPrice = 69.57, Currency = "UYU" },
                new Equipment { Id = 16, Name = "Alza 59", Type = "Tipo 10", Stock = 62, Category = "Material", LowThreshold = 11, MediumThreshold = 76, DisplayOrder = 16, UnitPrice = 489.99, Currency = "USD" },
                new Equipment { Id = 17, Name = "Techo 55", Type = "Tipo 6", Stock = 478, Category = "Material", LowThreshold = 35, MediumThreshold = 60, DisplayOrder = 17, UnitPrice = 54.93, Currency = "UYU" },
                new Equipment { Id = 18, Name = "Guantes 21", Type = "Tipo 3", Stock = 159, Category = "Herramienta", LowThreshold = 41, MediumThreshold = 87, DisplayOrder = 18, UnitPrice = 41.64, Currency = "USD" },
                new Equipment { Id = 19, Name = "Cepillo 67", Type = "Tipo 9", Stock = 435, Category = "Herramienta", LowThreshold = 49, MediumThreshold = 95, DisplayOrder = 19, UnitPrice = 252.85, Currency = "USD" },
                new Equipment { Id = 20, Name = "Alza 76", Type = "Tipo 6", Stock = 5, Category = "Material", LowThreshold = 22, MediumThreshold = 32, DisplayOrder = 20, UnitPrice = 494.91, Currency = "UYU" },
                new Equipment { Id = 21, Name = "Piso 45", Type = "Tipo 10", Stock = 53, Category = "Material", LowThreshold = 28, MediumThreshold = 63, DisplayOrder = 21, UnitPrice = 440.13, Currency = "USD" },
                new Equipment { Id = 22, Name = "Amitraz 4", Type = "Tipo 2", Stock = 81, Category = "Medicamento", LowThreshold = 12, MediumThreshold = 51, DisplayOrder = 22, UnitPrice = 130.30, Currency = "UYU" },
                new Equipment { Id = 23, Name = "Pinza 35", Type = "Tipo 8", Stock = 440, Category = "Herramienta", LowThreshold = 31, MediumThreshold = 128, DisplayOrder = 23, UnitPrice = 11.02, Currency = "USD" },
                new Equipment { Id = 24, Name = "Techo 89", Type = "Tipo 2", Stock = 160, Category = "Material", LowThreshold = 5, MediumThreshold = 71, DisplayOrder = 24, UnitPrice = 467.96, Currency = "USD" },
                new Equipment { Id = 25, Name = "Piso 50", Type = "Tipo 5", Stock = 478, Category = "Material", LowThreshold = 45, MediumThreshold = 69, DisplayOrder = 25, UnitPrice = 364.36, Currency = "UYU" }
            );

            // Relaciones de Movimiento para evitar ciclos de cascada
            modelBuilder.Entity<Movimiento>().HasOne(m => m.ApiarioOrigen).WithMany().HasForeignKey(m => m.ApiarioOrigenId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Movimiento>().HasOne(m => m.ApiarioDestino).WithMany().HasForeignKey(m => m.ApiarioDestinoId).OnDelete(DeleteBehavior.Restrict);

            // Relaciones de Inversion y Ganancia con Analisis
            modelBuilder.Entity<Inversion>().HasOne(i => i.Analisis).WithMany(a => a.Inversiones).HasForeignKey(i => i.AnalisisId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Ganancia>().HasOne(g => g.Analisis).WithMany(a => a.Ganancias).HasForeignKey(g => g.AnalisisId).OnDelete(DeleteBehavior.Cascade);

            // Seed inicial de Colmenas
            modelBuilder.Entity<Colmena>().HasData(
                new Colmena { Id = 1, Identificador = "#HIVE-0042", CodigoEscaneo = "100001", ApiarioId = 1, Estado = "Óptimo", PesoKg = 45.2, TemperaturaInterna = 34.5, HumedadInterna = 55.0, ProduccionMielKg = 0.0, EsPiloto = true, EsNucleo = false, CantidadAbejas = 45000, UbicacionIntraApiario = "Fila 1, Pos 1", ComportamientoAbejas = "Dócil", EstadoReina = "Presente", Alzas = 0, MediasAlzas = 0, AlzasTresCuartos = 0 },
                new Colmena { Id = 2, Identificador = "#HIVE-0089", CodigoEscaneo = "100002", ApiarioId = 1, Estado = "Alerta", PesoKg = 42.8, TemperaturaInterna = 32.0, HumedadInterna = 0, ProduccionMielKg = 39.0, EsPiloto = false, EsNucleo = true, CantidadAbejas = 38000, UbicacionIntraApiario = "Fila 1, Pos 2", ComportamientoAbejas = "Defensivo", EstadoReina = "Presente", Alzas = 1, MediasAlzas = 0, AlzasTresCuartos = 1 },
                new Colmena { Id = 3, Identificador = "#HIVE-0112", CodigoEscaneo = "100003", ApiarioId = 2, Estado = "Crítico", PesoKg = 31.0, TemperaturaInterna = 36.5, HumedadInterna = 60.0, ProduccionMielKg = 22.0, EsPiloto = true, EsNucleo = false, CantidadAbejas = 15000, UbicacionIntraApiario = "Fila 2, Pos 1", ComportamientoAbejas = "Agresivo", EstadoReina = "Ausente", Alzas = 1, MediasAlzas = 0, AlzasTresCuartos = 0 },
                new Colmena { Id = 4, Identificador = "#HIVE-0045", CodigoEscaneo = "100004", ApiarioId = 2, Estado = "Óptimo", PesoKg = 48.1, TemperaturaInterna = 34.2, HumedadInterna = 58.0, ProduccionMielKg = 0.0, EsPiloto = true, EsNucleo = true, CantidadAbejas = 50000, UbicacionIntraApiario = "Fila 2, Pos 2", ComportamientoAbejas = "Dócil", EstadoReina = "Presente", Alzas = 0, MediasAlzas = 0, AlzasTresCuartos = 0 },
                new Colmena { Id = 5, Identificador = "#HIVE-0001", CodigoEscaneo = "100005", ApiarioId = 3, Estado = "Óptimo", PesoKg = 40.0, TemperaturaInterna = 35.1, HumedadInterna = 52.0, ProduccionMielKg = 39.0, EsPiloto = true, EsNucleo = false, CantidadAbejas = 42000, UbicacionIntraApiario = "Fila 1, Pos 1", ComportamientoAbejas = "Dócil", EstadoReina = "Presente", Alzas = 1, MediasAlzas = 0, AlzasTresCuartos = 1 },
                new Colmena { Id = 6, Identificador = "#HIVE-0002", CodigoEscaneo = "100006", ApiarioId = 4, Estado = "Óptimo", PesoKg = 39.5, TemperaturaInterna = 34.8, HumedadInterna = 0, ProduccionMielKg = 0.0, EsPiloto = false, EsNucleo = false, CantidadAbejas = 41000, UbicacionIntraApiario = "Fila 1, Pos 2", ComportamientoAbejas = "Dócil", EstadoReina = "Presente", Alzas = 0, MediasAlzas = 0, AlzasTresCuartos = 0 },
                new Colmena { Id = 7, Identificador = "#HIVE-0003", CodigoEscaneo = "100007", ApiarioId = 5, Estado = "Crítico", PesoKg = 25.0, TemperaturaInterna = 30.0, HumedadInterna = 82.0, ProduccionMielKg = 22.0, EsPiloto = true, EsNucleo = true, CantidadAbejas = 12000, UbicacionIntraApiario = "Única", ComportamientoAbejas = "Agresivo", EstadoReina = "Presente", Alzas = 1, MediasAlzas = 0, AlzasTresCuartos = 0 },
                new Colmena { Id = 8, Identificador = "#HIVE-0008", CodigoEscaneo = "100008", ApiarioId = 4, Estado = "Alerta", PesoKg = 52.0, TemperaturaInterna = 35.6, HumedadInterna = 53.2, ProduccionMielKg = 0.0, EsPiloto = false, EsNucleo = false, CantidadAbejas = 11201, UbicacionIntraApiario = "Fila 1, Pos 10", ComportamientoAbejas = "Agresivo", EstadoReina = "Cambiando", Alzas = 0, MediasAlzas = 0, AlzasTresCuartos = 0 },
                new Colmena { Id = 9, Identificador = "#HIVE-0009", CodigoEscaneo = "100009", ApiarioId = 3, Estado = "Alerta", PesoKg = 56.5, TemperaturaInterna = 35.5, HumedadInterna = 41.0, ProduccionMielKg = 22.0, EsPiloto = false, EsNucleo = false, CantidadAbejas = 55923, UbicacionIntraApiario = "Fila 1, Pos 10", ComportamientoAbejas = "Agresivo", EstadoReina = "Ausente", Alzas = 1, MediasAlzas = 0, AlzasTresCuartos = 0 },
                new Colmena { Id = 10, Identificador = "#HIVE-0010", CodigoEscaneo = "100010", ApiarioId = 3, Estado = "Óptimo", PesoKg = 43.7, TemperaturaInterna = 31.0, HumedadInterna = 52.2, ProduccionMielKg = 0.0, EsPiloto = false, EsNucleo = false, CantidadAbejas = 55082, UbicacionIntraApiario = "Fila 3, Pos 7", ComportamientoAbejas = "Defensivo", EstadoReina = "Ausente", Alzas = 0, MediasAlzas = 0, AlzasTresCuartos = 0 },
                new Colmena { Id = 11, Identificador = "#HIVE-0011", CodigoEscaneo = "100011", ApiarioId = 4, Estado = "Crítico", PesoKg = 32.4, TemperaturaInterna = 34.2, HumedadInterna = 60.7, ProduccionMielKg = 22.0, EsPiloto = false, EsNucleo = false, CantidadAbejas = 35199, UbicacionIntraApiario = "Fila 4, Pos 2", ComportamientoAbejas = "Dócil", EstadoReina = "Ausente", Alzas = 1, MediasAlzas = 0, AlzasTresCuartos = 0 },
                new Colmena { Id = 12, Identificador = "#HIVE-0012", CodigoEscaneo = "100012", ApiarioId = 5, Estado = "Crítico", PesoKg = 28.3, TemperaturaInterna = 34.7, HumedadInterna = 56.8, ProduccionMielKg = 22.0, EsPiloto = false, EsNucleo = false, CantidadAbejas = 27403, UbicacionIntraApiario = "Fila 2, Pos 10", ComportamientoAbejas = "Dócil", EstadoReina = "Ausente", Alzas = 1, MediasAlzas = 0, AlzasTresCuartos = 0 },
                new Colmena { Id = 13, Identificador = "#HIVE-0013", CodigoEscaneo = "100013", ApiarioId = 5, Estado = "Óptimo", PesoKg = 47.8, TemperaturaInterna = 35.6, HumedadInterna = 61.5, ProduccionMielKg = 17.0, EsPiloto = false, EsNucleo = false, CantidadAbejas = 55403, UbicacionIntraApiario = "Fila 3, Pos 5", ComportamientoAbejas = "Defensivo", EstadoReina = "Presente", Alzas = 0, MediasAlzas = 0, AlzasTresCuartos = 1 },
                new Colmena { Id = 14, Identificador = "#HIVE-0014", CodigoEscaneo = "100014", ApiarioId = 3, Estado = "Crítico", PesoKg = 25.3, TemperaturaInterna = 35.6, HumedadInterna = 61.7, ProduccionMielKg = 0.0, EsPiloto = false, EsNucleo = false, CantidadAbejas = 21796, UbicacionIntraApiario = "Fila 4, Pos 1", ComportamientoAbejas = "Dócil", EstadoReina = "Ausente", Alzas = 0, MediasAlzas = 0, AlzasTresCuartos = 0 },
                new Colmena { Id = 15, Identificador = "#HIVE-0015", CodigoEscaneo = "100015", ApiarioId = 1, Estado = "Óptimo", PesoKg = 43.8, TemperaturaInterna = 33.6, HumedadInterna = 43.5, ProduccionMielKg = 0.0, EsPiloto = false, EsNucleo = false, CantidadAbejas = 44489, UbicacionIntraApiario = "Fila 1, Pos 9", ComportamientoAbejas = "Agresivo", EstadoReina = "Ausente", Alzas = 0, MediasAlzas = 0, AlzasTresCuartos = 0 },
                new Colmena { Id = 16, Identificador = "#HIVE-0016", CodigoEscaneo = "100016", ApiarioId = 3, Estado = "Alerta", PesoKg = 21.7, TemperaturaInterna = 34.9, HumedadInterna = 55.8, ProduccionMielKg = 22.0, EsPiloto = false, EsNucleo = true, CantidadAbejas = 57003, UbicacionIntraApiario = "Fila 2, Pos 7", ComportamientoAbejas = "Agresivo", EstadoReina = "Presente", Alzas = 1, MediasAlzas = 0, AlzasTresCuartos = 0 },
                new Colmena { Id = 17, Identificador = "#HIVE-0017", CodigoEscaneo = "100017", ApiarioId = 2, Estado = "Crítico", PesoKg = 40.8, TemperaturaInterna = 36.8, HumedadInterna = 46.1, ProduccionMielKg = 22.0, EsPiloto = false, EsNucleo = false, CantidadAbejas = 51710, UbicacionIntraApiario = "Fila 1, Pos 6", ComportamientoAbejas = "Defensivo", EstadoReina = "Ausente", Alzas = 1, MediasAlzas = 0, AlzasTresCuartos = 0 },
                new Colmena { Id = 18, Identificador = "#HIVE-0018", CodigoEscaneo = "100018", ApiarioId = 1, Estado = "Alerta", PesoKg = 20.6, TemperaturaInterna = 30.3, HumedadInterna = 69.8, ProduccionMielKg = 0.0, EsPiloto = false, EsNucleo = false, CantidadAbejas = 30317, UbicacionIntraApiario = "Fila 2, Pos 3", ComportamientoAbejas = "Agresivo", EstadoReina = "Cambiando", Alzas = 0, MediasAlzas = 0, AlzasTresCuartos = 0 },
                new Colmena { Id = 19, Identificador = "#HIVE-0019", CodigoEscaneo = "100019", ApiarioId = 4, Estado = "Crítico", PesoKg = 58.8, TemperaturaInterna = 31.8, HumedadInterna = 67.8, ProduccionMielKg = 0.0, EsPiloto = false, EsNucleo = false, CantidadAbejas = 12828, UbicacionIntraApiario = "Fila 2, Pos 5", ComportamientoAbejas = "Dócil", EstadoReina = "Ausente", Alzas = 0, MediasAlzas = 0, AlzasTresCuartos = 0 },
                new Colmena { Id = 20, Identificador = "#HIVE-0020", CodigoEscaneo = "100020", ApiarioId = 3, Estado = "Crítico", PesoKg = 39.3, TemperaturaInterna = 37.5, HumedadInterna = 40.1, ProduccionMielKg = 0.0, EsPiloto = false, EsNucleo = false, CantidadAbejas = 36856, UbicacionIntraApiario = "Fila 4, Pos 8", ComportamientoAbejas = "Dócil", EstadoReina = "Cambiando", Alzas = 0, MediasAlzas = 0, AlzasTresCuartos = 0 },
                new Colmena { Id = 21, Identificador = "#HIVE-0021", CodigoEscaneo = "100021", ApiarioId = 1, Estado = "Crítico", PesoKg = 51.1, TemperaturaInterna = 33.9, HumedadInterna = 48.4, ProduccionMielKg = 0.0, EsPiloto = false, EsNucleo = false, CantidadAbejas = 52958, UbicacionIntraApiario = "Fila 4, Pos 1", ComportamientoAbejas = "Dócil", EstadoReina = "Presente", Alzas = 0, MediasAlzas = 0, AlzasTresCuartos = 0 },
                new Colmena { Id = 22, Identificador = "#HIVE-0022", CodigoEscaneo = "100022", ApiarioId = 1, Estado = "Crítico", PesoKg = 29.3, TemperaturaInterna = 31.8, HumedadInterna = 55.8, ProduccionMielKg = 22.0, EsPiloto = false, EsNucleo = false, CantidadAbejas = 26012, UbicacionIntraApiario = "Fila 1, Pos 5", ComportamientoAbejas = "Dócil", EstadoReina = "Cambiando", Alzas = 1, MediasAlzas = 0, AlzasTresCuartos = 0 },
                new Colmena { Id = 23, Identificador = "#HIVE-0023", CodigoEscaneo = "100023", ApiarioId = 2, Estado = "Crítico", PesoKg = 36.4, TemperaturaInterna = 32.1, HumedadInterna = 53.6, ProduccionMielKg = 46.0, EsPiloto = false, EsNucleo = false, CantidadAbejas = 40475, UbicacionIntraApiario = "Fila 4, Pos 1", ComportamientoAbejas = "Defensivo", EstadoReina = "Ausente", Alzas = 1, MediasAlzas = 2, AlzasTresCuartos = 0 },
                new Colmena { Id = 24, Identificador = "#HIVE-0024", CodigoEscaneo = "100024", ApiarioId = 5, Estado = "Óptimo", PesoKg = 49.0, TemperaturaInterna = 37.7, HumedadInterna = 42.8, ProduccionMielKg = 41.0, EsPiloto = false, EsNucleo = false, CantidadAbejas = 13287, UbicacionIntraApiario = "Fila 3, Pos 7", ComportamientoAbejas = "Agresivo", EstadoReina = "Ausente", Alzas = 0, MediasAlzas = 2, AlzasTresCuartos = 1 },
                new Colmena { Id = 25, Identificador = "#HIVE-0025", CodigoEscaneo = "100025", ApiarioId = 5, Estado = "Óptimo", PesoKg = 59.3, TemperaturaInterna = 31.4, HumedadInterna = 43.2, ProduccionMielKg = 22.0, EsPiloto = true, EsNucleo = false, CantidadAbejas = 55720, UbicacionIntraApiario = "Fila 1, Pos 2", ComportamientoAbejas = "Defensivo", EstadoReina = "Presente", Alzas = 1, MediasAlzas = 0, AlzasTresCuartos = 0 },
                new Colmena { Id = 26, Identificador = "#HIVE-0026", CodigoEscaneo = "100026", ApiarioId = 2, Estado = "Alerta", PesoKg = 57.5, TemperaturaInterna = 35.1, HumedadInterna = 54.7, ProduccionMielKg = 29.0, EsPiloto = false, EsNucleo = false, CantidadAbejas = 37187, UbicacionIntraApiario = "Fila 3, Pos 2", ComportamientoAbejas = "Dócil", EstadoReina = "Cambiando", Alzas = 0, MediasAlzas = 1, AlzasTresCuartos = 1 },
                new Colmena { Id = 27, Identificador = "#HIVE-0027", CodigoEscaneo = "100027", ApiarioId = 2, Estado = "Alerta", PesoKg = 46.0, TemperaturaInterna = 32.3, HumedadInterna = 67.9, ProduccionMielKg = 22.0, EsPiloto = true, EsNucleo = true, CantidadAbejas = 15033, UbicacionIntraApiario = "Fila 3, Pos 9", ComportamientoAbejas = "Agresivo", EstadoReina = "Cambiando", Alzas = 1, MediasAlzas = 0, AlzasTresCuartos = 0 },
                new Colmena { Id = 28, Identificador = "#HIVE-0028", CodigoEscaneo = "100028", ApiarioId = 5, Estado = "Óptimo", PesoKg = 32.4, TemperaturaInterna = 34.7, HumedadInterna = 59.4, ProduccionMielKg = 39.0, EsPiloto = true, EsNucleo = false, CantidadAbejas = 49191, UbicacionIntraApiario = "Fila 4, Pos 9", ComportamientoAbejas = "Defensivo", EstadoReina = "Ausente", Alzas = 1, MediasAlzas = 0, AlzasTresCuartos = 1 },
                new Colmena { Id = 29, Identificador = "#HIVE-0029", CodigoEscaneo = "100029", ApiarioId = 4, Estado = "Crítico", PesoKg = 59.1, TemperaturaInterna = 34.7, HumedadInterna = 66.5, ProduccionMielKg = 0.0, EsPiloto = false, EsNucleo = true, CantidadAbejas = 20521, UbicacionIntraApiario = "Fila 2, Pos 8", ComportamientoAbejas = "Dócil", EstadoReina = "Presente", Alzas = 0, MediasAlzas = 0, AlzasTresCuartos = 0 },
                new Colmena { Id = 30, Identificador = "#HIVE-0030", CodigoEscaneo = "100030", ApiarioId = 2, Estado = "Crítico", PesoKg = 38.2, TemperaturaInterna = 35.5, HumedadInterna = 42.2, ProduccionMielKg = 22.0, EsPiloto = true, EsNucleo = true, CantidadAbejas = 49262, UbicacionIntraApiario = "Fila 1, Pos 5", ComportamientoAbejas = "Defensivo", EstadoReina = "Presente", Alzas = 1, MediasAlzas = 0, AlzasTresCuartos = 0 },
                new Colmena { Id = 31, Identificador = "#HIVE-0031", CodigoEscaneo = "100031", ApiarioId = 3, Estado = "Alerta", PesoKg = 42.9, TemperaturaInterna = 36.6, HumedadInterna = 48.4, ProduccionMielKg = 12.0, EsPiloto = true, EsNucleo = true, CantidadAbejas = 58853, UbicacionIntraApiario = "Fila 2, Pos 7", ComportamientoAbejas = "Defensivo", EstadoReina = "Presente", Alzas = 0, MediasAlzas = 1, AlzasTresCuartos = 0 },
                new Colmena { Id = 32, Identificador = "#HIVE-0032", CodigoEscaneo = "100032", ApiarioId = 3, Estado = "Óptimo", PesoKg = 27.2, TemperaturaInterna = 36.6, HumedadInterna = 53.4, ProduccionMielKg = 0.0, EsPiloto = true, EsNucleo = false, CantidadAbejas = 18926, UbicacionIntraApiario = "Fila 4, Pos 7", ComportamientoAbejas = "Agresivo", EstadoReina = "Presente", Alzas = 0, MediasAlzas = 0, AlzasTresCuartos = 0 },
                new Colmena { Id = 33, Identificador = "#HIVE-0033", CodigoEscaneo = "100033", ApiarioId = 4, Estado = "Alerta", PesoKg = 23.8, TemperaturaInterna = 35.5, HumedadInterna = 58.8, ProduccionMielKg = 34.0, EsPiloto = false, EsNucleo = true, CantidadAbejas = 28734, UbicacionIntraApiario = "Fila 2, Pos 6", ComportamientoAbejas = "Defensivo", EstadoReina = "Cambiando", Alzas = 1, MediasAlzas = 1, AlzasTresCuartos = 0 },
                new Colmena { Id = 34, Identificador = "#HIVE-0034", CodigoEscaneo = "100034", ApiarioId = 4, Estado = "Óptimo", PesoKg = 32.6, TemperaturaInterna = 32.6, HumedadInterna = 68.4, ProduccionMielKg = 22.0, EsPiloto = true, EsNucleo = false, CantidadAbejas = 48819, UbicacionIntraApiario = "Fila 3, Pos 1", ComportamientoAbejas = "Defensivo", EstadoReina = "Ausente", Alzas = 1, MediasAlzas = 0, AlzasTresCuartos = 0 },
                new Colmena { Id = 35, Identificador = "#HIVE-0035", CodigoEscaneo = "100035", ApiarioId = 4, Estado = "Alerta", PesoKg = 32.5, TemperaturaInterna = 37.6, HumedadInterna = 41.8, ProduccionMielKg = 29.0, EsPiloto = true, EsNucleo = true, CantidadAbejas = 54686, UbicacionIntraApiario = "Fila 4, Pos 2", ComportamientoAbejas = "Defensivo", EstadoReina = "Cambiando", Alzas = 0, MediasAlzas = 1, AlzasTresCuartos = 1 },
                new Colmena { Id = 36, Identificador = "#HIVE-0036", CodigoEscaneo = "100036", ApiarioId = 1, Estado = "Alerta", PesoKg = 21.1, TemperaturaInterna = 35.1, HumedadInterna = 46.9, ProduccionMielKg = 0.0, EsPiloto = true, EsNucleo = false, CantidadAbejas = 16738, UbicacionIntraApiario = "Fila 2, Pos 7", ComportamientoAbejas = "Agresivo", EstadoReina = "Ausente", Alzas = 0, MediasAlzas = 0, AlzasTresCuartos = 0 },
                new Colmena { Id = 37, Identificador = "#HIVE-0037", CodigoEscaneo = "100037", ApiarioId = 4, Estado = "Óptimo", PesoKg = 50.1, TemperaturaInterna = 37.4, HumedadInterna = 44.6, ProduccionMielKg = 17.0, EsPiloto = false, EsNucleo = false, CantidadAbejas = 19321, UbicacionIntraApiario = "Fila 5, Pos 10", ComportamientoAbejas = "Dócil", EstadoReina = "Cambiando", Alzas = 0, MediasAlzas = 0, AlzasTresCuartos = 1 },
                new Colmena { Id = 38, Identificador = "#HIVE-0038", CodigoEscaneo = "100038", ApiarioId = 1, Estado = "Óptimo", PesoKg = 30.6, TemperaturaInterna = 33.5, HumedadInterna = 52.6, ProduccionMielKg = 46.0, EsPiloto = true, EsNucleo = false, CantidadAbejas = 36239, UbicacionIntraApiario = "Fila 1, Pos 8", ComportamientoAbejas = "Agresivo", EstadoReina = "Ausente", Alzas = 1, MediasAlzas = 2, AlzasTresCuartos = 0 },
                new Colmena { Id = 39, Identificador = "#HIVE-0039", CodigoEscaneo = "100039", ApiarioId = 3, Estado = "Crítico", PesoKg = 21.2, TemperaturaInterna = 33.6, HumedadInterna = 50.7, ProduccionMielKg = 0.0, EsPiloto = false, EsNucleo = false, CantidadAbejas = 51989, UbicacionIntraApiario = "Fila 4, Pos 1", ComportamientoAbejas = "Dócil", EstadoReina = "Ausente", Alzas = 0, MediasAlzas = 0, AlzasTresCuartos = 0 },
                new Colmena { Id = 40, Identificador = "#HIVE-0040", CodigoEscaneo = "100040", ApiarioId = 3, Estado = "Alerta", PesoKg = 36.0, TemperaturaInterna = 36.7, HumedadInterna = 68.5, ProduccionMielKg = 24.0, EsPiloto = false, EsNucleo = false, CantidadAbejas = 24633, UbicacionIntraApiario = "Fila 4, Pos 6", ComportamientoAbejas = "Defensivo", EstadoReina = "Ausente", Alzas = 0, MediasAlzas = 2, AlzasTresCuartos = 0 },
                new Colmena { Id = 41, Identificador = "#HIVE-0041", CodigoEscaneo = "100041", ApiarioId = 5, Estado = "Óptimo", PesoKg = 42.8, TemperaturaInterna = 35.2, HumedadInterna = 49.8, ProduccionMielKg = 41.0, EsPiloto = false, EsNucleo = true, CantidadAbejas = 46293, UbicacionIntraApiario = "Fila 2, Pos 4", ComportamientoAbejas = "Defensivo", EstadoReina = "Presente", Alzas = 0, MediasAlzas = 2, AlzasTresCuartos = 1 },
                new Colmena { Id = 42, Identificador = "#HIVE-0042", CodigoEscaneo = "100042", ApiarioId = 1, Estado = "Alerta", PesoKg = 41.0, TemperaturaInterna = 32.5, HumedadInterna = 55.9, ProduccionMielKg = 39.0, EsPiloto = false, EsNucleo = false, CantidadAbejas = 12573, UbicacionIntraApiario = "Fila 2, Pos 8", ComportamientoAbejas = "Dócil", EstadoReina = "Ausente", Alzas = 1, MediasAlzas = 0, AlzasTresCuartos = 1 },
                new Colmena { Id = 43, Identificador = "#HIVE-0043", CodigoEscaneo = "100043", ApiarioId = 4, Estado = "Crítico", PesoKg = 54.2, TemperaturaInterna = 37.0, HumedadInterna = 44.3, ProduccionMielKg = 22.0, EsPiloto = false, EsNucleo = false, CantidadAbejas = 22604, UbicacionIntraApiario = "Fila 5, Pos 10", ComportamientoAbejas = "Dócil", EstadoReina = "Ausente", Alzas = 1, MediasAlzas = 0, AlzasTresCuartos = 0 },
                new Colmena { Id = 44, Identificador = "#HIVE-0044", CodigoEscaneo = "100044", ApiarioId = 4, Estado = "Crítico", PesoKg = 24.0, TemperaturaInterna = 32.7, HumedadInterna = 41.1, ProduccionMielKg = 46.0, EsPiloto = true, EsNucleo = false, CantidadAbejas = 59848, UbicacionIntraApiario = "Fila 1, Pos 8", ComportamientoAbejas = "Dócil", EstadoReina = "Cambiando", Alzas = 1, MediasAlzas = 2, AlzasTresCuartos = 0 },
                new Colmena { Id = 45, Identificador = "#HIVE-0045", CodigoEscaneo = "100045", ApiarioId = 1, Estado = "Alerta", PesoKg = 22.1, TemperaturaInterna = 37.7, HumedadInterna = 59.7, ProduccionMielKg = 12.0, EsPiloto = true, EsNucleo = false, CantidadAbejas = 44767, UbicacionIntraApiario = "Fila 3, Pos 2", ComportamientoAbejas = "Dócil", EstadoReina = "Ausente", Alzas = 0, MediasAlzas = 1, AlzasTresCuartos = 0 },
                new Colmena { Id = 46, Identificador = "#HIVE-0046", CodigoEscaneo = "100046", ApiarioId = 4, Estado = "Crítico", PesoKg = 23.9, TemperaturaInterna = 33.3, HumedadInterna = 59.9, ProduccionMielKg = 22.0, EsPiloto = false, EsNucleo = true, CantidadAbejas = 56454, UbicacionIntraApiario = "Fila 2, Pos 4", ComportamientoAbejas = "Defensivo", EstadoReina = "Presente", Alzas = 1, MediasAlzas = 0, AlzasTresCuartos = 0 },
                new Colmena { Id = 47, Identificador = "#HIVE-0047", CodigoEscaneo = "100047", ApiarioId = 4, Estado = "Crítico", PesoKg = 57.2, TemperaturaInterna = 35.2, HumedadInterna = 63.2, ProduccionMielKg = 0.0, EsPiloto = false, EsNucleo = true, CantidadAbejas = 40516, UbicacionIntraApiario = "Fila 2, Pos 6", ComportamientoAbejas = "Dócil", EstadoReina = "Ausente", Alzas = 0, MediasAlzas = 0, AlzasTresCuartos = 0 },
                new Colmena { Id = 48, Identificador = "#HIVE-0048", CodigoEscaneo = "100048", ApiarioId = 1, Estado = "Alerta", PesoKg = 38.3, TemperaturaInterna = 35.6, HumedadInterna = 64.3, ProduccionMielKg = 22.0, EsPiloto = false, EsNucleo = false, CantidadAbejas = 36180, UbicacionIntraApiario = "Fila 3, Pos 4", ComportamientoAbejas = "Agresivo", EstadoReina = "Cambiando", Alzas = 1, MediasAlzas = 0, AlzasTresCuartos = 0 },
                new Colmena { Id = 49, Identificador = "#HIVE-0049", CodigoEscaneo = "100049", ApiarioId = 4, Estado = "Crítico", PesoKg = 30.5, TemperaturaInterna = 34.3, HumedadInterna = 48.9, ProduccionMielKg = 34.0, EsPiloto = true, EsNucleo = false, CantidadAbejas = 16233, UbicacionIntraApiario = "Fila 3, Pos 3", ComportamientoAbejas = "Defensivo", EstadoReina = "Presente", Alzas = 1, MediasAlzas = 1, AlzasTresCuartos = 0 },
                new Colmena { Id = 50, Identificador = "#HIVE-0050", CodigoEscaneo = "100050", ApiarioId = 5, Estado = "Óptimo", PesoKg = 47.6, TemperaturaInterna = 36.5, HumedadInterna = 43.9, ProduccionMielKg = 34.0, EsPiloto = false, EsNucleo = false, CantidadAbejas = 20332, UbicacionIntraApiario = "Fila 3, Pos 3", ComportamientoAbejas = "Defensivo", EstadoReina = "Ausente", Alzas = 1, MediasAlzas = 1, AlzasTresCuartos = 0 }
            );

            // Seed inicial de Extracciones
            modelBuilder.Entity<Extraccion>().HasData(
                new Extraccion { Id = 1, Fecha = DateTime.Now.AddMonths(-0).AddDays(-DateTime.Now.Day + 15), KilosTotales = 302.5, CantidadColmenasCosechadas = 19, Notas = "Cosecha mensual general" },
                new Extraccion { Id = 2, Fecha = DateTime.Now.AddMonths(-1).AddDays(-DateTime.Now.Day + 15), KilosTotales = 103.8, CantidadColmenasCosechadas = 6, Notas = "Cosecha mensual general" },
                new Extraccion { Id = 3, Fecha = DateTime.Now.AddMonths(-2).AddDays(-DateTime.Now.Day + 15), KilosTotales = 214.7, CantidadColmenasCosechadas = 12, Notas = "Cosecha mensual general" },
                new Extraccion { Id = 4, Fecha = DateTime.Now.AddMonths(-3).AddDays(-DateTime.Now.Day + 15), KilosTotales = 210.7, CantidadColmenasCosechadas = 6, Notas = "Cosecha mensual general" },
                new Extraccion { Id = 5, Fecha = DateTime.Now.AddMonths(-4).AddDays(-DateTime.Now.Day + 15), KilosTotales = 213.0, CantidadColmenasCosechadas = 16, Notas = "Cosecha mensual general" },
                new Extraccion { Id = 6, Fecha = DateTime.Now.AddMonths(-5).AddDays(-DateTime.Now.Day + 15), KilosTotales = 274.3, CantidadColmenasCosechadas = 19, Notas = "Cosecha mensual general" }
            );

            // Seed inicial de Notas Tecnicas
            modelBuilder.Entity<NotaTecnica>().HasData(
                new NotaTecnica { Id = 1, ColmenaId = 1, Detalles = "Revisión general, todo normal.", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-10), Temperatura = 34.5, Humedad = 55.0 },
                new NotaTecnica { Id = 2, ColmenaId = 2, Detalles = "Abejas defensivas, observar.", EstadoReina = "Presente", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-15), Temperatura = 35.0, Humedad = 60.0 },
                new NotaTecnica { Id = 3, ColmenaId = 3, Detalles = "Reina no avistada. Posible enjambrazón.", EstadoReina = "Ausente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-2), Temperatura = 33.2, Humedad = 50.0 },
                new NotaTecnica { Id = 4, ColmenaId = 4, Detalles = "Excelente producción.", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-5), Temperatura = 34.8, Humedad = 56.0 },
                new NotaTecnica { Id = 5, ColmenaId = 5, Detalles = "Cosecha registrada en masa.", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-7), ExtraccionId = 1, AlzasCosechadas = 2, KilosCosechados = 44.0 },
                new NotaTecnica { Id = 6, ColmenaId = 6, Detalles = "Normal.", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-20), Temperatura = 35.1, Humedad = 58.0 },
                new NotaTecnica { Id = 7, ColmenaId = 7, Detalles = "Humedad alta.", EstadoReina = "Presente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-35), Temperatura = 32.0, Humedad = 80.0 },
                new NotaTecnica { Id = 8, ColmenaId = 7, Detalles = "Se alimentó", EstadoReina = "Cambiando", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-30), Temperatura = 33.1, Humedad = 61.4 },
                new NotaTecnica { Id = 9, ColmenaId = 4, Detalles = "Revisión de rutina", EstadoReina = "Cambiando", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-93), Temperatura = 31.4, Humedad = 48.7 },
                new NotaTecnica { Id = 10, ColmenaId = 36, Detalles = "Revisión de rutina", EstadoReina = "Cambiando", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-33), Temperatura = 33.6, Humedad = 63.2 },
                new NotaTecnica { Id = 11, ColmenaId = 16, Detalles = "Se limpió piso", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-87), Temperatura = 37.7, Humedad = 61.2 },
                new NotaTecnica { Id = 12, ColmenaId = 23, Detalles = "Reina joven", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-65), Temperatura = 33.8, Humedad = 45.1 },
                new NotaTecnica { Id = 13, ColmenaId = 3, Detalles = "Sin novedades", EstadoReina = "Cambiando", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-9), Temperatura = 36.7, Humedad = 68.1 },
                new NotaTecnica { Id = 14, ColmenaId = 45, Detalles = "Alta presencia de zánganos", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-25), Temperatura = 35.7, Humedad = 56.2 },
                new NotaTecnica { Id = 15, ColmenaId = 11, Detalles = "Se agregó cera", EstadoReina = "Cambiando", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-64), Temperatura = 31.7, Humedad = 45.1 },
                new NotaTecnica { Id = 16, ColmenaId = 22, Detalles = "Alta presencia de zánganos", EstadoReina = "Ausente", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-38), Temperatura = 35.1, Humedad = 54.2 },
                new NotaTecnica { Id = 17, ColmenaId = 1, Detalles = "Sin novedades", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-53), Temperatura = 34.9, Humedad = 51.6 },
                new NotaTecnica { Id = 18, ColmenaId = 42, Detalles = "Reina joven", EstadoReina = "Cambiando", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-49), Temperatura = 32.3, Humedad = 53.1 },
                new NotaTecnica { Id = 19, ColmenaId = 6, Detalles = "Alta presencia de zánganos", EstadoReina = "Presente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-3), Temperatura = 38.0, Humedad = 55.0 },
                new NotaTecnica { Id = 20, ColmenaId = 24, Detalles = "Reina joven", EstadoReina = "Presente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-45), Temperatura = 31.4, Humedad = 63.3 },
                new NotaTecnica { Id = 21, ColmenaId = 42, Detalles = "Revisión de rutina", EstadoReina = "Cambiando", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-51), Temperatura = 36.0, Humedad = 66.7 },
                new NotaTecnica { Id = 22, ColmenaId = 11, Detalles = "Sin novedades", EstadoReina = "Cambiando", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-35), Temperatura = 30.9, Humedad = 41.9 },
                new NotaTecnica { Id = 23, ColmenaId = 31, Detalles = "Se alimentó", EstadoReina = "Ausente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-12), Temperatura = 36.1, Humedad = 45.6 },
                new NotaTecnica { Id = 24, ColmenaId = 22, Detalles = "Sin novedades", EstadoReina = "Cambiando", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-95), Temperatura = 34.6, Humedad = 65.2 },
                new NotaTecnica { Id = 25, ColmenaId = 25, Detalles = "Se agregó cera", EstadoReina = "Ausente", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-23), Temperatura = 31.9, Humedad = 67.0 },
                new NotaTecnica { Id = 26, ColmenaId = 21, Detalles = "Se alimentó", EstadoReina = "Cambiando", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-11), Temperatura = 36.0, Humedad = 50.2 },
                new NotaTecnica { Id = 27, ColmenaId = 40, Detalles = "Se alimentó", EstadoReina = "Ausente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-48), Temperatura = 30.9, Humedad = 58.6 },
                new NotaTecnica { Id = 28, ColmenaId = 50, Detalles = "Se limpió piso", EstadoReina = "Presente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-73), Temperatura = 34.1, Humedad = 47.9 },
                new NotaTecnica { Id = 29, ColmenaId = 35, Detalles = "Se alimentó", EstadoReina = "Cambiando", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-17), Temperatura = 32.6, Humedad = 46.9 },
                new NotaTecnica { Id = 30, ColmenaId = 13, Detalles = "Alta presencia de zánganos", EstadoReina = "Presente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-1), Temperatura = 30.4, Humedad = 55.5 },
                new NotaTecnica { Id = 31, ColmenaId = 45, Detalles = "Revisión de rutina", EstadoReina = "Ausente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-99), Temperatura = 31.2, Humedad = 59.8 },
                new NotaTecnica { Id = 32, ColmenaId = 44, Detalles = "Reina joven", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-73), Temperatura = 35.0, Humedad = 63.2 },
                new NotaTecnica { Id = 33, ColmenaId = 27, Detalles = "Sin novedades", EstadoReina = "Presente", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-42), Temperatura = 37.9, Humedad = 53.1 },
                new NotaTecnica { Id = 34, ColmenaId = 9, Detalles = "Alta presencia de zánganos", EstadoReina = "Cambiando", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-15), Temperatura = 30.4, Humedad = 57.0 },
                new NotaTecnica { Id = 35, ColmenaId = 13, Detalles = "Sin novedades", EstadoReina = "Presente", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-68), Temperatura = 32.0, Humedad = 46.3 },
                new NotaTecnica { Id = 36, ColmenaId = 34, Detalles = "Se limpió piso", EstadoReina = "Presente", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-91), Temperatura = 36.3, Humedad = 56.5 },
                new NotaTecnica { Id = 37, ColmenaId = 26, Detalles = "Se limpió piso", EstadoReina = "Ausente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-70), Temperatura = 36.9, Humedad = 63.5 },
                new NotaTecnica { Id = 38, ColmenaId = 7, Detalles = "Reina joven", EstadoReina = "Cambiando", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-50), Temperatura = 33.5, Humedad = 60.3 },
                new NotaTecnica { Id = 39, ColmenaId = 49, Detalles = "Alta presencia de zánganos", EstadoReina = "Cambiando", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-61), Temperatura = 34.8, Humedad = 64.0 },
                new NotaTecnica { Id = 40, ColmenaId = 10, Detalles = "Se alimentó", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-38), Temperatura = 34.7, Humedad = 60.9 },
                new NotaTecnica { Id = 41, ColmenaId = 13, Detalles = "Se limpió piso", EstadoReina = "Ausente", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-99), Temperatura = 36.0, Humedad = 61.0 },
                new NotaTecnica { Id = 42, ColmenaId = 22, Detalles = "Alta presencia de zánganos", EstadoReina = "Cambiando", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-19), Temperatura = 33.6, Humedad = 47.3 },
                new NotaTecnica { Id = 43, ColmenaId = 49, Detalles = "Reina joven", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-35), Temperatura = 37.2, Humedad = 69.8 },
                new NotaTecnica { Id = 44, ColmenaId = 44, Detalles = "Se limpió piso", EstadoReina = "Presente", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-90), Temperatura = 36.6, Humedad = 67.9 },
                new NotaTecnica { Id = 45, ColmenaId = 50, Detalles = "Sin novedades", EstadoReina = "Cambiando", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-64), Temperatura = 37.0, Humedad = 62.5 },
                new NotaTecnica { Id = 46, ColmenaId = 31, Detalles = "Alta presencia de zánganos", EstadoReina = "Cambiando", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-2), Temperatura = 34.3, Humedad = 65.0 },
                new NotaTecnica { Id = 47, ColmenaId = 10, Detalles = "Reina joven", EstadoReina = "Cambiando", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-18), Temperatura = 37.5, Humedad = 47.2 },
                new NotaTecnica { Id = 48, ColmenaId = 19, Detalles = "Revisión de rutina", EstadoReina = "Presente", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-62), Temperatura = 30.7, Humedad = 48.0 },
                new NotaTecnica { Id = 49, ColmenaId = 37, Detalles = "Revisión de rutina", EstadoReina = "Ausente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-70), Temperatura = 31.8, Humedad = 60.9 },
                new NotaTecnica { Id = 50, ColmenaId = 24, Detalles = "Se alimentó", EstadoReina = "Cambiando", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-90), Temperatura = 37.5, Humedad = 41.9 },
                new NotaTecnica { Id = 51, ColmenaId = 7, Detalles = "Se alimentó", EstadoReina = "Ausente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-51), Temperatura = 37.3, Humedad = 48.2 },
                new NotaTecnica { Id = 52, ColmenaId = 9, Detalles = "Se agregó cera", EstadoReina = "Ausente", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-22), Temperatura = 31.5, Humedad = 51.5 },
                new NotaTecnica { Id = 53, ColmenaId = 3, Detalles = "Se alimentó", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-65), Temperatura = 33.9, Humedad = 64.8 },
                new NotaTecnica { Id = 54, ColmenaId = 39, Detalles = "Reina joven", EstadoReina = "Cambiando", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-63), Temperatura = 37.8, Humedad = 62.7 },
                new NotaTecnica { Id = 55, ColmenaId = 28, Detalles = "Revisión de rutina", EstadoReina = "Ausente", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-62), Temperatura = 37.1, Humedad = 57.4 },
                new NotaTecnica { Id = 56, ColmenaId = 17, Detalles = "Se alimentó", EstadoReina = "Cambiando", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-65), Temperatura = 36.6, Humedad = 55.5 },
                new NotaTecnica { Id = 57, ColmenaId = 26, Detalles = "Se alimentó", EstadoReina = "Presente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-52), Temperatura = 31.3, Humedad = 66.4 },
                new NotaTecnica { Id = 58, ColmenaId = 47, Detalles = "Revisión de rutina", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-78), Temperatura = 36.5, Humedad = 45.6 },
                new NotaTecnica { Id = 59, ColmenaId = 26, Detalles = "Alta presencia de zánganos", EstadoReina = "Ausente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-33), Temperatura = 32.3, Humedad = 58.3 },
                new NotaTecnica { Id = 60, ColmenaId = 37, Detalles = "Se agregó cera", EstadoReina = "Ausente", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-51), Temperatura = 31.3, Humedad = 46.8 },
                new NotaTecnica { Id = 61, ColmenaId = 41, Detalles = "Revisión de rutina", EstadoReina = "Cambiando", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-42), Temperatura = 31.1, Humedad = 64.3 },
                new NotaTecnica { Id = 62, ColmenaId = 22, Detalles = "Alta presencia de zánganos", EstadoReina = "Ausente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-9), Temperatura = 36.6, Humedad = 41.4 },
                new NotaTecnica { Id = 63, ColmenaId = 16, Detalles = "Se limpió piso", EstadoReina = "Presente", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-75), Temperatura = 33.3, Humedad = 61.6 },
                new NotaTecnica { Id = 64, ColmenaId = 29, Detalles = "Se limpió piso", EstadoReina = "Cambiando", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-10), Temperatura = 36.6, Humedad = 41.2 },
                new NotaTecnica { Id = 65, ColmenaId = 5, Detalles = "Se agregó cera", EstadoReina = "Presente", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-29), Temperatura = 32.7, Humedad = 62.8 },
                new NotaTecnica { Id = 66, ColmenaId = 18, Detalles = "Se alimentó", EstadoReina = "Cambiando", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-80), Temperatura = 35.5, Humedad = 53.9 },
                new NotaTecnica { Id = 67, ColmenaId = 34, Detalles = "Revisión de rutina", EstadoReina = "Ausente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-78), Temperatura = 32.5, Humedad = 65.1 },
                new NotaTecnica { Id = 68, ColmenaId = 50, Detalles = "Se alimentó", EstadoReina = "Presente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-10), Temperatura = 34.9, Humedad = 41.0 },
                new NotaTecnica { Id = 69, ColmenaId = 27, Detalles = "Se limpió piso", EstadoReina = "Cambiando", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-66), Temperatura = 31.3, Humedad = 68.6 },
                new NotaTecnica { Id = 70, ColmenaId = 19, Detalles = "Revisión de rutina", EstadoReina = "Cambiando", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-36), Temperatura = 33.0, Humedad = 67.7 },
                new NotaTecnica { Id = 71, ColmenaId = 36, Detalles = "Revisión de rutina", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-80), Temperatura = 32.5, Humedad = 68.1 },
                new NotaTecnica { Id = 72, ColmenaId = 7, Detalles = "Reina joven", EstadoReina = "Ausente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-96), Temperatura = 33.0, Humedad = 48.0 },
                new NotaTecnica { Id = 73, ColmenaId = 14, Detalles = "Reina joven", EstadoReina = "Presente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-44), Temperatura = 36.4, Humedad = 60.7 },
                new NotaTecnica { Id = 74, ColmenaId = 36, Detalles = "Alta presencia de zánganos", EstadoReina = "Cambiando", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-89), Temperatura = 35.6, Humedad = 54.2 },
                new NotaTecnica { Id = 75, ColmenaId = 28, Detalles = "Sin novedades", EstadoReina = "Ausente", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-53), Temperatura = 34.9, Humedad = 52.4 },
                new NotaTecnica { Id = 76, ColmenaId = 25, Detalles = "Se limpió piso", EstadoReina = "Presente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-51), Temperatura = 32.8, Humedad = 63.0 },
                new NotaTecnica { Id = 77, ColmenaId = 39, Detalles = "Reina joven", EstadoReina = "Presente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-62), Temperatura = 33.3, Humedad = 48.8 },
                new NotaTecnica { Id = 78, ColmenaId = 14, Detalles = "Alta presencia de zánganos", EstadoReina = "Presente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-41), Temperatura = 34.5, Humedad = 67.7 },
                new NotaTecnica { Id = 79, ColmenaId = 14, Detalles = "Alta presencia de zánganos", EstadoReina = "Ausente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-64), Temperatura = 32.4, Humedad = 63.9 },
                new NotaTecnica { Id = 80, ColmenaId = 34, Detalles = "Se alimentó", EstadoReina = "Presente", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-55), Temperatura = 31.9, Humedad = 62.7 }
            );

            // Seed inicial de Treatments y TreatmentEquipments
            modelBuilder.Entity<Treatment>().HasData(
                new Treatment { Id = 1, ColmenaId = 1, Titulo = "Aplicación Ácido Oxálico", Tipo = "Medicinal", Nota = "Tratamiento por goteo. Dosis estándar 50ml por colmena. Temperatura ambiente 18°C.", Fecha = new DateTime(2025, 10, 12, 14, 30, 0) },
                new Treatment { Id = 2, ColmenaId = 1, Titulo = "Alimentación de Soporte", Tipo = "Mantenimiento", Nota = "Jarabe de azúcar 2:1. 2 Litros suministrados en alimentador de techo.", Fecha = new DateTime(2025, 08, 28, 9, 15, 0) },
                new Treatment { Id = 3, ColmenaId = 43, Titulo = "Vitaminas", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-40) },
                new Treatment { Id = 4, ColmenaId = 9, Titulo = "Aplicación Amitraz", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-199) },
                new Treatment { Id = 5, ColmenaId = 40, Titulo = "Alimentación Proteica", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-101) },
                new Treatment { Id = 6, ColmenaId = 19, Titulo = "Vitaminas", Tipo = "Preventivo", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-173) },
                new Treatment { Id = 7, ColmenaId = 1, Titulo = "Alimentación Proteica", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-167) },
                new Treatment { Id = 8, ColmenaId = 45, Titulo = "Revisión Sanitaria", Tipo = "Mantenimiento", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-73) },
                new Treatment { Id = 9, ColmenaId = 12, Titulo = "Alimentación Proteica", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-153) },
                new Treatment { Id = 10, ColmenaId = 20, Titulo = "Goteo Ácido", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-96) },
                new Treatment { Id = 11, ColmenaId = 5, Titulo = "Alimentación Proteica", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-38) },
                new Treatment { Id = 12, ColmenaId = 15, Titulo = "Aplicación Amitraz", Tipo = "Mantenimiento", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-197) },
                new Treatment { Id = 13, ColmenaId = 14, Titulo = "Alimentación Proteica", Tipo = "Preventivo", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-174) },
                new Treatment { Id = 14, ColmenaId = 47, Titulo = "Aplicación Amitraz", Tipo = "Mantenimiento", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-127) },
                new Treatment { Id = 15, ColmenaId = 48, Titulo = "Goteo Ácido", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-37) },
                new Treatment { Id = 16, ColmenaId = 24, Titulo = "Vitaminas", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-144) },
                new Treatment { Id = 17, ColmenaId = 28, Titulo = "Vitaminas", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-159) },
                new Treatment { Id = 18, ColmenaId = 28, Titulo = "Alimentación Proteica", Tipo = "Preventivo", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-88) },
                new Treatment { Id = 19, ColmenaId = 44, Titulo = "Alimentación Proteica", Tipo = "Mantenimiento", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-139) },
                new Treatment { Id = 20, ColmenaId = 34, Titulo = "Aplicación Amitraz", Tipo = "Preventivo", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-170) },
                new Treatment { Id = 21, ColmenaId = 4, Titulo = "Goteo Ácido", Tipo = "Preventivo", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-188) },
                new Treatment { Id = 22, ColmenaId = 38, Titulo = "Revisión Sanitaria", Tipo = "Preventivo", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-168) },
                new Treatment { Id = 23, ColmenaId = 10, Titulo = "Goteo Ácido", Tipo = "Mantenimiento", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-29) },
                new Treatment { Id = 24, ColmenaId = 38, Titulo = "Alimentación Proteica", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-17) },
                new Treatment { Id = 25, ColmenaId = 15, Titulo = "Revisión Sanitaria", Tipo = "Mantenimiento", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-31) },
                new Treatment { Id = 26, ColmenaId = 4, Titulo = "Revisión Sanitaria", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-143) },
                new Treatment { Id = 27, ColmenaId = 28, Titulo = "Goteo Ácido", Tipo = "Mantenimiento", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-60) },
                new Treatment { Id = 28, ColmenaId = 47, Titulo = "Revisión Sanitaria", Tipo = "Preventivo", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-120) },
                new Treatment { Id = 29, ColmenaId = 6, Titulo = "Alimentación Proteica", Tipo = "Preventivo", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-20) },
                new Treatment { Id = 30, ColmenaId = 2, Titulo = "Alimentación Proteica", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-72) },
                new Treatment { Id = 31, ColmenaId = 6, Titulo = "Goteo Ácido", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-28) },
                new Treatment { Id = 32, ColmenaId = 47, Titulo = "Goteo Ácido", Tipo = "Mantenimiento", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-182) },
                new Treatment { Id = 33, ColmenaId = 33, Titulo = "Aplicación Amitraz", Tipo = "Mantenimiento", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-158) },
                new Treatment { Id = 34, ColmenaId = 10, Titulo = "Aplicación Amitraz", Tipo = "Mantenimiento", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-70) },
                new Treatment { Id = 35, ColmenaId = 48, Titulo = "Aplicación Amitraz", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-108) },
                new Treatment { Id = 36, ColmenaId = 32, Titulo = "Revisión Sanitaria", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-23) },
                new Treatment { Id = 37, ColmenaId = 7, Titulo = "Revisión Sanitaria", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-16) },
                new Treatment { Id = 38, ColmenaId = 27, Titulo = "Revisión Sanitaria", Tipo = "Preventivo", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-29) },
                new Treatment { Id = 39, ColmenaId = 21, Titulo = "Vitaminas", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-151) },
                new Treatment { Id = 40, ColmenaId = 41, Titulo = "Alimentación Proteica", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-46) }
            );

            modelBuilder.Entity<TreatmentEquipment>().HasData(
                new TreatmentEquipment { Id = 1, TreatmentId = 1, EquipmentName = "Ácido Oxálico (Glicerina)", Cantidad = 1 },
                new TreatmentEquipment { Id = 2, TreatmentId = 2, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 3, TreatmentId = 3, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 4, TreatmentId = 4, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 5, TreatmentId = 5, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 6, TreatmentId = 6, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 7, TreatmentId = 7, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 8, TreatmentId = 8, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 9, TreatmentId = 9, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 10, TreatmentId = 10, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 11, TreatmentId = 11, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 12, TreatmentId = 12, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 13, TreatmentId = 13, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 14, TreatmentId = 14, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 15, TreatmentId = 15, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 16, TreatmentId = 16, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 17, TreatmentId = 17, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 18, TreatmentId = 18, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 19, TreatmentId = 19, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 20, TreatmentId = 20, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 21, TreatmentId = 21, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 22, TreatmentId = 22, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 23, TreatmentId = 23, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 24, TreatmentId = 24, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 25, TreatmentId = 25, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 26, TreatmentId = 26, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 27, TreatmentId = 27, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 28, TreatmentId = 28, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 29, TreatmentId = 29, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 30, TreatmentId = 30, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 31, TreatmentId = 31, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 32, TreatmentId = 32, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 33, TreatmentId = 33, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 34, TreatmentId = 34, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 35, TreatmentId = 35, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 36, TreatmentId = 36, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 37, TreatmentId = 37, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 38, TreatmentId = 38, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 39, TreatmentId = 39, EquipmentName = "Tratamiento Vario", Cantidad = 1 },
                new TreatmentEquipment { Id = 40, TreatmentId = 40, EquipmentName = "Tratamiento Vario", Cantidad = 1 }
            );

            // Seed inicial de Movimientos
            modelBuilder.Entity<Movimiento>().HasData(
                new Movimiento { Id = 1, ColmenaId = 1, ApiarioOrigenId = 1, ApiarioDestinoId = 2, Razon = "Polinización Alfalfa", FechaSalida = DateTime.Now.AddDays(-5), FechaRegreso = DateTime.Now.AddDays(15), Estado = "Vigente" },
                new Movimiento { Id = 2, ColmenaId = 2, ApiarioOrigenId = 1, ApiarioDestinoId = 3, Razon = "Refugio Invernal", FechaSalida = DateTime.Now.AddDays(-20), FechaRegreso = DateTime.Now.AddDays(2), Estado = "Vigente" },
                new Movimiento { Id = 3, ColmenaId = 3, ApiarioOrigenId = 1, ApiarioDestinoId = 2, Razon = "Prueba de campo", FechaSalida = DateTime.Now.AddDays(-10), FechaRegreso = DateTime.Now.AddDays(-2), Estado = "Vigente" },
                new Movimiento { Id = 4, ColmenaId = 5, ApiarioOrigenId = 2, ApiarioDestinoId = 1, Razon = "Floración temprana", FechaSalida = DateTime.Now.AddDays(-40), FechaRegreso = DateTime.Now.AddDays(-10), Estado = "Completado" },
                new Movimiento { Id = 5, ColmenaId = 6, ApiarioOrigenId = 2, ApiarioDestinoId = 3, Razon = "Error de registro", FechaSalida = DateTime.Now.AddDays(-5), FechaRegreso = DateTime.Now.AddDays(5), Estado = "Cancelado" },
                new Movimiento { Id = 6, ColmenaId = 29, ApiarioOrigenId = 2, ApiarioDestinoId = 5, Razon = "Refugio Invernal", FechaSalida = DateTime.Now.AddDays(-28), FechaRegreso = DateTime.Now.AddDays(-(30)), Estado = "Vigente" },
                new Movimiento { Id = 7, ColmenaId = 29, ApiarioOrigenId = 4, ApiarioDestinoId = 2, Razon = "Floración Pradera", FechaSalida = DateTime.Now.AddDays(-56), FechaRegreso = DateTime.Now.AddDays(-(49)), Estado = "Vigente" },
                new Movimiento { Id = 8, ColmenaId = 10, ApiarioOrigenId = 5, ApiarioDestinoId = 3, Razon = "Venta/Préstamo", FechaSalida = DateTime.Now.AddDays(-36), FechaRegreso = DateTime.Now.AddDays(-(38)), Estado = "Completado" },
                new Movimiento { Id = 9, ColmenaId = 13, ApiarioOrigenId = 5, ApiarioDestinoId = 3, Razon = "Cuarentena", FechaSalida = DateTime.Now.AddDays(-17), FechaRegreso = DateTime.Now.AddDays(-(18)), Estado = "Cancelado" },
                new Movimiento { Id = 10, ColmenaId = 19, ApiarioOrigenId = 1, ApiarioDestinoId = 2, Razon = "Venta/Préstamo", FechaSalida = DateTime.Now.AddDays(-69), FechaRegreso = DateTime.Now.AddDays(-(52)), Estado = "Cancelado" },
                new Movimiento { Id = 11, ColmenaId = 1, ApiarioOrigenId = 1, ApiarioDestinoId = 2, Razon = "Cuarentena", FechaSalida = DateTime.Now.AddDays(-71), FechaRegreso = DateTime.Now.AddDays(-(60)), Estado = "Completado" },
                new Movimiento { Id = 12, ColmenaId = 34, ApiarioOrigenId = 1, ApiarioDestinoId = 2, Razon = "Refugio Invernal", FechaSalida = DateTime.Now.AddDays(-62), FechaRegreso = DateTime.Now.AddDays(-(55)), Estado = "Completado" },
                new Movimiento { Id = 13, ColmenaId = 2, ApiarioOrigenId = 1, ApiarioDestinoId = 4, Razon = "Refugio Invernal", FechaSalida = DateTime.Now.AddDays(-36), FechaRegreso = DateTime.Now.AddDays(-(40)), Estado = "Completado" },
                new Movimiento { Id = 14, ColmenaId = 27, ApiarioOrigenId = 3, ApiarioDestinoId = 1, Razon = "Venta/Préstamo", FechaSalida = DateTime.Now.AddDays(-53), FechaRegreso = DateTime.Now.AddDays(-(63)), Estado = "Completado" },
                new Movimiento { Id = 15, ColmenaId = 24, ApiarioOrigenId = 4, ApiarioDestinoId = 3, Razon = "Cuarentena", FechaSalida = DateTime.Now.AddDays(-82), FechaRegreso = DateTime.Now.AddDays(-(102)), Estado = "Cancelado" },
                new Movimiento { Id = 16, ColmenaId = 9, ApiarioOrigenId = 2, ApiarioDestinoId = 1, Razon = "Venta/Préstamo", FechaSalida = DateTime.Now.AddDays(-86), FechaRegreso = DateTime.Now.AddDays(-(78)), Estado = "Completado" },
                new Movimiento { Id = 17, ColmenaId = 27, ApiarioOrigenId = 2, ApiarioDestinoId = 4, Razon = "Floración Eucalyptus", FechaSalida = DateTime.Now.AddDays(-29), FechaRegreso = DateTime.Now.AddDays(-(10)), Estado = "Vigente" },
                new Movimiento { Id = 18, ColmenaId = 32, ApiarioOrigenId = 1, ApiarioDestinoId = 5, Razon = "Cuarentena", FechaSalida = DateTime.Now.AddDays(-32), FechaRegreso = DateTime.Now.AddDays(-(52)), Estado = "Completado" },
                new Movimiento { Id = 19, ColmenaId = 20, ApiarioOrigenId = 2, ApiarioDestinoId = 4, Razon = "Floración Pradera", FechaSalida = DateTime.Now.AddDays(-38), FechaRegreso = DateTime.Now.AddDays(-(57)), Estado = "Vigente" },
                new Movimiento { Id = 20, ColmenaId = 3, ApiarioOrigenId = 1, ApiarioDestinoId = 5, Razon = "Cuarentena", FechaSalida = DateTime.Now.AddDays(-33), FechaRegreso = DateTime.Now.AddDays(-(46)), Estado = "Cancelado" },
                new Movimiento { Id = 21, ColmenaId = 9, ApiarioOrigenId = 4, ApiarioDestinoId = 1, Razon = "Cuarentena", FechaSalida = DateTime.Now.AddDays(-46), FechaRegreso = DateTime.Now.AddDays(-(47)), Estado = "Cancelado" },
                new Movimiento { Id = 22, ColmenaId = 8, ApiarioOrigenId = 2, ApiarioDestinoId = 1, Razon = "Refugio Invernal", FechaSalida = DateTime.Now.AddDays(-29), FechaRegreso = DateTime.Now.AddDays(-(20)), Estado = "Vigente" },
                new Movimiento { Id = 23, ColmenaId = 1, ApiarioOrigenId = 1, ApiarioDestinoId = 3, Razon = "Venta/Préstamo", FechaSalida = DateTime.Now.AddDays(-60), FechaRegreso = DateTime.Now.AddDays(-(47)), Estado = "Completado" },
                new Movimiento { Id = 24, ColmenaId = 14, ApiarioOrigenId = 2, ApiarioDestinoId = 5, Razon = "Cuarentena", FechaSalida = DateTime.Now.AddDays(-89), FechaRegreso = DateTime.Now.AddDays(-(86)), Estado = "Completado" },
                new Movimiento { Id = 25, ColmenaId = 26, ApiarioOrigenId = 1, ApiarioDestinoId = 5, Razon = "Floración Eucalyptus", FechaSalida = DateTime.Now.AddDays(-17), FechaRegreso = DateTime.Now.AddDays(-(32)), Estado = "Completado" },
                new Movimiento { Id = 26, ColmenaId = 40, ApiarioOrigenId = 2, ApiarioDestinoId = 5, Razon = "Refugio Invernal", FechaSalida = DateTime.Now.AddDays(-64), FechaRegreso = DateTime.Now.AddDays(-(78)), Estado = "Completado" },
                new Movimiento { Id = 27, ColmenaId = 30, ApiarioOrigenId = 2, ApiarioDestinoId = 3, Razon = "Floración Eucalyptus", FechaSalida = DateTime.Now.AddDays(-36), FechaRegreso = DateTime.Now.AddDays(-(39)), Estado = "Completado" },
                new Movimiento { Id = 28, ColmenaId = 4, ApiarioOrigenId = 2, ApiarioDestinoId = 4, Razon = "Cuarentena", FechaSalida = DateTime.Now.AddDays(-96), FechaRegreso = DateTime.Now.AddDays(-(100)), Estado = "Completado" },
                new Movimiento { Id = 29, ColmenaId = 11, ApiarioOrigenId = 5, ApiarioDestinoId = 1, Razon = "Venta/Préstamo", FechaSalida = DateTime.Now.AddDays(-92), FechaRegreso = DateTime.Now.AddDays(-(109)), Estado = "Cancelado" },
                new Movimiento { Id = 30, ColmenaId = 6, ApiarioOrigenId = 2, ApiarioDestinoId = 4, Razon = "Refugio Invernal", FechaSalida = DateTime.Now.AddDays(-95), FechaRegreso = DateTime.Now.AddDays(-(108)), Estado = "Cancelado" }
            );

            // Seed inicial de Analisis Financieros
            modelBuilder.Entity<Analisis>().HasData(
                new Analisis { Id = 1, FechaInicio = new DateTime(2025, 8, 1), FechaFin = new DateTime(2025, 11, 30) },
                new Analisis { Id = 2, FechaInicio = new DateTime(2026, 5, 29), FechaFin = null },
                new Analisis { Id = 3, FechaInicio = new DateTime(2024, 8, 1), FechaFin = new DateTime(2025, 3, 30) },
                new Analisis { Id = 4, FechaInicio = new DateTime(2023, 9, 1), FechaFin = new DateTime(2024, 4, 15) }
            );

            // Seed inicial de Inversiones
            modelBuilder.Entity<Inversion>().HasData(
                new Inversion { Id = 1, AnalisisId = 1, Titulo = "Combustible por viaje", Nota = "Logística", Precio = 2400.0 },
                new Inversion { Id = 2, AnalisisId = 1, Titulo = "Equipamiento nuevo", Nota = "Ahumadores, trajes", Precio = 3150.0 },
                new Inversion { Id = 3, AnalisisId = 1, Titulo = "Tratamientos Varroa", Nota = "Suministros Médicos", Precio = 4200.0 },
                new Inversion { Id = 4, AnalisisId = 1, Titulo = "Mantenimiento de Cajas", Nota = "Materiales", Precio = 4500.0 },
                new Inversion { Id = 5, AnalisisId = 2, Titulo = "Compra de cera estampada", Nota = "Insumo inicial", Precio = 1200.0 },
                new Inversion { Id = 6, AnalisisId = 4, Titulo = "Herramientas", Nota = "Gasto operativo", Precio = 4761.64 },
                new Inversion { Id = 7, AnalisisId = 1, Titulo = "Alimentación", Nota = "Gasto operativo", Precio = 5940.06 },
                new Inversion { Id = 8, AnalisisId = 3, Titulo = "Suministros", Nota = "Gasto operativo", Precio = 5137.00 },
                new Inversion { Id = 9, AnalisisId = 1, Titulo = "Compra Reinas", Nota = "Gasto operativo", Precio = 6838.92 },
                new Inversion { Id = 10, AnalisisId = 1, Titulo = "Herramientas", Nota = "Gasto operativo", Precio = 7185.34 },
                new Inversion { Id = 11, AnalisisId = 2, Titulo = "Combustible", Nota = "Gasto operativo", Precio = 6073.76 },
                new Inversion { Id = 12, AnalisisId = 3, Titulo = "Combustible", Nota = "Gasto operativo", Precio = 1367.80 },
                new Inversion { Id = 13, AnalisisId = 3, Titulo = "Herramientas", Nota = "Gasto operativo", Precio = 7023.77 },
                new Inversion { Id = 14, AnalisisId = 1, Titulo = "Suministros", Nota = "Gasto operativo", Precio = 8054.27 },
                new Inversion { Id = 15, AnalisisId = 1, Titulo = "Herramientas", Nota = "Gasto operativo", Precio = 690.90 },
                new Inversion { Id = 16, AnalisisId = 1, Titulo = "Suministros", Nota = "Gasto operativo", Precio = 3315.32 },
                new Inversion { Id = 17, AnalisisId = 1, Titulo = "Compra Reinas", Nota = "Gasto operativo", Precio = 543.96 },
                new Inversion { Id = 18, AnalisisId = 1, Titulo = "Herramientas", Nota = "Gasto operativo", Precio = 6132.86 },
                new Inversion { Id = 19, AnalisisId = 3, Titulo = "Compra Reinas", Nota = "Gasto operativo", Precio = 4244.98 },
                new Inversion { Id = 20, AnalisisId = 2, Titulo = "Reparación Camioneta", Nota = "Gasto operativo", Precio = 4966.31 },
                new Inversion { Id = 21, AnalisisId = 4, Titulo = "Compra Reinas", Nota = "Gasto operativo", Precio = 6615.53 },
                new Inversion { Id = 22, AnalisisId = 3, Titulo = "Suministros", Nota = "Gasto operativo", Precio = 9982.61 },
                new Inversion { Id = 23, AnalisisId = 3, Titulo = "Reparación Camioneta", Nota = "Gasto operativo", Precio = 1594.28 },
                new Inversion { Id = 24, AnalisisId = 1, Titulo = "Herramientas", Nota = "Gasto operativo", Precio = 9027.15 },
                new Inversion { Id = 25, AnalisisId = 3, Titulo = "Suministros", Nota = "Gasto operativo", Precio = 3028.01 },
                new Inversion { Id = 26, AnalisisId = 4, Titulo = "Suministros", Nota = "Gasto operativo", Precio = 8671.21 },
                new Inversion { Id = 27, AnalisisId = 4, Titulo = "Combustible", Nota = "Gasto operativo", Precio = 1428.08 },
                new Inversion { Id = 28, AnalisisId = 2, Titulo = "Suministros", Nota = "Gasto operativo", Precio = 6436.72 },
                new Inversion { Id = 29, AnalisisId = 2, Titulo = "Alimentación", Nota = "Gasto operativo", Precio = 1610.32 },
                new Inversion { Id = 30, AnalisisId = 1, Titulo = "Combustible", Nota = "Gasto operativo", Precio = 3310.83 },
                new Inversion { Id = 31, AnalisisId = 3, Titulo = "Combustible", Nota = "Gasto operativo", Precio = 2293.58 },
                new Inversion { Id = 32, AnalisisId = 2, Titulo = "Herramientas", Nota = "Gasto operativo", Precio = 9485.06 },
                new Inversion { Id = 33, AnalisisId = 4, Titulo = "Alimentación", Nota = "Gasto operativo", Precio = 4430.51 },
                new Inversion { Id = 34, AnalisisId = 3, Titulo = "Suministros", Nota = "Gasto operativo", Precio = 6422.26 },
                new Inversion { Id = 35, AnalisisId = 1, Titulo = "Combustible", Nota = "Gasto operativo", Precio = 7585.95 },
                new Inversion { Id = 36, AnalisisId = 1, Titulo = "Suministros", Nota = "Gasto operativo", Precio = 8684.95 },
                new Inversion { Id = 37, AnalisisId = 4, Titulo = "Suministros", Nota = "Gasto operativo", Precio = 1464.48 },
                new Inversion { Id = 38, AnalisisId = 1, Titulo = "Compra Reinas", Nota = "Gasto operativo", Precio = 7458.54 },
                new Inversion { Id = 39, AnalisisId = 2, Titulo = "Compra Reinas", Nota = "Gasto operativo", Precio = 1525.53 },
                new Inversion { Id = 40, AnalisisId = 2, Titulo = "Suministros", Nota = "Gasto operativo", Precio = 6379.57 }
            );

            // Seed inicial de Ganancias
            modelBuilder.Entity<Ganancia>().HasData(
                new Ganancia { Id = 1, AnalisisId = 1, Titulo = "Venta de Miel (850 kg)", Descripcion = "Precio: $35/kg", Monto = 29750.0 },
                new Ganancia { Id = 2, AnalisisId = 1, Titulo = "Venta de Núcleos (20 u.)", Descripcion = "Precio: $350/u.", Monto = 7000.0 },
                new Ganancia { Id = 3, AnalisisId = 1, Titulo = "Venta de Polen (50 kg)", Descripcion = "Precio: $35/kg", Monto = 1750.0 },
                new Ganancia { Id = 4, AnalisisId = 2, Titulo = "Venta anticipada de propóleo", Descripcion = "Reserva de lote", Monto = 3500.0 },
                new Ganancia { Id = 5, AnalisisId = 3, Titulo = "Polinización", Descripcion = "Ingreso operativo", Monto = 29717.78 },
                new Ganancia { Id = 6, AnalisisId = 2, Titulo = "Venta Tambor Miel", Descripcion = "Ingreso operativo", Monto = 18368.51 },
                new Ganancia { Id = 7, AnalisisId = 1, Titulo = "Venta Núcleos", Descripcion = "Ingreso operativo", Monto = 7355.38 },
                new Ganancia { Id = 8, AnalisisId = 4, Titulo = "Propóleo", Descripcion = "Ingreso operativo", Monto = 22978.86 },
                new Ganancia { Id = 9, AnalisisId = 4, Titulo = "Polinización", Descripcion = "Ingreso operativo", Monto = 5258.62 },
                new Ganancia { Id = 10, AnalisisId = 4, Titulo = "Polinización", Descripcion = "Ingreso operativo", Monto = 26007.05 },
                new Ganancia { Id = 11, AnalisisId = 3, Titulo = "Polinización", Descripcion = "Ingreso operativo", Monto = 4582.23 },
                new Ganancia { Id = 12, AnalisisId = 1, Titulo = "Venta Tambor Miel", Descripcion = "Ingreso operativo", Monto = 14708.47 },
                new Ganancia { Id = 13, AnalisisId = 3, Titulo = "Venta Tambor Miel", Descripcion = "Ingreso operativo", Monto = 16672.27 },
                new Ganancia { Id = 14, AnalisisId = 1, Titulo = "Propóleo", Descripcion = "Ingreso operativo", Monto = 5093.17 },
                new Ganancia { Id = 15, AnalisisId = 2, Titulo = "Propóleo", Descripcion = "Ingreso operativo", Monto = 29355.81 },
                new Ganancia { Id = 16, AnalisisId = 4, Titulo = "Polinización", Descripcion = "Ingreso operativo", Monto = 27453.57 },
                new Ganancia { Id = 17, AnalisisId = 1, Titulo = "Cera", Descripcion = "Ingreso operativo", Monto = 25781.18 },
                new Ganancia { Id = 18, AnalisisId = 3, Titulo = "Propóleo", Descripcion = "Ingreso operativo", Monto = 11894.81 },
                new Ganancia { Id = 19, AnalisisId = 2, Titulo = "Polinización", Descripcion = "Ingreso operativo", Monto = 9508.57 },
                new Ganancia { Id = 20, AnalisisId = 4, Titulo = "Venta Núcleos", Descripcion = "Ingreso operativo", Monto = 7210.97 },
                new Ganancia { Id = 21, AnalisisId = 1, Titulo = "Cera", Descripcion = "Ingreso operativo", Monto = 14765.19 },
                new Ganancia { Id = 22, AnalisisId = 3, Titulo = "Propóleo", Descripcion = "Ingreso operativo", Monto = 2098.10 },
                new Ganancia { Id = 23, AnalisisId = 3, Titulo = "Cera", Descripcion = "Ingreso operativo", Monto = 10154.12 },
                new Ganancia { Id = 24, AnalisisId = 2, Titulo = "Propóleo", Descripcion = "Ingreso operativo", Monto = 12874.49 },
                new Ganancia { Id = 25, AnalisisId = 1, Titulo = "Venta Núcleos", Descripcion = "Ingreso operativo", Monto = 14028.94 },
                new Ganancia { Id = 26, AnalisisId = 4, Titulo = "Cera", Descripcion = "Ingreso operativo", Monto = 22364.18 },
                new Ganancia { Id = 27, AnalisisId = 2, Titulo = "Propóleo", Descripcion = "Ingreso operativo", Monto = 5601.88 },
                new Ganancia { Id = 28, AnalisisId = 1, Titulo = "Venta Tambor Miel", Descripcion = "Ingreso operativo", Monto = 4108.57 },
                new Ganancia { Id = 29, AnalisisId = 3, Titulo = "Venta Núcleos", Descripcion = "Ingreso operativo", Monto = 10000.81 },
                new Ganancia { Id = 30, AnalisisId = 2, Titulo = "Propóleo", Descripcion = "Ingreso operativo", Monto = 17510.40 }
            );

            // Seed inicial de Declaraciones Juradas
            modelBuilder.Entity<DeclaracionJurada>().HasData(
                new DeclaracionJurada { Id = 1, FechaEntrega = new DateTime(2025, 7, 21) }
            );
        }
    }
}
