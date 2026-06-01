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
                new Equipment { Id = 7, Name = "Cera Estampada", Type = "Cera Orgánica", Stock = 5, Category = "Material", LowThreshold = 10, MediumThreshold = 25, DisplayOrder = 7, UnitPrice = 120.0, Currency = "UYU" }
            );

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
                new Colmena { Id = 7, Identificador = "#HIVE-0003", CodigoEscaneo = "100007", ApiarioId = 5, Estado = "Crítico", PesoKg = 25.0, TemperaturaInterna = 30.0, HumedadInterna = 82.0, ProduccionMielKg = 10.0, EsPiloto = true, EsNucleo = true, CantidadAbejas = 12000, UbicacionIntraApiario = "Única", ComportamientoAbejas = "Agresivo", EstadoReina = "Presente" }
            );

            // Seed inicial de Notas Tecnicas
            modelBuilder.Entity<NotaTecnica>().HasData(
                new NotaTecnica { Id = 1, ColmenaId = 1, Detalles = "Revisión general, todo normal.", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-10) },
                new NotaTecnica { Id = 2, ColmenaId = 2, Detalles = "Abejas defensivas, observar.", EstadoReina = "Presente", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-15) },
                new NotaTecnica { Id = 3, ColmenaId = 3, Detalles = "Reina no avistada. Posible enjambrazón.", EstadoReina = "Ausente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-2) },
                new NotaTecnica { Id = 4, ColmenaId = 4, Detalles = "Excelente producción.", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-5) },
                new NotaTecnica { Id = 5, ColmenaId = 5, Detalles = "Alza agregada.", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-7) },
                new NotaTecnica { Id = 6, ColmenaId = 6, Detalles = "Normal.", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-20) },
                new NotaTecnica { Id = 7, ColmenaId = 7, Detalles = "Humedad alta.", EstadoReina = "Presente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-35) }
            );

            // Seed inicial de Treatments y TreatmentEquipments
            modelBuilder.Entity<Treatment>().HasData(
                new Treatment { Id = 1, ColmenaId = 1, Titulo = "Aplicación Ácido Oxálico", Tipo = "Medicinal", Nota = "Tratamiento por goteo. Dosis estándar 50ml por colmena. Temperatura ambiente 18°C.", Fecha = new DateTime(2025, 10, 12, 14, 30, 0) },
                new Treatment { Id = 2, ColmenaId = 1, Titulo = "Alimentación de Soporte", Tipo = "Mantenimiento", Nota = "Jarabe de azúcar 2:1. 2 Litros suministrados en alimentador de techo.", Fecha = new DateTime(2025, 08, 28, 9, 15, 0) }
            );

            modelBuilder.Entity<TreatmentEquipment>().HasData(
                new TreatmentEquipment { Id = 1, TreatmentId = 1, EquipmentName = "Ácido Oxálico (Glicerina)", Cantidad = 1 }
            );

            // Relaciones de Movimiento para evitar ciclos de cascada
            modelBuilder.Entity<Movimiento>()
                .HasOne(m => m.ApiarioOrigen)
                .WithMany()
                .HasForeignKey(m => m.ApiarioOrigenId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Movimiento>()
                .HasOne(m => m.ApiarioDestino)
                .WithMany()
                .HasForeignKey(m => m.ApiarioDestinoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed inicial de Movimientos
            modelBuilder.Entity<Movimiento>().HasData(
                new Movimiento { Id = 1, ColmenaId = 1, ApiarioOrigenId = 1, ApiarioDestinoId = 2, Razon = "Polinización Alfalfa", FechaSalida = DateTime.Now.AddDays(-5), FechaRegreso = DateTime.Now.AddDays(15), Estado = "Vigente" },
                new Movimiento { Id = 2, ColmenaId = 2, ApiarioOrigenId = 1, ApiarioDestinoId = 3, Razon = "Refugio Invernal", FechaSalida = DateTime.Now.AddDays(-20), FechaRegreso = DateTime.Now.AddDays(2), Estado = "Vigente" },
                new Movimiento { Id = 3, ColmenaId = 3, ApiarioOrigenId = 1, ApiarioDestinoId = 2, Razon = "Prueba de campo", FechaSalida = DateTime.Now.AddDays(-10), FechaRegreso = DateTime.Now.AddDays(-2), Estado = "Vigente" }, // Pendiente de retorno
                new Movimiento { Id = 4, ColmenaId = 5, ApiarioOrigenId = 2, ApiarioDestinoId = 1, Razon = "Floración temprana", FechaSalida = DateTime.Now.AddDays(-40), FechaRegreso = DateTime.Now.AddDays(-10), Estado = "Completado" },
                new Movimiento { Id = 5, ColmenaId = 6, ApiarioOrigenId = 2, ApiarioDestinoId = 3, Razon = "Error de registro", FechaSalida = DateTime.Now.AddDays(-5), FechaRegreso = DateTime.Now.AddDays(5), Estado = "Cancelado" }
            );

            // Relaciones de Inversion y Ganancia con Analisis
            modelBuilder.Entity<Inversion>()
                .HasOne(i => i.Analisis)
                .WithMany(a => a.Inversiones)
                .HasForeignKey(i => i.AnalisisId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ganancia>()
                .HasOne(g => g.Analisis)
                .WithMany(a => a.Ganancias)
                .HasForeignKey(g => g.AnalisisId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed inicial de Analisis Financieros
            modelBuilder.Entity<Analisis>().HasData(
                new Analisis { Id = 1, FechaInicio = new DateTime(2025, 8, 1), FechaFin = new DateTime(2025, 11, 30) },
                new Analisis { Id = 2, FechaInicio = new DateTime(2026, 5, 29), FechaFin = null }
            );

            // Seed inicial de Inversiones
            modelBuilder.Entity<Inversion>().HasData(
                new Inversion { Id = 1, AnalisisId = 1, Titulo = "Combustible por viaje", Nota = "Logística", Precio = 2400.0 },
                new Inversion { Id = 2, AnalisisId = 1, Titulo = "Equipamiento nuevo", Nota = "Ahumadores, trajes", Precio = 3150.0 },
                new Inversion { Id = 3, AnalisisId = 1, Titulo = "Tratamientos Varroa", Nota = "Suministros Médicos", Precio = 4200.0 },
                new Inversion { Id = 4, AnalisisId = 1, Titulo = "Mantenimiento de Cajas", Nota = "Materiales", Precio = 4500.0 },
                new Inversion { Id = 5, AnalisisId = 2, Titulo = "Compra de cera estampada", Nota = "Insumo inicial", Precio = 1200.0 }
            );

            // Seed inicial de Ganancias
            modelBuilder.Entity<Ganancia>().HasData(
                new Ganancia { Id = 1, AnalisisId = 1, Titulo = "Venta de Miel (850 kg)", Descripcion = "Precio: $35/kg", Monto = 29750.0 },
                new Ganancia { Id = 2, AnalisisId = 1, Titulo = "Venta de Núcleos (20 u.)", Descripcion = "Precio: $350/u.", Monto = 7000.0 },
                new Ganancia { Id = 3, AnalisisId = 1, Titulo = "Venta de Polen (50 kg)", Descripcion = "Precio: $35/kg", Monto = 1750.0 },
                new Ganancia { Id = 4, AnalisisId = 2, Titulo = "Venta anticipada de propóleo", Descripcion = "Reserva de lote", Monto = 3500.0 }
            );

            // Seed inicial de Declaraciones Juradas
            modelBuilder.Entity<DeclaracionJurada>().HasData(
                new DeclaracionJurada { Id = 1, FechaEntrega = new DateTime(2025, 7, 21) }
            );
        }
    }
}
