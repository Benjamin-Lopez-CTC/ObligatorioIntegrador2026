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
                new Equipment { Id = 1, Name = "Ahumador Inoxidable", Type = "Ahumador estándar 10x25cm", Stock = 12, Category = "Herramienta", LowThreshold = 5, MediumThreshold = 15, DisplayOrder = 1 },
                new Equipment { Id = 2, Name = "Palanca de Manejo", Type = "Pinza y palanca universal", Stock = 24, Category = "Herramienta", LowThreshold = 10, MediumThreshold = 20, DisplayOrder = 2 },
                new Equipment { Id = 3, Name = "Ácido Oxálico (Glicerina)", Type = "Tratamiento Varroa", Stock = 50, Category = "Medicamento", LowThreshold = 15, MediumThreshold = 40, DisplayOrder = 3 },
                new Equipment { Id = 4, Name = "Amitraz (Tiras)", Type = "Tratamiento Varroa", Stock = 15, Category = "Medicamento", LowThreshold = 10, MediumThreshold = 25, DisplayOrder = 4 },
                new Equipment { Id = 5, Name = "Alzas Melarias (Media)", Type = "Madera", Stock = 120, Category = "Material", LowThreshold = 30, MediumThreshold = 80, DisplayOrder = 5 },
                new Equipment { Id = 6, Name = "Marcos Alambrados", Type = "Madera/Alambre", Stock = 450, Category = "Material", LowThreshold = 100, MediumThreshold = 200, DisplayOrder = 6 },
                new Equipment { Id = 7, Name = "Cera Estampada", Type = "Cera Orgánica", Stock = 5, Category = "Material", LowThreshold = 10, MediumThreshold = 25, DisplayOrder = 7 }
            );

            // Seed inicial de Apiarios
            modelBuilder.Entity<Apiario>().HasData(
                new Apiario { Id = 1, Nombre = "Apiario Norte", StringIdentificador = "001", UbicacionTexto = "Valle Central", UbicacionCoordenadas = "34°05'12.1\"S 70°45'22.4\"W", FechaCreacion = new DateTime(2023, 10, 12), UltimaInspeccion = new DateTime(2026, 10, 12), Responsable = "Ing. Martín Valenzuela", NotasEstado = "Zona con alta floración esperada para el próximo mes. Preparar alzas melarias adicionales. Monitoreo preventivo de Varroa recomendado.", HumedadInterna = 58 },
                new Apiario { Id = 2, Nombre = "Apiario Sur", StringIdentificador = "002", UbicacionTexto = "Colinas Bajas", UbicacionCoordenadas = "34°08'10.2\"S 70°42'15.4\"W", FechaCreacion = new DateTime(2024, 02, 05), UltimaInspeccion = new DateTime(2026, 10, 05), Responsable = "Ing. Martín Valenzuela", NotasEstado = "Revisar posibles daños por viento en el sector este.", HumedadInterna = 45 },
                new Apiario { Id = 3, Nombre = "Apiario Este", StringIdentificador = "003", UbicacionTexto = "Bosque Nativo", UbicacionCoordenadas = "34°10'05.1\"S 70°40'00.0\"W", FechaCreacion = new DateTime(2025, 01, 15), UltimaInspeccion = new DateTime(2026, 10, 15), Responsable = "Benjamin Lopez", NotasEstado = "Producción crítica, requiere revisión urgente de reinas.", HumedadInterna = 62 }
            );

            // Seed inicial de Colmenas
            modelBuilder.Entity<Colmena>().HasData(
                new Colmena { Id = 1, Identificador = "#HIVE-0042", CodigoEscaneo = "100001", ApiarioId = 1, Estado = "Óptimo", PesoKg = 45.2, TemperaturaInterna = 34.5, HumedadInterna = 55.0, ProduccionMielKg = 40.0, EsPiloto = true, CantidadAbejas = 45000, UbicacionIntraApiario = "Fila 1, Pos 1", ComportamientoAbejas = "Dócil", EstadoReina = "Presente", UltimaNotaTecnica = "Revisión general, todo normal.", FechaUltimaNota = DateTime.Now.AddDays(-10) },
                new Colmena { Id = 2, Identificador = "#HIVE-0089", CodigoEscaneo = "100002", ApiarioId = 1, Estado = "Alerta", PesoKg = 42.8, TemperaturaInterna = 32.0, HumedadInterna = 0, ProduccionMielKg = 35.0, EsPiloto = false, CantidadAbejas = 38000, UbicacionIntraApiario = "Fila 1, Pos 2", ComportamientoAbejas = "Defensivo", EstadoReina = "Presente", UltimaNotaTecnica = "Abejas defensivas, observar.", FechaUltimaNota = DateTime.Now.AddDays(-15) },
                new Colmena { Id = 3, Identificador = "#HIVE-0112", CodigoEscaneo = "100003", ApiarioId = 1, Estado = "Crítico", PesoKg = 31.0, TemperaturaInterna = 36.5, HumedadInterna = 60.0, ProduccionMielKg = 20.0, EsPiloto = true, CantidadAbejas = 15000, UbicacionIntraApiario = "Fila 2, Pos 1", ComportamientoAbejas = "Agresivo", EstadoReina = "Ausente", UltimaNotaTecnica = "Reina no avistada. Posible enjambrazón.", FechaUltimaNota = DateTime.Now.AddDays(-2) },
                new Colmena { Id = 4, Identificador = "#HIVE-0045", CodigoEscaneo = "100004", ApiarioId = 1, Estado = "Óptimo", PesoKg = 48.1, TemperaturaInterna = 34.2, HumedadInterna = 58.0, ProduccionMielKg = 45.0, EsPiloto = true, CantidadAbejas = 50000, UbicacionIntraApiario = "Fila 2, Pos 2", ComportamientoAbejas = "Dócil", EstadoReina = "Presente", UltimaNotaTecnica = "Excelente producción.", FechaUltimaNota = DateTime.Now.AddDays(-5) },
                new Colmena { Id = 5, Identificador = "#HIVE-0001", CodigoEscaneo = "100005", ApiarioId = 2, Estado = "Óptimo", PesoKg = 40.0, TemperaturaInterna = 35.1, HumedadInterna = 52.0, ProduccionMielKg = 30.0, EsPiloto = true, CantidadAbejas = 42000, UbicacionIntraApiario = "Fila 1, Pos 1", ComportamientoAbejas = "Dócil", EstadoReina = "Presente", UltimaNotaTecnica = "Alza agregada.", FechaUltimaNota = DateTime.Now.AddDays(-7) },
                new Colmena { Id = 6, Identificador = "#HIVE-0002", CodigoEscaneo = "100006", ApiarioId = 2, Estado = "Óptimo", PesoKg = 39.5, TemperaturaInterna = 34.8, HumedadInterna = 0, ProduccionMielKg = 30.0, EsPiloto = false, CantidadAbejas = 41000, UbicacionIntraApiario = "Fila 1, Pos 2", ComportamientoAbejas = "Dócil", EstadoReina = "Presente", UltimaNotaTecnica = "Normal.", FechaUltimaNota = DateTime.Now.AddDays(-20) },
                new Colmena { Id = 7, Identificador = "#HIVE-0003", CodigoEscaneo = "100007", ApiarioId = 3, Estado = "Crítico", PesoKg = 25.0, TemperaturaInterna = 30.0, HumedadInterna = 82.0, ProduccionMielKg = 10.0, EsPiloto = true, CantidadAbejas = 12000, UbicacionIntraApiario = "Única", ComportamientoAbejas = "Agresivo", EstadoReina = "Presente", UltimaNotaTecnica = "Humedad alta.", FechaUltimaNota = DateTime.Now.AddDays(-35) }
            );

            // Seed inicial de Treatments y TreatmentEquipments
            modelBuilder.Entity<Treatment>().HasData(
                new Treatment { Id = 1, ColmenaId = 1, Titulo = "Aplicación Ácido Oxálico", Tipo = "Medicinal", Nota = "Tratamiento por goteo. Dosis estándar 50ml por colmena. Temperatura ambiente 18°C.", Fecha = new DateTime(2025, 10, 12, 14, 30, 0) },
                new Treatment { Id = 2, ColmenaId = 1, Titulo = "Alimentación de Soporte", Tipo = "Mantenimiento", Nota = "Jarabe de azúcar 2:1. 2 Litros suministrados en alimentador de techo.", Fecha = new DateTime(2025, 08, 28, 9, 15, 0) }
            );

            modelBuilder.Entity<TreatmentEquipment>().HasData(
                new TreatmentEquipment { Id = 1, TreatmentId = 1, EquipmentName = "Ácido Oxálico (Glicerina)", Cantidad = 1 }
            );
        }
    }
}
