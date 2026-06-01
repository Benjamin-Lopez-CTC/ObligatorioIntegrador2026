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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed inicial de usuarios
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "b.lopez", Password = "Admin123!", FullName = "Benjamin Lopez", Role = "Admin" },
                new User { Id = 2, Username = "f.alvarez", Password = "Admin123!", FullName = "Felipe Alvarez", Role = "Admin" },
                new User { Id = 3, Username = "m.vergues", Password = "Matias123!", FullName = "Matías Vergues", Role = "Beekeeper" }
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
                new Equipment { Id = 8, Name = "Cepillo 11", Type = "Tipo 6", Stock = 70, Category = "Herramienta", LowThreshold = 19, MediumThreshold = 48, DisplayOrder = 8, UnitPrice = 64.00, Currency = "UYU" },
                new Equipment { Id = 9, Name = "Techo 49", Type = "Tipo 7", Stock = 204, Category = "Material", LowThreshold = 7, MediumThreshold = 80, DisplayOrder = 9, UnitPrice = 367.20, Currency = "USD" },
                new Equipment { Id = 10, Name = "Suplemento Vitamínico 8", Type = "Tipo 2", Stock = 268, Category = "Medicamento", LowThreshold = 8, MediumThreshold = 105, DisplayOrder = 10, UnitPrice = 479.75, Currency = "USD" },
                new Equipment { Id = 11, Name = "Ácido Oxálico 73", Type = "Tipo 1", Stock = 386, Category = "Medicamento", LowThreshold = 33, MediumThreshold = 98, DisplayOrder = 11, UnitPrice = 318.46, Currency = "USD" },
                new Equipment { Id = 12, Name = "Timol 28", Type = "Tipo 7", Stock = 21, Category = "Medicamento", LowThreshold = 43, MediumThreshold = 127, DisplayOrder = 12, UnitPrice = 189.63, Currency = "UYU" },
                new Equipment { Id = 13, Name = "Alimentador 44", Type = "Tipo 4", Stock = 105, Category = "Material", LowThreshold = 19, MediumThreshold = 47, DisplayOrder = 13, UnitPrice = 152.93, Currency = "USD" },
                new Equipment { Id = 14, Name = "Pinza 32", Type = "Tipo 2", Stock = 492, Category = "Herramienta", LowThreshold = 34, MediumThreshold = 68, DisplayOrder = 14, UnitPrice = 421.96, Currency = "UYU" },
                new Equipment { Id = 15, Name = "Cepillo 16", Type = "Tipo 9", Stock = 388, Category = "Herramienta", LowThreshold = 48, MediumThreshold = 143, DisplayOrder = 15, UnitPrice = 266.00, Currency = "UYU" },
                new Equipment { Id = 16, Name = "Fluvalinato 100", Type = "Tipo 1", Stock = 304, Category = "Medicamento", LowThreshold = 39, MediumThreshold = 108, DisplayOrder = 16, UnitPrice = 446.40, Currency = "USD" },
                new Equipment { Id = 17, Name = "Techo 16", Type = "Tipo 2", Stock = 187, Category = "Material", LowThreshold = 10, MediumThreshold = 90, DisplayOrder = 17, UnitPrice = 37.16, Currency = "UYU" },
                new Equipment { Id = 18, Name = "Alimentador 21", Type = "Tipo 3", Stock = 90, Category = "Material", LowThreshold = 38, MediumThreshold = 116, DisplayOrder = 18, UnitPrice = 90.70, Currency = "UYU" },
                new Equipment { Id = 19, Name = "Ahumador 17", Type = "Tipo 9", Stock = 113, Category = "Herramienta", LowThreshold = 14, MediumThreshold = 107, DisplayOrder = 19, UnitPrice = 237.65, Currency = "USD" },
                new Equipment { Id = 20, Name = "Ácido Oxálico 6", Type = "Tipo 10", Stock = 418, Category = "Medicamento", LowThreshold = 9, MediumThreshold = 29, DisplayOrder = 20, UnitPrice = 220.20, Currency = "UYU" },
                new Equipment { Id = 21, Name = "Ácido Oxálico 72", Type = "Tipo 1", Stock = 16, Category = "Medicamento", LowThreshold = 8, MediumThreshold = 77, DisplayOrder = 21, UnitPrice = 239.81, Currency = "UYU" },
                new Equipment { Id = 22, Name = "Suplemento Vitamínico 80", Type = "Tipo 4", Stock = 211, Category = "Medicamento", LowThreshold = 36, MediumThreshold = 127, DisplayOrder = 22, UnitPrice = 318.59, Currency = "USD" },
                new Equipment { Id = 23, Name = "Extractor 81", Type = "Tipo 1", Stock = 257, Category = "Herramienta", LowThreshold = 48, MediumThreshold = 70, DisplayOrder = 23, UnitPrice = 128.92, Currency = "USD" },
                new Equipment { Id = 24, Name = "Timol 13", Type = "Tipo 5", Stock = 260, Category = "Medicamento", LowThreshold = 33, MediumThreshold = 113, DisplayOrder = 24, UnitPrice = 448.77, Currency = "USD" },
                new Equipment { Id = 25, Name = "Timol 87", Type = "Tipo 3", Stock = 404, Category = "Medicamento", LowThreshold = 27, MediumThreshold = 49, DisplayOrder = 25, UnitPrice = 489.32, Currency = "USD" }
            );

            // Relaciones de Movimiento para evitar ciclos de cascada
            modelBuilder.Entity<Movimiento>().HasOne(m => m.ApiarioOrigen).WithMany().HasForeignKey(m => m.ApiarioOrigenId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Movimiento>().HasOne(m => m.ApiarioDestino).WithMany().HasForeignKey(m => m.ApiarioDestinoId).OnDelete(DeleteBehavior.Restrict);

            // Relaciones de Inversion y Ganancia con Analisis
            modelBuilder.Entity<Inversion>().HasOne(i => i.Analisis).WithMany(a => a.Inversiones).HasForeignKey(i => i.AnalisisId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Ganancia>().HasOne(g => g.Analisis).WithMany(a => a.Ganancias).HasForeignKey(g => g.AnalisisId).OnDelete(DeleteBehavior.Cascade);

            // Seed inicial de Apiarios
            modelBuilder.Entity<Apiario>().HasData(
                new Apiario { Id = 1, Nombre = "Talar", StringIdentificador = "001", Departamento = "Flores", SeccionPolicial = "6", Paraje = "Talar", UbicacionTexto = "Flores (Talar)", UbicacionCoordenadas = "34°05'12.1\"S 70°45'22.4\"W", FechaCreacion = new DateTime(2023, 10, 12), UltimaInspeccion = new DateTime(2026, 05, 12), Responsable = "Ing. Martín Valenzuela", NotasEstado = "Zona con alta floración esperada para el próximo mes. Preparar alzas melarias adicionales. Monitoreo preventivo de Varroa recomendado.", HumedadInterna = 58 },
                new Apiario { Id = 2, Nombre = "Cerro", StringIdentificador = "002", Departamento = "Flores", SeccionPolicial = "6", Paraje = "Cerro", UbicacionTexto = "Flores (Cerro)", UbicacionCoordenadas = "34°08'10.2\"S 70°42'15.4\"W", FechaCreacion = new DateTime(2024, 02, 05), UltimaInspeccion = new DateTime(2026, 05, 05), Responsable = "Ing. Martín Valenzuela", NotasEstado = "Revisar posibles daños por viento en el sector este.", HumedadInterna = 45 },
                new Apiario { Id = 3, Nombre = "Cantera", StringIdentificador = "003", Departamento = "San José", SeccionPolicial = "3", Paraje = "Cantera", UbicacionTexto = "San José (Cantera)", UbicacionCoordenadas = "34°10'05.1\"S 70°40'00.0\"W", FechaCreacion = new DateTime(2025, 01, 15), UltimaInspeccion = new DateTime(2026, 05, 15), Responsable = "Benjamin Lopez", NotasEstado = "Producción crítica, requiere revisión urgente de reinas.", HumedadInterna = 62 },
                new Apiario { Id = 4, Nombre = "Alfalfa", StringIdentificador = "004", Departamento = "San José", SeccionPolicial = "3", Paraje = "Alfalfa", UbicacionTexto = "San José (Alfalfa)", UbicacionCoordenadas = "34°12'00.0\"S 70°38'00.0\"W", FechaCreacion = new DateTime(2025, 02, 10), UltimaInspeccion = new DateTime(2026, 05, 10), Responsable = "Benjamin Lopez", NotasEstado = "Desarrollo normal.", HumedadInterna = 55 },
                new Apiario { Id = 5, Nombre = "Caraguatá", StringIdentificador = "005", Departamento = "San José", SeccionPolicial = "4", Paraje = "Caraguatá", UbicacionTexto = "San José (Caraguatá)", UbicacionCoordenadas = "34°15'00.0\"S 70°35'00.0\"W", FechaCreacion = new DateTime(2025, 03, 01), UltimaInspeccion = new DateTime(2026, 05, 01), Responsable = "Ing. Martín Valenzuela", NotasEstado = "Ubicación ideal para invierno.", HumedadInterna = 50}
            );

            // Seed inicial de Colmenas
            modelBuilder.Entity<Colmena>().HasData(
                new Colmena { Id = 1, Identificador = "#HIVE-0042", CodigoEscaneo = "100001", ApiarioId = 1, Estado = "Óptimo", PesoKg = 45.2, TemperaturaInterna = 34.5, HumedadInterna = 55.0, ProduccionMielKg = 40.0, EsPiloto = true, EsNucleo = false, CantidadAbejas = 45000, UbicacionIntraApiario = "Fila 1, Pos 1", ComportamientoAbejas = "Dócil", EstadoReina = "Presente" },
                new Colmena { Id = 2, Identificador = "#HIVE-0089", CodigoEscaneo = "100002", ApiarioId = 1, Estado = "Alerta", PesoKg = 42.8, TemperaturaInterna = 32.0, HumedadInterna = 0, ProduccionMielKg = 35.0, EsPiloto = false, EsNucleo = true, CantidadAbejas = 38000, UbicacionIntraApiario = "Fila 1, Pos 2", ComportamientoAbejas = "Defensivo", EstadoReina = "Presente" },
                new Colmena { Id = 3, Identificador = "#HIVE-0112", CodigoEscaneo = "100003", ApiarioId = 2, Estado = "Crítico", PesoKg = 31.0, TemperaturaInterna = 36.5, HumedadInterna = 60.0, ProduccionMielKg = 20.0, EsPiloto = true, EsNucleo = false, CantidadAbejas = 15000, UbicacionIntraApiario = "Fila 2, Pos 1", ComportamientoAbejas = "Agresivo", EstadoReina = "Ausente" },
                new Colmena { Id = 4, Identificador = "#HIVE-0045", CodigoEscaneo = "100004", ApiarioId = 2, Estado = "Óptimo", PesoKg = 48.1, TemperaturaInterna = 34.2, HumedadInterna = 58.0, ProduccionMielKg = 45.0, EsPiloto = true, EsNucleo = true, CantidadAbejas = 50000, UbicacionIntraApiario = "Fila 2, Pos 2", ComportamientoAbejas = "Dócil", EstadoReina = "Presente" },
                new Colmena { Id = 5, Identificador = "#HIVE-0001", CodigoEscaneo = "100005", ApiarioId = 3, Estado = "Óptimo", PesoKg = 40.0, TemperaturaInterna = 35.1, HumedadInterna = 52.0, ProduccionMielKg = 30.0, EsPiloto = true, EsNucleo = false, CantidadAbejas = 42000, UbicacionIntraApiario = "Fila 1, Pos 1", ComportamientoAbejas = "Dócil", EstadoReina = "Presente" },
                new Colmena { Id = 6, Identificador = "#HIVE-0002", CodigoEscaneo = "100006", ApiarioId = 4, Estado = "Óptimo", PesoKg = 39.5, TemperaturaInterna = 34.8, HumedadInterna = 0, ProduccionMielKg = 30.0, EsPiloto = false, EsNucleo = false, CantidadAbejas = 41000, UbicacionIntraApiario = "Fila 1, Pos 2", ComportamientoAbejas = "Dócil", EstadoReina = "Presente" },
                new Colmena { Id = 7, Identificador = "#HIVE-0003", CodigoEscaneo = "100007", ApiarioId = 5, Estado = "Crítico", PesoKg = 25.0, TemperaturaInterna = 30.0, HumedadInterna = 82.0, ProduccionMielKg = 10.0, EsPiloto = true, EsNucleo = true, CantidadAbejas = 12000, UbicacionIntraApiario = "Única", ComportamientoAbejas = "Agresivo", EstadoReina = "Presente" },
                new Colmena { Id = 8, Identificador = "#HIVE-0008", CodigoEscaneo = "100008", ApiarioId = 4, Estado = "Óptimo", PesoKg = 22.5, TemperaturaInterna = 31.9, HumedadInterna = 57.2, ProduccionMielKg = 11.6, EsPiloto = false, EsNucleo = true, CantidadAbejas = 56151, UbicacionIntraApiario = "Fila 1, Pos 1", ComportamientoAbejas = "Defensivo", EstadoReina = "Cambiando" },
                new Colmena { Id = 9, Identificador = "#HIVE-0009", CodigoEscaneo = "100009", ApiarioId = 1, Estado = "Óptimo", PesoKg = 29.3, TemperaturaInterna = 37.7, HumedadInterna = 51.9, ProduccionMielKg = 28.2, EsPiloto = true, EsNucleo = false, CantidadAbejas = 45396, UbicacionIntraApiario = "Fila 4, Pos 10", ComportamientoAbejas = "Agresivo", EstadoReina = "Cambiando" },
                new Colmena { Id = 10, Identificador = "#HIVE-0010", CodigoEscaneo = "100010", ApiarioId = 2, Estado = "Óptimo", PesoKg = 40.5, TemperaturaInterna = 32.3, HumedadInterna = 63.2, ProduccionMielKg = 23.4, EsPiloto = false, EsNucleo = true, CantidadAbejas = 45956, UbicacionIntraApiario = "Fila 3, Pos 6", ComportamientoAbejas = "Defensivo", EstadoReina = "Cambiando" },
                new Colmena { Id = 11, Identificador = "#HIVE-0011", CodigoEscaneo = "100011", ApiarioId = 4, Estado = "Alerta", PesoKg = 45.2, TemperaturaInterna = 32.5, HumedadInterna = 41.6, ProduccionMielKg = 30.6, EsPiloto = false, EsNucleo = true, CantidadAbejas = 22306, UbicacionIntraApiario = "Fila 1, Pos 4", ComportamientoAbejas = "Defensivo", EstadoReina = "Ausente" },
                new Colmena { Id = 12, Identificador = "#HIVE-0012", CodigoEscaneo = "100012", ApiarioId = 4, Estado = "Alerta", PesoKg = 41.3, TemperaturaInterna = 31.8, HumedadInterna = 67.7, ProduccionMielKg = 22.4, EsPiloto = false, EsNucleo = false, CantidadAbejas = 49003, UbicacionIntraApiario = "Fila 4, Pos 2", ComportamientoAbejas = "Dócil", EstadoReina = "Ausente" },
                new Colmena { Id = 13, Identificador = "#HIVE-0013", CodigoEscaneo = "100013", ApiarioId = 2, Estado = "Óptimo", PesoKg = 42.8, TemperaturaInterna = 35.9, HumedadInterna = 69.0, ProduccionMielKg = 11.2, EsPiloto = false, EsNucleo = false, CantidadAbejas = 57785, UbicacionIntraApiario = "Fila 1, Pos 6", ComportamientoAbejas = "Defensivo", EstadoReina = "Cambiando" },
                new Colmena { Id = 14, Identificador = "#HIVE-0014", CodigoEscaneo = "100014", ApiarioId = 4, Estado = "Alerta", PesoKg = 52.0, TemperaturaInterna = 32.7, HumedadInterna = 69.5, ProduccionMielKg = 28.8, EsPiloto = true, EsNucleo = false, CantidadAbejas = 59527, UbicacionIntraApiario = "Fila 3, Pos 7", ComportamientoAbejas = "Agresivo", EstadoReina = "Cambiando" },
                new Colmena { Id = 15, Identificador = "#HIVE-0015", CodigoEscaneo = "100015", ApiarioId = 5, Estado = "Alerta", PesoKg = 48.7, TemperaturaInterna = 35.7, HumedadInterna = 61.1, ProduccionMielKg = 48.4, EsPiloto = false, EsNucleo = false, CantidadAbejas = 23446, UbicacionIntraApiario = "Fila 5, Pos 8", ComportamientoAbejas = "Dócil", EstadoReina = "Ausente" },
                new Colmena { Id = 16, Identificador = "#HIVE-0016", CodigoEscaneo = "100016", ApiarioId = 3, Estado = "Alerta", PesoKg = 42.2, TemperaturaInterna = 36.4, HumedadInterna = 43.9, ProduccionMielKg = 17.5, EsPiloto = false, EsNucleo = false, CantidadAbejas = 46601, UbicacionIntraApiario = "Fila 2, Pos 8", ComportamientoAbejas = "Dócil", EstadoReina = "Ausente" },
                new Colmena { Id = 17, Identificador = "#HIVE-0017", CodigoEscaneo = "100017", ApiarioId = 2, Estado = "Óptimo", PesoKg = 46.5, TemperaturaInterna = 36.2, HumedadInterna = 68.6, ProduccionMielKg = 40.0, EsPiloto = false, EsNucleo = true, CantidadAbejas = 45291, UbicacionIntraApiario = "Fila 5, Pos 9", ComportamientoAbejas = "Defensivo", EstadoReina = "Cambiando" },
                new Colmena { Id = 18, Identificador = "#HIVE-0018", CodigoEscaneo = "100018", ApiarioId = 2, Estado = "Óptimo", PesoKg = 29.1, TemperaturaInterna = 37.0, HumedadInterna = 68.7, ProduccionMielKg = 42.3, EsPiloto = true, EsNucleo = false, CantidadAbejas = 46887, UbicacionIntraApiario = "Fila 2, Pos 8", ComportamientoAbejas = "Defensivo", EstadoReina = "Ausente" },
                new Colmena { Id = 19, Identificador = "#HIVE-0019", CodigoEscaneo = "100019", ApiarioId = 1, Estado = "Alerta", PesoKg = 53.0, TemperaturaInterna = 33.3, HumedadInterna = 60.0, ProduccionMielKg = 49.5, EsPiloto = false, EsNucleo = false, CantidadAbejas = 20653, UbicacionIntraApiario = "Fila 4, Pos 10", ComportamientoAbejas = "Dócil", EstadoReina = "Presente" },
                new Colmena { Id = 20, Identificador = "#HIVE-0020", CodigoEscaneo = "100020", ApiarioId = 2, Estado = "Alerta", PesoKg = 24.9, TemperaturaInterna = 36.3, HumedadInterna = 51.3, ProduccionMielKg = 40.5, EsPiloto = false, EsNucleo = false, CantidadAbejas = 59685, UbicacionIntraApiario = "Fila 2, Pos 3", ComportamientoAbejas = "Dócil", EstadoReina = "Presente" },
                new Colmena { Id = 21, Identificador = "#HIVE-0021", CodigoEscaneo = "100021", ApiarioId = 2, Estado = "Alerta", PesoKg = 43.4, TemperaturaInterna = 37.4, HumedadInterna = 49.7, ProduccionMielKg = 16.0, EsPiloto = false, EsNucleo = false, CantidadAbejas = 25031, UbicacionIntraApiario = "Fila 4, Pos 5", ComportamientoAbejas = "Dócil", EstadoReina = "Presente" },
                new Colmena { Id = 22, Identificador = "#HIVE-0022", CodigoEscaneo = "100022", ApiarioId = 4, Estado = "Alerta", PesoKg = 47.2, TemperaturaInterna = 35.1, HumedadInterna = 53.8, ProduccionMielKg = 37.4, EsPiloto = false, EsNucleo = false, CantidadAbejas = 42777, UbicacionIntraApiario = "Fila 4, Pos 3", ComportamientoAbejas = "Defensivo", EstadoReina = "Cambiando" },
                new Colmena { Id = 23, Identificador = "#HIVE-0023", CodigoEscaneo = "100023", ApiarioId = 3, Estado = "Alerta", PesoKg = 51.8, TemperaturaInterna = 33.1, HumedadInterna = 69.7, ProduccionMielKg = 18.5, EsPiloto = false, EsNucleo = true, CantidadAbejas = 56213, UbicacionIntraApiario = "Fila 5, Pos 10", ComportamientoAbejas = "Defensivo", EstadoReina = "Presente" },
                new Colmena { Id = 24, Identificador = "#HIVE-0024", CodigoEscaneo = "100024", ApiarioId = 2, Estado = "Alerta", PesoKg = 35.9, TemperaturaInterna = 36.5, HumedadInterna = 66.4, ProduccionMielKg = 35.5, EsPiloto = false, EsNucleo = false, CantidadAbejas = 35726, UbicacionIntraApiario = "Fila 3, Pos 3", ComportamientoAbejas = "Agresivo", EstadoReina = "Presente" },
                new Colmena { Id = 25, Identificador = "#HIVE-0025", CodigoEscaneo = "100025", ApiarioId = 3, Estado = "Crítico", PesoKg = 41.0, TemperaturaInterna = 32.3, HumedadInterna = 40.6, ProduccionMielKg = 24.4, EsPiloto = false, EsNucleo = false, CantidadAbejas = 25129, UbicacionIntraApiario = "Fila 3, Pos 6", ComportamientoAbejas = "Dócil", EstadoReina = "Ausente" },
                new Colmena { Id = 26, Identificador = "#HIVE-0026", CodigoEscaneo = "100026", ApiarioId = 4, Estado = "Crítico", PesoKg = 58.4, TemperaturaInterna = 37.2, HumedadInterna = 43.0, ProduccionMielKg = 28.1, EsPiloto = true, EsNucleo = false, CantidadAbejas = 29755, UbicacionIntraApiario = "Fila 5, Pos 4", ComportamientoAbejas = "Dócil", EstadoReina = "Presente" },
                new Colmena { Id = 27, Identificador = "#HIVE-0027", CodigoEscaneo = "100027", ApiarioId = 2, Estado = "Alerta", PesoKg = 34.4, TemperaturaInterna = 37.1, HumedadInterna = 62.2, ProduccionMielKg = 30.5, EsPiloto = true, EsNucleo = true, CantidadAbejas = 38437, UbicacionIntraApiario = "Fila 2, Pos 5", ComportamientoAbejas = "Agresivo", EstadoReina = "Ausente" },
                new Colmena { Id = 28, Identificador = "#HIVE-0028", CodigoEscaneo = "100028", ApiarioId = 4, Estado = "Crítico", PesoKg = 43.1, TemperaturaInterna = 31.3, HumedadInterna = 53.5, ProduccionMielKg = 18.6, EsPiloto = false, EsNucleo = false, CantidadAbejas = 20245, UbicacionIntraApiario = "Fila 4, Pos 7", ComportamientoAbejas = "Defensivo", EstadoReina = "Presente" },
                new Colmena { Id = 29, Identificador = "#HIVE-0029", CodigoEscaneo = "100029", ApiarioId = 4, Estado = "Crítico", PesoKg = 26.6, TemperaturaInterna = 33.4, HumedadInterna = 55.2, ProduccionMielKg = 37.5, EsPiloto = false, EsNucleo = false, CantidadAbejas = 28682, UbicacionIntraApiario = "Fila 1, Pos 5", ComportamientoAbejas = "Agresivo", EstadoReina = "Ausente" },
                new Colmena { Id = 30, Identificador = "#HIVE-0030", CodigoEscaneo = "100030", ApiarioId = 1, Estado = "Alerta", PesoKg = 31.5, TemperaturaInterna = 33.4, HumedadInterna = 44.5, ProduccionMielKg = 39.6, EsPiloto = false, EsNucleo = false, CantidadAbejas = 18054, UbicacionIntraApiario = "Fila 5, Pos 9", ComportamientoAbejas = "Agresivo", EstadoReina = "Ausente" },
                new Colmena { Id = 31, Identificador = "#HIVE-0031", CodigoEscaneo = "100031", ApiarioId = 1, Estado = "Alerta", PesoKg = 37.4, TemperaturaInterna = 31.9, HumedadInterna = 48.9, ProduccionMielKg = 14.7, EsPiloto = false, EsNucleo = false, CantidadAbejas = 39952, UbicacionIntraApiario = "Fila 1, Pos 7", ComportamientoAbejas = "Dócil", EstadoReina = "Presente" },
                new Colmena { Id = 32, Identificador = "#HIVE-0032", CodigoEscaneo = "100032", ApiarioId = 1, Estado = "Óptimo", PesoKg = 51.1, TemperaturaInterna = 35.1, HumedadInterna = 52.9, ProduccionMielKg = 32.7, EsPiloto = false, EsNucleo = false, CantidadAbejas = 26969, UbicacionIntraApiario = "Fila 5, Pos 10", ComportamientoAbejas = "Defensivo", EstadoReina = "Presente" },
                new Colmena { Id = 33, Identificador = "#HIVE-0033", CodigoEscaneo = "100033", ApiarioId = 5, Estado = "Óptimo", PesoKg = 42.6, TemperaturaInterna = 32.5, HumedadInterna = 49.6, ProduccionMielKg = 30.8, EsPiloto = false, EsNucleo = false, CantidadAbejas = 22338, UbicacionIntraApiario = "Fila 3, Pos 2", ComportamientoAbejas = "Agresivo", EstadoReina = "Ausente" },
                new Colmena { Id = 34, Identificador = "#HIVE-0034", CodigoEscaneo = "100034", ApiarioId = 3, Estado = "Óptimo", PesoKg = 57.0, TemperaturaInterna = 35.0, HumedadInterna = 47.5, ProduccionMielKg = 10.8, EsPiloto = false, EsNucleo = false, CantidadAbejas = 22424, UbicacionIntraApiario = "Fila 1, Pos 10", ComportamientoAbejas = "Agresivo", EstadoReina = "Cambiando" },
                new Colmena { Id = 35, Identificador = "#HIVE-0035", CodigoEscaneo = "100035", ApiarioId = 4, Estado = "Alerta", PesoKg = 43.7, TemperaturaInterna = 37.5, HumedadInterna = 53.2, ProduccionMielKg = 29.7, EsPiloto = false, EsNucleo = true, CantidadAbejas = 51770, UbicacionIntraApiario = "Fila 2, Pos 1", ComportamientoAbejas = "Dócil", EstadoReina = "Cambiando" },
                new Colmena { Id = 36, Identificador = "#HIVE-0036", CodigoEscaneo = "100036", ApiarioId = 4, Estado = "Crítico", PesoKg = 51.6, TemperaturaInterna = 32.0, HumedadInterna = 64.3, ProduccionMielKg = 22.2, EsPiloto = false, EsNucleo = true, CantidadAbejas = 44633, UbicacionIntraApiario = "Fila 5, Pos 6", ComportamientoAbejas = "Dócil", EstadoReina = "Ausente" },
                new Colmena { Id = 37, Identificador = "#HIVE-0037", CodigoEscaneo = "100037", ApiarioId = 1, Estado = "Crítico", PesoKg = 55.1, TemperaturaInterna = 32.7, HumedadInterna = 52.8, ProduccionMielKg = 32.7, EsPiloto = true, EsNucleo = false, CantidadAbejas = 16284, UbicacionIntraApiario = "Fila 5, Pos 10", ComportamientoAbejas = "Defensivo", EstadoReina = "Cambiando" },
                new Colmena { Id = 38, Identificador = "#HIVE-0038", CodigoEscaneo = "100038", ApiarioId = 5, Estado = "Óptimo", PesoKg = 49.7, TemperaturaInterna = 33.7, HumedadInterna = 42.2, ProduccionMielKg = 43.3, EsPiloto = false, EsNucleo = false, CantidadAbejas = 29351, UbicacionIntraApiario = "Fila 4, Pos 7", ComportamientoAbejas = "Dócil", EstadoReina = "Ausente" },
                new Colmena { Id = 39, Identificador = "#HIVE-0039", CodigoEscaneo = "100039", ApiarioId = 5, Estado = "Alerta", PesoKg = 21.9, TemperaturaInterna = 31.5, HumedadInterna = 61.5, ProduccionMielKg = 13.2, EsPiloto = false, EsNucleo = false, CantidadAbejas = 46070, UbicacionIntraApiario = "Fila 1, Pos 4", ComportamientoAbejas = "Dócil", EstadoReina = "Presente" },
                new Colmena { Id = 40, Identificador = "#HIVE-0040", CodigoEscaneo = "100040", ApiarioId = 5, Estado = "Crítico", PesoKg = 37.8, TemperaturaInterna = 32.4, HumedadInterna = 60.6, ProduccionMielKg = 39.9, EsPiloto = true, EsNucleo = true, CantidadAbejas = 38562, UbicacionIntraApiario = "Fila 3, Pos 5", ComportamientoAbejas = "Dócil", EstadoReina = "Presente" },
                new Colmena { Id = 41, Identificador = "#HIVE-0041", CodigoEscaneo = "100041", ApiarioId = 4, Estado = "Óptimo", PesoKg = 49.5, TemperaturaInterna = 31.3, HumedadInterna = 57.8, ProduccionMielKg = 47.0, EsPiloto = true, EsNucleo = false, CantidadAbejas = 58811, UbicacionIntraApiario = "Fila 1, Pos 3", ComportamientoAbejas = "Defensivo", EstadoReina = "Presente" },
                new Colmena { Id = 42, Identificador = "#HIVE-0042", CodigoEscaneo = "100042", ApiarioId = 1, Estado = "Alerta", PesoKg = 55.0, TemperaturaInterna = 36.7, HumedadInterna = 46.8, ProduccionMielKg = 27.1, EsPiloto = false, EsNucleo = false, CantidadAbejas = 32130, UbicacionIntraApiario = "Fila 4, Pos 10", ComportamientoAbejas = "Agresivo", EstadoReina = "Cambiando" },
                new Colmena { Id = 43, Identificador = "#HIVE-0043", CodigoEscaneo = "100043", ApiarioId = 4, Estado = "Crítico", PesoKg = 43.0, TemperaturaInterna = 37.6, HumedadInterna = 57.9, ProduccionMielKg = 24.0, EsPiloto = false, EsNucleo = false, CantidadAbejas = 53960, UbicacionIntraApiario = "Fila 4, Pos 5", ComportamientoAbejas = "Dócil", EstadoReina = "Ausente" },
                new Colmena { Id = 44, Identificador = "#HIVE-0044", CodigoEscaneo = "100044", ApiarioId = 3, Estado = "Óptimo", PesoKg = 24.0, TemperaturaInterna = 34.2, HumedadInterna = 41.6, ProduccionMielKg = 15.0, EsPiloto = false, EsNucleo = false, CantidadAbejas = 39310, UbicacionIntraApiario = "Fila 4, Pos 7", ComportamientoAbejas = "Defensivo", EstadoReina = "Presente" },
                new Colmena { Id = 45, Identificador = "#HIVE-0045", CodigoEscaneo = "100045", ApiarioId = 2, Estado = "Alerta", PesoKg = 37.9, TemperaturaInterna = 30.0, HumedadInterna = 43.3, ProduccionMielKg = 25.9, EsPiloto = true, EsNucleo = true, CantidadAbejas = 26526, UbicacionIntraApiario = "Fila 5, Pos 8", ComportamientoAbejas = "Agresivo", EstadoReina = "Ausente" },
                new Colmena { Id = 46, Identificador = "#HIVE-0046", CodigoEscaneo = "100046", ApiarioId = 5, Estado = "Óptimo", PesoKg = 57.4, TemperaturaInterna = 37.0, HumedadInterna = 60.6, ProduccionMielKg = 42.9, EsPiloto = false, EsNucleo = false, CantidadAbejas = 29703, UbicacionIntraApiario = "Fila 4, Pos 8", ComportamientoAbejas = "Agresivo", EstadoReina = "Cambiando" },
                new Colmena { Id = 47, Identificador = "#HIVE-0047", CodigoEscaneo = "100047", ApiarioId = 5, Estado = "Alerta", PesoKg = 22.7, TemperaturaInterna = 33.4, HumedadInterna = 63.5, ProduccionMielKg = 32.4, EsPiloto = true, EsNucleo = true, CantidadAbejas = 38598, UbicacionIntraApiario = "Fila 1, Pos 6", ComportamientoAbejas = "Dócil", EstadoReina = "Cambiando" },
                new Colmena { Id = 48, Identificador = "#HIVE-0048", CodigoEscaneo = "100048", ApiarioId = 5, Estado = "Alerta", PesoKg = 48.4, TemperaturaInterna = 30.7, HumedadInterna = 57.0, ProduccionMielKg = 24.4, EsPiloto = false, EsNucleo = true, CantidadAbejas = 10655, UbicacionIntraApiario = "Fila 2, Pos 9", ComportamientoAbejas = "Defensivo", EstadoReina = "Ausente" },
                new Colmena { Id = 49, Identificador = "#HIVE-0049", CodigoEscaneo = "100049", ApiarioId = 4, Estado = "Crítico", PesoKg = 56.5, TemperaturaInterna = 34.4, HumedadInterna = 46.3, ProduccionMielKg = 10.7, EsPiloto = false, EsNucleo = false, CantidadAbejas = 45048, UbicacionIntraApiario = "Fila 2, Pos 6", ComportamientoAbejas = "Agresivo", EstadoReina = "Presente" },
                new Colmena { Id = 50, Identificador = "#HIVE-0050", CodigoEscaneo = "100050", ApiarioId = 1, Estado = "Óptimo", PesoKg = 25.5, TemperaturaInterna = 30.9, HumedadInterna = 60.6, ProduccionMielKg = 11.9, EsPiloto = false, EsNucleo = false, CantidadAbejas = 51624, UbicacionIntraApiario = "Fila 1, Pos 4", ComportamientoAbejas = "Agresivo", EstadoReina = "Cambiando" }
            );

            // Seed inicial de Notas Tecnicas
            modelBuilder.Entity<NotaTecnica>().HasData(
                new NotaTecnica { Id = 1, ColmenaId = 1, Detalles = "Revisión general, todo normal.", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-10) },
                new NotaTecnica { Id = 2, ColmenaId = 2, Detalles = "Abejas defensivas, observar.", EstadoReina = "Presente", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-15) },
                new NotaTecnica { Id = 3, ColmenaId = 3, Detalles = "Reina no avistada. Posible enjambrazón.", EstadoReina = "Ausente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-2) },
                new NotaTecnica { Id = 4, ColmenaId = 4, Detalles = "Excelente producción.", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-5) },
                new NotaTecnica { Id = 5, ColmenaId = 5, Detalles = "Alza agregada.", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-7) },
                new NotaTecnica { Id = 6, ColmenaId = 6, Detalles = "Normal.", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-20) },
                new NotaTecnica { Id = 7, ColmenaId = 7, Detalles = "Humedad alta.", EstadoReina = "Presente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-35) },
                new NotaTecnica { Id = 8, ColmenaId = 49, Detalles = "Sin novedades", EstadoReina = "Cambiando", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-33) },
                new NotaTecnica { Id = 9, ColmenaId = 16, Detalles = "Reina joven", EstadoReina = "Cambiando", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-79) },
                new NotaTecnica { Id = 10, ColmenaId = 42, Detalles = "Sin novedades", EstadoReina = "Ausente", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-44) },
                new NotaTecnica { Id = 11, ColmenaId = 50, Detalles = "Cosecha parcial", EstadoReina = "Ausente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-80) },
                new NotaTecnica { Id = 12, ColmenaId = 22, Detalles = "Revisión de rutina", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-17) },
                new NotaTecnica { Id = 13, ColmenaId = 39, Detalles = "Alta presencia de zánganos", EstadoReina = "Cambiando", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-55) },
                new NotaTecnica { Id = 14, ColmenaId = 19, Detalles = "Cosecha parcial", EstadoReina = "Cambiando", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-39) },
                new NotaTecnica { Id = 15, ColmenaId = 49, Detalles = "Reina joven", EstadoReina = "Cambiando", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-55) },
                new NotaTecnica { Id = 16, ColmenaId = 35, Detalles = "Se limpió piso", EstadoReina = "Cambiando", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-85) },
                new NotaTecnica { Id = 17, ColmenaId = 38, Detalles = "Alta presencia de zánganos", EstadoReina = "Cambiando", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-16) },
                new NotaTecnica { Id = 18, ColmenaId = 2, Detalles = "Se limpió piso", EstadoReina = "Cambiando", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-1) },
                new NotaTecnica { Id = 19, ColmenaId = 36, Detalles = "Se alimentó", EstadoReina = "Cambiando", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-91) },
                new NotaTecnica { Id = 20, ColmenaId = 2, Detalles = "Alta presencia de zánganos", EstadoReina = "Cambiando", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-76) },
                new NotaTecnica { Id = 21, ColmenaId = 38, Detalles = "Se agregó cera", EstadoReina = "Cambiando", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-17) },
                new NotaTecnica { Id = 22, ColmenaId = 36, Detalles = "Sin novedades", EstadoReina = "Ausente", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-42) },
                new NotaTecnica { Id = 23, ColmenaId = 34, Detalles = "Se agregó cera", EstadoReina = "Presente", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-86) },
                new NotaTecnica { Id = 24, ColmenaId = 22, Detalles = "Se alimentó", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-69) },
                new NotaTecnica { Id = 25, ColmenaId = 31, Detalles = "Revisión de rutina", EstadoReina = "Ausente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-15) },
                new NotaTecnica { Id = 26, ColmenaId = 46, Detalles = "Se agregó cera", EstadoReina = "Ausente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-99) },
                new NotaTecnica { Id = 27, ColmenaId = 41, Detalles = "Cosecha parcial", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-58) },
                new NotaTecnica { Id = 28, ColmenaId = 43, Detalles = "Se agregó cera", EstadoReina = "Ausente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-97) },
                new NotaTecnica { Id = 29, ColmenaId = 41, Detalles = "Se alimentó", EstadoReina = "Ausente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-98) },
                new NotaTecnica { Id = 30, ColmenaId = 37, Detalles = "Se limpió piso", EstadoReina = "Cambiando", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-1) },
                new NotaTecnica { Id = 31, ColmenaId = 31, Detalles = "Se agregó cera", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-38) },
                new NotaTecnica { Id = 32, ColmenaId = 11, Detalles = "Alta presencia de zánganos", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-7) },
                new NotaTecnica { Id = 33, ColmenaId = 33, Detalles = "Sin novedades", EstadoReina = "Cambiando", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-55) },
                new NotaTecnica { Id = 34, ColmenaId = 44, Detalles = "Se limpió piso", EstadoReina = "Cambiando", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-81) },
                new NotaTecnica { Id = 35, ColmenaId = 7, Detalles = "Revisión de rutina", EstadoReina = "Cambiando", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-9) },
                new NotaTecnica { Id = 36, ColmenaId = 24, Detalles = "Se alimentó", EstadoReina = "Cambiando", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-99) },
                new NotaTecnica { Id = 37, ColmenaId = 21, Detalles = "Se limpió piso", EstadoReina = "Ausente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-90) },
                new NotaTecnica { Id = 38, ColmenaId = 19, Detalles = "Revisión de rutina", EstadoReina = "Presente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-1) },
                new NotaTecnica { Id = 39, ColmenaId = 3, Detalles = "Sin novedades", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-42) },
                new NotaTecnica { Id = 40, ColmenaId = 23, Detalles = "Cosecha parcial", EstadoReina = "Cambiando", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-32) },
                new NotaTecnica { Id = 41, ColmenaId = 5, Detalles = "Alta presencia de zánganos", EstadoReina = "Presente", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-97) },
                new NotaTecnica { Id = 42, ColmenaId = 8, Detalles = "Se alimentó", EstadoReina = "Presente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-14) },
                new NotaTecnica { Id = 43, ColmenaId = 11, Detalles = "Se agregó cera", EstadoReina = "Ausente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-51) },
                new NotaTecnica { Id = 44, ColmenaId = 31, Detalles = "Se alimentó", EstadoReina = "Presente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-24) },
                new NotaTecnica { Id = 45, ColmenaId = 32, Detalles = "Se alimentó", EstadoReina = "Cambiando", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-72) },
                new NotaTecnica { Id = 46, ColmenaId = 5, Detalles = "Se agregó cera", EstadoReina = "Cambiando", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-49) },
                new NotaTecnica { Id = 47, ColmenaId = 31, Detalles = "Alta presencia de zánganos", EstadoReina = "Presente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-74) },
                new NotaTecnica { Id = 48, ColmenaId = 21, Detalles = "Sin novedades", EstadoReina = "Cambiando", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-73) },
                new NotaTecnica { Id = 49, ColmenaId = 35, Detalles = "Se alimentó", EstadoReina = "Cambiando", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-57) },
                new NotaTecnica { Id = 50, ColmenaId = 5, Detalles = "Sin novedades", EstadoReina = "Ausente", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-13) },
                new NotaTecnica { Id = 51, ColmenaId = 17, Detalles = "Se alimentó", EstadoReina = "Ausente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-32) },
                new NotaTecnica { Id = 52, ColmenaId = 44, Detalles = "Se alimentó", EstadoReina = "Presente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-33) },
                new NotaTecnica { Id = 53, ColmenaId = 6, Detalles = "Sin novedades", EstadoReina = "Presente", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-16) },
                new NotaTecnica { Id = 54, ColmenaId = 19, Detalles = "Sin novedades", EstadoReina = "Ausente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-85) },
                new NotaTecnica { Id = 55, ColmenaId = 26, Detalles = "Sin novedades", EstadoReina = "Cambiando", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-26) },
                new NotaTecnica { Id = 56, ColmenaId = 2, Detalles = "Cosecha parcial", EstadoReina = "Ausente", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-47) },
                new NotaTecnica { Id = 57, ColmenaId = 50, Detalles = "Se alimentó", EstadoReina = "Ausente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-35) },
                new NotaTecnica { Id = 58, ColmenaId = 43, Detalles = "Se agregó cera", EstadoReina = "Ausente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-91) },
                new NotaTecnica { Id = 59, ColmenaId = 19, Detalles = "Se alimentó", EstadoReina = "Presente", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-55) },
                new NotaTecnica { Id = 60, ColmenaId = 50, Detalles = "Reina joven", EstadoReina = "Presente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-23) },
                new NotaTecnica { Id = 61, ColmenaId = 9, Detalles = "Cosecha parcial", EstadoReina = "Presente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-31) },
                new NotaTecnica { Id = 62, ColmenaId = 6, Detalles = "Se agregó cera", EstadoReina = "Ausente", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-7) },
                new NotaTecnica { Id = 63, ColmenaId = 24, Detalles = "Alta presencia de zánganos", EstadoReina = "Cambiando", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-73) },
                new NotaTecnica { Id = 64, ColmenaId = 32, Detalles = "Se limpió piso", EstadoReina = "Presente", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-49) },
                new NotaTecnica { Id = 65, ColmenaId = 20, Detalles = "Alta presencia de zánganos", EstadoReina = "Cambiando", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-3) },
                new NotaTecnica { Id = 66, ColmenaId = 13, Detalles = "Se alimentó", EstadoReina = "Ausente", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-55) },
                new NotaTecnica { Id = 67, ColmenaId = 44, Detalles = "Alta presencia de zánganos", EstadoReina = "Ausente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-60) },
                new NotaTecnica { Id = 68, ColmenaId = 23, Detalles = "Cosecha parcial", EstadoReina = "Ausente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-8) },
                new NotaTecnica { Id = 69, ColmenaId = 5, Detalles = "Reina joven", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-58) },
                new NotaTecnica { Id = 70, ColmenaId = 22, Detalles = "Se agregó cera", EstadoReina = "Cambiando", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-79) },
                new NotaTecnica { Id = 71, ColmenaId = 22, Detalles = "Revisión de rutina", EstadoReina = "Presente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-56) },
                new NotaTecnica { Id = 72, ColmenaId = 18, Detalles = "Reina joven", EstadoReina = "Ausente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-76) },
                new NotaTecnica { Id = 73, ColmenaId = 27, Detalles = "Reina joven", EstadoReina = "Cambiando", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-13) },
                new NotaTecnica { Id = 74, ColmenaId = 47, Detalles = "Se agregó cera", EstadoReina = "Cambiando", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-23) },
                new NotaTecnica { Id = 75, ColmenaId = 8, Detalles = "Sin novedades", EstadoReina = "Ausente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-56) },
                new NotaTecnica { Id = 76, ColmenaId = 9, Detalles = "Se agregó cera", EstadoReina = "Ausente", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-19) },
                new NotaTecnica { Id = 77, ColmenaId = 9, Detalles = "Sin novedades", EstadoReina = "Cambiando", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-31) },
                new NotaTecnica { Id = 78, ColmenaId = 13, Detalles = "Se limpió piso", EstadoReina = "Ausente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-16) },
                new NotaTecnica { Id = 79, ColmenaId = 7, Detalles = "Sin novedades", EstadoReina = "Cambiando", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-49) },
                new NotaTecnica { Id = 80, ColmenaId = 40, Detalles = "Cosecha parcial", EstadoReina = "Cambiando", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-37) }
            );

            // Seed inicial de Treatments y TreatmentEquipments
            modelBuilder.Entity<Treatment>().HasData(
                new Treatment { Id = 1, ColmenaId = 1, Titulo = "Aplicación Ácido Oxálico", Tipo = "Medicinal", Nota = "Tratamiento por goteo. Dosis estándar 50ml por colmena. Temperatura ambiente 18°C.", Fecha = new DateTime(2025, 10, 12, 14, 30, 0) },
                new Treatment { Id = 2, ColmenaId = 1, Titulo = "Alimentación de Soporte", Tipo = "Mantenimiento", Nota = "Jarabe de azúcar 2:1. 2 Litros suministrados en alimentador de techo.", Fecha = new DateTime(2025, 08, 28, 9, 15, 0) },
                new Treatment { Id = 3, ColmenaId = 45, Titulo = "Goteo Ácido", Tipo = "Preventivo", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-125) },
                new Treatment { Id = 4, ColmenaId = 40, Titulo = "Goteo Ácido", Tipo = "Preventivo", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-180) },
                new Treatment { Id = 5, ColmenaId = 29, Titulo = "Aplicación Amitraz", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-187) },
                new Treatment { Id = 6, ColmenaId = 8, Titulo = "Revisión Sanitaria", Tipo = "Preventivo", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-92) },
                new Treatment { Id = 7, ColmenaId = 44, Titulo = "Vitaminas", Tipo = "Mantenimiento", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-107) },
                new Treatment { Id = 8, ColmenaId = 41, Titulo = "Goteo Ácido", Tipo = "Preventivo", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-18) },
                new Treatment { Id = 9, ColmenaId = 18, Titulo = "Alimentación Proteica", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-35) },
                new Treatment { Id = 10, ColmenaId = 32, Titulo = "Aplicación Amitraz", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-68) },
                new Treatment { Id = 11, ColmenaId = 7, Titulo = "Revisión Sanitaria", Tipo = "Preventivo", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-177) },
                new Treatment { Id = 12, ColmenaId = 36, Titulo = "Aplicación Amitraz", Tipo = "Mantenimiento", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-166) },
                new Treatment { Id = 13, ColmenaId = 24, Titulo = "Revisión Sanitaria", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-91) },
                new Treatment { Id = 14, ColmenaId = 36, Titulo = "Aplicación Amitraz", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-114) },
                new Treatment { Id = 15, ColmenaId = 12, Titulo = "Goteo Ácido", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-86) },
                new Treatment { Id = 16, ColmenaId = 28, Titulo = "Vitaminas", Tipo = "Mantenimiento", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-14) },
                new Treatment { Id = 17, ColmenaId = 23, Titulo = "Aplicación Amitraz", Tipo = "Preventivo", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-62) },
                new Treatment { Id = 18, ColmenaId = 25, Titulo = "Aplicación Amitraz", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-49) },
                new Treatment { Id = 19, ColmenaId = 19, Titulo = "Aplicación Amitraz", Tipo = "Preventivo", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-106) },
                new Treatment { Id = 20, ColmenaId = 6, Titulo = "Revisión Sanitaria", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-44) },
                new Treatment { Id = 21, ColmenaId = 44, Titulo = "Aplicación Amitraz", Tipo = "Preventivo", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-149) },
                new Treatment { Id = 22, ColmenaId = 11, Titulo = "Aplicación Amitraz", Tipo = "Mantenimiento", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-50) },
                new Treatment { Id = 23, ColmenaId = 42, Titulo = "Vitaminas", Tipo = "Preventivo", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-150) },
                new Treatment { Id = 24, ColmenaId = 49, Titulo = "Aplicación Amitraz", Tipo = "Mantenimiento", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-25) },
                new Treatment { Id = 25, ColmenaId = 12, Titulo = "Aplicación Amitraz", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-56) },
                new Treatment { Id = 26, ColmenaId = 41, Titulo = "Revisión Sanitaria", Tipo = "Preventivo", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-25) },
                new Treatment { Id = 27, ColmenaId = 44, Titulo = "Aplicación Amitraz", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-198) },
                new Treatment { Id = 28, ColmenaId = 3, Titulo = "Revisión Sanitaria", Tipo = "Mantenimiento", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-163) },
                new Treatment { Id = 29, ColmenaId = 41, Titulo = "Vitaminas", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-66) },
                new Treatment { Id = 30, ColmenaId = 10, Titulo = "Alimentación Proteica", Tipo = "Mantenimiento", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-134) },
                new Treatment { Id = 31, ColmenaId = 26, Titulo = "Aplicación Amitraz", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-131) },
                new Treatment { Id = 32, ColmenaId = 5, Titulo = "Alimentación Proteica", Tipo = "Mantenimiento", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-36) },
                new Treatment { Id = 33, ColmenaId = 39, Titulo = "Aplicación Amitraz", Tipo = "Preventivo", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-153) },
                new Treatment { Id = 34, ColmenaId = 23, Titulo = "Alimentación Proteica", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-148) },
                new Treatment { Id = 35, ColmenaId = 34, Titulo = "Alimentación Proteica", Tipo = "Preventivo", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-130) },
                new Treatment { Id = 36, ColmenaId = 4, Titulo = "Aplicación Amitraz", Tipo = "Preventivo", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-97) },
                new Treatment { Id = 37, ColmenaId = 46, Titulo = "Vitaminas", Tipo = "Mantenimiento", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-96) },
                new Treatment { Id = 38, ColmenaId = 23, Titulo = "Vitaminas", Tipo = "Medicinal", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-30) },
                new Treatment { Id = 39, ColmenaId = 12, Titulo = "Goteo Ácido", Tipo = "Preventivo", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-110) },
                new Treatment { Id = 40, ColmenaId = 39, Titulo = "Vitaminas", Tipo = "Mantenimiento", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-121) }
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
                new Movimiento { Id = 6, ColmenaId = 8, ApiarioOrigenId = 5, ApiarioDestinoId = 3, Razon = "Floración Eucalyptus", FechaSalida = DateTime.Now.AddDays(-45), FechaRegreso = DateTime.Now.AddDays(-40), Estado = "Vigente" },
                new Movimiento { Id = 7, ColmenaId = 50, ApiarioOrigenId = 5, ApiarioDestinoId = 2, Razon = "Cuarentena", FechaSalida = DateTime.Now.AddDays(-99), FechaRegreso = DateTime.Now.AddDays(-99), Estado = "Cancelado" },
                new Movimiento { Id = 8, ColmenaId = 50, ApiarioOrigenId = 3, ApiarioDestinoId = 1, Razon = "Cuarentena", FechaSalida = DateTime.Now.AddDays(-86), FechaRegreso = DateTime.Now.AddDays(-81), Estado = "Completado" },
                new Movimiento { Id = 9, ColmenaId = 39, ApiarioOrigenId = 3, ApiarioDestinoId = 1, Razon = "Cuarentena", FechaSalida = DateTime.Now.AddDays(-10), FechaRegreso = DateTime.Now.AddDays(-5), Estado = "Completado" },
                new Movimiento { Id = 10, ColmenaId = 25, ApiarioOrigenId = 3, ApiarioDestinoId = 2, Razon = "Venta/Préstamo", FechaSalida = DateTime.Now.AddDays(-68), FechaRegreso = DateTime.Now.AddDays(-66), Estado = "Completado" },
                new Movimiento { Id = 11, ColmenaId = 15, ApiarioOrigenId = 1, ApiarioDestinoId = 2, Razon = "Floración Pradera", FechaSalida = DateTime.Now.AddDays(-44), FechaRegreso = DateTime.Now.AddDays(-39), Estado = "Vigente" },
                new Movimiento { Id = 12, ColmenaId = 46, ApiarioOrigenId = 5, ApiarioDestinoId = 2, Razon = "Refugio Invernal", FechaSalida = DateTime.Now.AddDays(-41), FechaRegreso = DateTime.Now.AddDays(-37), Estado = "Completado" },
                new Movimiento { Id = 13, ColmenaId = 14, ApiarioOrigenId = 2, ApiarioDestinoId = 1, Razon = "Cuarentena", FechaSalida = DateTime.Now.AddDays(-74), FechaRegreso = DateTime.Now.AddDays(-68), Estado = "Completado" },
                new Movimiento { Id = 14, ColmenaId = 41, ApiarioOrigenId = 2, ApiarioDestinoId = 5, Razon = "Venta/Préstamo", FechaSalida = DateTime.Now.AddDays(-75), FechaRegreso = DateTime.Now.AddDays(-70), Estado = "Cancelado" },
                new Movimiento { Id = 15, ColmenaId = 12, ApiarioOrigenId = 2, ApiarioDestinoId = 4, Razon = "Floración Pradera", FechaSalida = DateTime.Now.AddDays(-35), FechaRegreso = DateTime.Now.AddDays(-45), Estado = "Completado" },
                new Movimiento { Id = 16, ColmenaId = 47, ApiarioOrigenId = 3, ApiarioDestinoId = 1, Razon = "Refugio Invernal", FechaSalida = DateTime.Now.AddDays(-49), FechaRegreso = DateTime.Now.AddDays(-66), Estado = "Vigente" },
                new Movimiento { Id = 17, ColmenaId = 38, ApiarioOrigenId = 5, ApiarioDestinoId = 3, Razon = "Refugio Invernal", FechaSalida = DateTime.Now.AddDays(-89), FechaRegreso = DateTime.Now.AddDays(-99), Estado = "Completado" },
                new Movimiento { Id = 18, ColmenaId = 6, ApiarioOrigenId = 3, ApiarioDestinoId = 2, Razon = "Refugio Invernal", FechaSalida = DateTime.Now.AddDays(-18), FechaRegreso = DateTime.Now.AddDays(-12), Estado = "Vigente" },
                new Movimiento { Id = 19, ColmenaId = 49, ApiarioOrigenId = 1, ApiarioDestinoId = 5, Razon = "Venta/Préstamo", FechaSalida = DateTime.Now.AddDays(-24), FechaRegreso = DateTime.Now.AddDays(-29), Estado = "Vigente" },
                new Movimiento { Id = 20, ColmenaId = 15, ApiarioOrigenId = 5, ApiarioDestinoId = 4, Razon = "Venta/Préstamo", FechaSalida = DateTime.Now.AddDays(-58), FechaRegreso = DateTime.Now.AddDays(-55), Estado = "Cancelado" },
                new Movimiento { Id = 21, ColmenaId = 10, ApiarioOrigenId = 1, ApiarioDestinoId = 4, Razon = "Refugio Invernal", FechaSalida = DateTime.Now.AddDays(-79), FechaRegreso = DateTime.Now.AddDays(-78), Estado = "Vigente" },
                new Movimiento { Id = 22, ColmenaId = 8, ApiarioOrigenId = 3, ApiarioDestinoId = 5, Razon = "Floración Eucalyptus", FechaSalida = DateTime.Now.AddDays(-84), FechaRegreso = DateTime.Now.AddDays(-68), Estado = "Vigente" },
                new Movimiento { Id = 23, ColmenaId = 41, ApiarioOrigenId = 1, ApiarioDestinoId = 4, Razon = "Refugio Invernal", FechaSalida = DateTime.Now.AddDays(-12), FechaRegreso = DateTime.Now.AddDays(3), Estado = "Cancelado" },
                new Movimiento { Id = 24, ColmenaId = 9, ApiarioOrigenId = 5, ApiarioDestinoId = 3, Razon = "Refugio Invernal", FechaSalida = DateTime.Now.AddDays(-90), FechaRegreso = DateTime.Now.AddDays(-103), Estado = "Cancelado" },
                new Movimiento { Id = 25, ColmenaId = 41, ApiarioOrigenId = 1, ApiarioDestinoId = 5, Razon = "Refugio Invernal", FechaSalida = DateTime.Now.AddDays(-94), FechaRegreso = DateTime.Now.AddDays(-76), Estado = "Cancelado" },
                new Movimiento { Id = 26, ColmenaId = 13, ApiarioOrigenId = 1, ApiarioDestinoId = 5, Razon = "Cuarentena", FechaSalida = DateTime.Now.AddDays(-27), FechaRegreso = DateTime.Now.AddDays(-44), Estado = "Cancelado" },
                new Movimiento { Id = 27, ColmenaId = 49, ApiarioOrigenId = 4, ApiarioDestinoId = 3, Razon = "Cuarentena", FechaSalida = DateTime.Now.AddDays(-83), FechaRegreso = DateTime.Now.AddDays(-92), Estado = "Cancelado" },
                new Movimiento { Id = 28, ColmenaId = 2, ApiarioOrigenId = 5, ApiarioDestinoId = 2, Razon = "Cuarentena", FechaSalida = DateTime.Now.AddDays(-25), FechaRegreso = DateTime.Now.AddDays(-27), Estado = "Vigente" },
                new Movimiento { Id = 29, ColmenaId = 25, ApiarioOrigenId = 1, ApiarioDestinoId = 2, Razon = "Venta/Préstamo", FechaSalida = DateTime.Now.AddDays(-11), FechaRegreso = DateTime.Now.AddDays(-16), Estado = "Completado" },
                new Movimiento { Id = 30, ColmenaId = 9, ApiarioOrigenId = 5, ApiarioDestinoId = 4, Razon = "Venta/Préstamo", FechaSalida = DateTime.Now.AddDays(-85), FechaRegreso = DateTime.Now.AddDays(-84), Estado = "Completado" }
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
                new Inversion { Id = 6, AnalisisId = 3, Titulo = "Alimentación", Nota = "Gasto operativo", Precio = 2687.66 },
                new Inversion { Id = 7, AnalisisId = 4, Titulo = "Alimentación", Nota = "Gasto operativo", Precio = 3094.51 },
                new Inversion { Id = 8, AnalisisId = 4, Titulo = "Compra Reinas", Nota = "Gasto operativo", Precio = 3579.79 },
                new Inversion { Id = 9, AnalisisId = 4, Titulo = "Reparación Camioneta", Nota = "Gasto operativo", Precio = 9587.91 },
                new Inversion { Id = 10, AnalisisId = 1, Titulo = "Herramientas", Nota = "Gasto operativo", Precio = 3088.72 },
                new Inversion { Id = 11, AnalisisId = 3, Titulo = "Reparación Camioneta", Nota = "Gasto operativo", Precio = 3988.17 },
                new Inversion { Id = 12, AnalisisId = 2, Titulo = "Suministros", Nota = "Gasto operativo", Precio = 8649.10 },
                new Inversion { Id = 13, AnalisisId = 4, Titulo = "Herramientas", Nota = "Gasto operativo", Precio = 1507.46 },
                new Inversion { Id = 14, AnalisisId = 4, Titulo = "Alimentación", Nota = "Gasto operativo", Precio = 6914.10 },
                new Inversion { Id = 15, AnalisisId = 4, Titulo = "Reparación Camioneta", Nota = "Gasto operativo", Precio = 2655.22 },
                new Inversion { Id = 16, AnalisisId = 3, Titulo = "Combustible", Nota = "Gasto operativo", Precio = 6119.35 },
                new Inversion { Id = 17, AnalisisId = 3, Titulo = "Herramientas", Nota = "Gasto operativo", Precio = 1610.88 },
                new Inversion { Id = 18, AnalisisId = 1, Titulo = "Reparación Camioneta", Nota = "Gasto operativo", Precio = 9094.79 },
                new Inversion { Id = 19, AnalisisId = 4, Titulo = "Reparación Camioneta", Nota = "Gasto operativo", Precio = 6443.40 },
                new Inversion { Id = 20, AnalisisId = 1, Titulo = "Herramientas", Nota = "Gasto operativo", Precio = 2031.97 },
                new Inversion { Id = 21, AnalisisId = 4, Titulo = "Alimentación", Nota = "Gasto operativo", Precio = 7405.31 },
                new Inversion { Id = 22, AnalisisId = 4, Titulo = "Compra Reinas", Nota = "Gasto operativo", Precio = 783.29 },
                new Inversion { Id = 23, AnalisisId = 2, Titulo = "Compra Reinas", Nota = "Gasto operativo", Precio = 9321.67 },
                new Inversion { Id = 24, AnalisisId = 3, Titulo = "Herramientas", Nota = "Gasto operativo", Precio = 9044.08 },
                new Inversion { Id = 25, AnalisisId = 1, Titulo = "Reparación Camioneta", Nota = "Gasto operativo", Precio = 8804.26 },
                new Inversion { Id = 26, AnalisisId = 4, Titulo = "Suministros", Nota = "Gasto operativo", Precio = 5158.11 },
                new Inversion { Id = 27, AnalisisId = 1, Titulo = "Combustible", Nota = "Gasto operativo", Precio = 551.73 },
                new Inversion { Id = 28, AnalisisId = 1, Titulo = "Alimentación", Nota = "Gasto operativo", Precio = 6619.09 },
                new Inversion { Id = 29, AnalisisId = 2, Titulo = "Suministros", Nota = "Gasto operativo", Precio = 4314.72 },
                new Inversion { Id = 30, AnalisisId = 2, Titulo = "Combustible", Nota = "Gasto operativo", Precio = 901.85 },
                new Inversion { Id = 31, AnalisisId = 4, Titulo = "Suministros", Nota = "Gasto operativo", Precio = 7784.76 },
                new Inversion { Id = 32, AnalisisId = 4, Titulo = "Alimentación", Nota = "Gasto operativo", Precio = 8694.29 },
                new Inversion { Id = 33, AnalisisId = 1, Titulo = "Compra Reinas", Nota = "Gasto operativo", Precio = 1176.13 },
                new Inversion { Id = 34, AnalisisId = 2, Titulo = "Combustible", Nota = "Gasto operativo", Precio = 6370.05 },
                new Inversion { Id = 35, AnalisisId = 1, Titulo = "Herramientas", Nota = "Gasto operativo", Precio = 2119.51 },
                new Inversion { Id = 36, AnalisisId = 2, Titulo = "Suministros", Nota = "Gasto operativo", Precio = 5972.88 },
                new Inversion { Id = 37, AnalisisId = 1, Titulo = "Combustible", Nota = "Gasto operativo", Precio = 7188.13 },
                new Inversion { Id = 38, AnalisisId = 1, Titulo = "Alimentación", Nota = "Gasto operativo", Precio = 7715.77 },
                new Inversion { Id = 39, AnalisisId = 3, Titulo = "Alimentación", Nota = "Gasto operativo", Precio = 7182.65 },
                new Inversion { Id = 40, AnalisisId = 4, Titulo = "Compra Reinas", Nota = "Gasto operativo", Precio = 8679.61 }
            );

            // Seed inicial de Ganancias
            modelBuilder.Entity<Ganancia>().HasData(
                new Ganancia { Id = 1, AnalisisId = 1, Titulo = "Venta de Miel (850 kg)", Descripcion = "Precio: $35/kg", Monto = 29750.0 },
                new Ganancia { Id = 2, AnalisisId = 1, Titulo = "Venta de Núcleos (20 u.)", Descripcion = "Precio: $350/u.", Monto = 7000.0 },
                new Ganancia { Id = 3, AnalisisId = 1, Titulo = "Venta de Polen (50 kg)", Descripcion = "Precio: $35/kg", Monto = 1750.0 },
                new Ganancia { Id = 4, AnalisisId = 2, Titulo = "Venta anticipada de propóleo", Descripcion = "Reserva de lote", Monto = 3500.0 },
                new Ganancia { Id = 5, AnalisisId = 4, Titulo = "Propóleo", Descripcion = "Ingreso operativo", Monto = 5256.79 },
                new Ganancia { Id = 6, AnalisisId = 3, Titulo = "Polinización", Descripcion = "Ingreso operativo", Monto = 27479.83 },
                new Ganancia { Id = 7, AnalisisId = 3, Titulo = "Cera", Descripcion = "Ingreso operativo", Monto = 17299.50 },
                new Ganancia { Id = 8, AnalisisId = 1, Titulo = "Polinización", Descripcion = "Ingreso operativo", Monto = 27213.24 },
                new Ganancia { Id = 9, AnalisisId = 2, Titulo = "Propóleo", Descripcion = "Ingreso operativo", Monto = 8386.14 },
                new Ganancia { Id = 10, AnalisisId = 4, Titulo = "Polinización", Descripcion = "Ingreso operativo", Monto = 28453.92 },
                new Ganancia { Id = 11, AnalisisId = 2, Titulo = "Propóleo", Descripcion = "Ingreso operativo", Monto = 10812.12 },
                new Ganancia { Id = 12, AnalisisId = 3, Titulo = "Polinización", Descripcion = "Ingreso operativo", Monto = 27978.11 },
                new Ganancia { Id = 13, AnalisisId = 3, Titulo = "Propóleo", Descripcion = "Ingreso operativo", Monto = 21157.56 },
                new Ganancia { Id = 14, AnalisisId = 1, Titulo = "Venta Núcleos", Descripcion = "Ingreso operativo", Monto = 14131.12 },
                new Ganancia { Id = 15, AnalisisId = 1, Titulo = "Venta Núcleos", Descripcion = "Ingreso operativo", Monto = 20849.25 },
                new Ganancia { Id = 16, AnalisisId = 2, Titulo = "Cera", Descripcion = "Ingreso operativo", Monto = 10935.55 },
                new Ganancia { Id = 17, AnalisisId = 3, Titulo = "Cera", Descripcion = "Ingreso operativo", Monto = 11866.03 },
                new Ganancia { Id = 18, AnalisisId = 4, Titulo = "Propóleo", Descripcion = "Ingreso operativo", Monto = 11593.17 },
                new Ganancia { Id = 19, AnalisisId = 3, Titulo = "Venta Núcleos", Descripcion = "Ingreso operativo", Monto = 17699.20 },
                new Ganancia { Id = 20, AnalisisId = 4, Titulo = "Polinización", Descripcion = "Ingreso operativo", Monto = 21313.94 },
                new Ganancia { Id = 21, AnalisisId = 2, Titulo = "Venta Tambor Miel", Descripcion = "Ingreso operativo", Monto = 11630.39 },
                new Ganancia { Id = 22, AnalisisId = 1, Titulo = "Cera", Descripcion = "Ingreso operativo", Monto = 19774.92 },
                new Ganancia { Id = 23, AnalisisId = 1, Titulo = "Propóleo", Descripcion = "Ingreso operativo", Monto = 5849.98 },
                new Ganancia { Id = 24, AnalisisId = 4, Titulo = "Propóleo", Descripcion = "Ingreso operativo", Monto = 20233.69 },
                new Ganancia { Id = 25, AnalisisId = 1, Titulo = "Propóleo", Descripcion = "Ingreso operativo", Monto = 17946.85 },
                new Ganancia { Id = 26, AnalisisId = 1, Titulo = "Polinización", Descripcion = "Ingreso operativo", Monto = 17099.26 },
                new Ganancia { Id = 27, AnalisisId = 4, Titulo = "Venta Núcleos", Descripcion = "Ingreso operativo", Monto = 27166.48 },
                new Ganancia { Id = 28, AnalisisId = 1, Titulo = "Cera", Descripcion = "Ingreso operativo", Monto = 29364.90 },
                new Ganancia { Id = 29, AnalisisId = 1, Titulo = "Venta Núcleos", Descripcion = "Ingreso operativo", Monto = 25355.01 },
                new Ganancia { Id = 30, AnalisisId = 1, Titulo = "Venta Tambor Miel", Descripcion = "Ingreso operativo", Monto = 15200.74 }
            );

            // Seed inicial de Declaraciones Juradas
            modelBuilder.Entity<DeclaracionJurada>().HasData(
                new DeclaracionJurada { Id = 1, FechaEntrega = new DateTime(2025, 7, 21) }
            );
        }
    }
}
