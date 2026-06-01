const fs = require('fs');

function generateEquipments() {
    let code = "            // Seed inicial de equipamiento\n            modelBuilder.Entity<Equipment>().HasData(\n";
    let items = [];
    
    items.push('new Equipment { Id = 1, Name = "Ahumador Inoxidable", Type = "Ahumador estándar 10x25cm", Stock = 12, Category = "Herramienta", LowThreshold = 5, MediumThreshold = 15, DisplayOrder = 1, UnitPrice = 45.0, Currency = "USD" }');
    items.push('new Equipment { Id = 2, Name = "Palanca de Manejo", Type = "Pinza y palanca universal", Stock = 24, Category = "Herramienta", LowThreshold = 10, MediumThreshold = 20, DisplayOrder = 2, UnitPrice = 12.0, Currency = "USD" }');
    items.push('new Equipment { Id = 3, Name = "Ácido Oxálico (Glicerina)", Type = "Tratamiento Varroa", Stock = 50, Category = "Medicamento", LowThreshold = 15, MediumThreshold = 40, DisplayOrder = 3, UnitPrice = 25.0, Currency = "USD" }');
    items.push('new Equipment { Id = 4, Name = "Amitraz (Tiras)", Type = "Tratamiento Varroa", Stock = 15, Category = "Medicamento", LowThreshold = 10, MediumThreshold = 25, DisplayOrder = 4, UnitPrice = 30.0, Currency = "USD" }');
    items.push('new Equipment { Id = 5, Name = "Alzas Melarias (Media)", Type = "Madera", Stock = 120, Category = "Material", LowThreshold = 30, MediumThreshold = 80, DisplayOrder = 5, UnitPrice = 250.0, Currency = "UYU" }');
    items.push('new Equipment { Id = 6, Name = "Marcos Alambrados", Type = "Madera/Alambre", Stock = 450, Category = "Material", LowThreshold = 100, MediumThreshold = 200, DisplayOrder = 6, UnitPrice = 80.0, Currency = "UYU" }');
    items.push('new Equipment { Id = 7, Name = "Cera Estampada", Type = "Cera Orgánica", Stock = 5, Category = "Material", LowThreshold = 10, MediumThreshold = 25, DisplayOrder = 7, UnitPrice = 120.0, Currency = "UYU" }');

    const categories = ["Herramienta", "Medicamento", "Material"];
    const currencies = ["USD", "UYU"];
    const names = {
        "Herramienta": ["Ahumador", "Palanca", "Cepillo", "Pinza", "Traje", "Guantes", "Careta", "Cuchillo", "Extractor"],
        "Medicamento": ["Ácido Oxálico", "Amitraz", "Fluvalinato", "Timol", "Suplemento Vitamínico"],
        "Material": ["Alza", "Marco", "Cera", "Techo", "Piso", "Rejilla Excluidora", "Alimentador"]
    };

    for (let i = 8; i <= 25; i++) {
        let cat = categories[Math.floor(Math.random() * categories.length)];
        let name = names[cat][Math.floor(Math.random() * names[cat].length)] + " " + Math.floor(Math.random() * 100 + 1);
        let typeStr = "Tipo " + Math.floor(Math.random() * 10 + 1);
        let stock = Math.floor(Math.random() * 500);
        let low = Math.floor(Math.random() * 45 + 5);
        let med = low + Math.floor(Math.random() * 90 + 10);
        let price = (Math.random() * 500 + 5).toFixed(2).replace('.', ',');
        let curr = currencies[Math.floor(Math.random() * currencies.length)];
        
        // In C# doubles need '.' not ',' if cultural settings are invariant, but we can just use 123.45 format
        items.push(`new Equipment { Id = ${i}, Name = "${name}", Type = "${typeStr}", Stock = ${stock}, Category = "${cat}", LowThreshold = ${low}, MediumThreshold = ${med}, DisplayOrder = ${i}, UnitPrice = ${price.replace(',', '.')}, Currency = "${curr}" }`);
    }
    
    code += "                " + items.join(",\n                ") + "\n            );\n";
    return code;
}

function generateColmenas() {
    let code = "            // Seed inicial de Colmenas\n            modelBuilder.Entity<Colmena>().HasData(\n";
    let items = [];
    
    items.push('new Colmena { Id = 1, Identificador = "#HIVE-0042", CodigoEscaneo = "100001", ApiarioId = 1, Estado = "Óptimo", PesoKg = 45.2, TemperaturaInterna = 34.5, HumedadInterna = 55.0, ProduccionMielKg = 40.0, EsPiloto = true, EsNucleo = false, CantidadAbejas = 45000, UbicacionIntraApiario = "Fila 1, Pos 1", ComportamientoAbejas = "Dócil", EstadoReina = "Presente" }');
    items.push('new Colmena { Id = 2, Identificador = "#HIVE-0089", CodigoEscaneo = "100002", ApiarioId = 1, Estado = "Alerta", PesoKg = 42.8, TemperaturaInterna = 32.0, HumedadInterna = 0, ProduccionMielKg = 35.0, EsPiloto = false, EsNucleo = true, CantidadAbejas = 38000, UbicacionIntraApiario = "Fila 1, Pos 2", ComportamientoAbejas = "Defensivo", EstadoReina = "Presente" }');
    items.push('new Colmena { Id = 3, Identificador = "#HIVE-0112", CodigoEscaneo = "100003", ApiarioId = 2, Estado = "Crítico", PesoKg = 31.0, TemperaturaInterna = 36.5, HumedadInterna = 60.0, ProduccionMielKg = 20.0, EsPiloto = true, EsNucleo = false, CantidadAbejas = 15000, UbicacionIntraApiario = "Fila 2, Pos 1", ComportamientoAbejas = "Agresivo", EstadoReina = "Ausente" }');
    items.push('new Colmena { Id = 4, Identificador = "#HIVE-0045", CodigoEscaneo = "100004", ApiarioId = 2, Estado = "Óptimo", PesoKg = 48.1, TemperaturaInterna = 34.2, HumedadInterna = 58.0, ProduccionMielKg = 45.0, EsPiloto = true, EsNucleo = true, CantidadAbejas = 50000, UbicacionIntraApiario = "Fila 2, Pos 2", ComportamientoAbejas = "Dócil", EstadoReina = "Presente" }');
    items.push('new Colmena { Id = 5, Identificador = "#HIVE-0001", CodigoEscaneo = "100005", ApiarioId = 3, Estado = "Óptimo", PesoKg = 40.0, TemperaturaInterna = 35.1, HumedadInterna = 52.0, ProduccionMielKg = 30.0, EsPiloto = true, EsNucleo = false, CantidadAbejas = 42000, UbicacionIntraApiario = "Fila 1, Pos 1", ComportamientoAbejas = "Dócil", EstadoReina = "Presente" }');
    items.push('new Colmena { Id = 6, Identificador = "#HIVE-0002", CodigoEscaneo = "100006", ApiarioId = 4, Estado = "Óptimo", PesoKg = 39.5, TemperaturaInterna = 34.8, HumedadInterna = 0, ProduccionMielKg = 30.0, EsPiloto = false, EsNucleo = false, CantidadAbejas = 41000, UbicacionIntraApiario = "Fila 1, Pos 2", ComportamientoAbejas = "Dócil", EstadoReina = "Presente" }');
    items.push('new Colmena { Id = 7, Identificador = "#HIVE-0003", CodigoEscaneo = "100007", ApiarioId = 5, Estado = "Crítico", PesoKg = 25.0, TemperaturaInterna = 30.0, HumedadInterna = 82.0, ProduccionMielKg = 10.0, EsPiloto = true, EsNucleo = true, CantidadAbejas = 12000, UbicacionIntraApiario = "Única", ComportamientoAbejas = "Agresivo", EstadoReina = "Presente" }');

    const estados = ["Óptimo", "Alerta", "Crítico"];
    const comportamientos = ["Dócil", "Defensivo", "Agresivo"];
    const estadoReina = ["Presente", "Ausente", "Cambiando"];

    for (let i = 8; i <= 50; i++) {
        let ident = `#HIVE-0${i.toString().padStart(3, '0')}`;
        let codigo = `1000${i.toString().padStart(2, '0')}`;
        let apiarioId = Math.floor(Math.random() * 5 + 1);
        let est = estados[Math.floor(Math.random() * estados.length)];
        let peso = (Math.random() * 40 + 20).toFixed(1);
        let temp = (Math.random() * 8 + 30).toFixed(1);
        let hum = (Math.random() * 30 + 40).toFixed(1);
        let prod = (Math.random() * 40 + 10).toFixed(1);
        let es_piloto = Math.random() > 0.7 ? "true" : "false";
        let es_nucleo = Math.random() > 0.8 ? "true" : "false";
        let abejas = Math.floor(Math.random() * 50000 + 10000);
        let ubi = `Fila ${Math.floor(Math.random() * 5 + 1)}, Pos ${Math.floor(Math.random() * 10 + 1)}`;
        let comp = comportamientos[Math.floor(Math.random() * comportamientos.length)];
        let reina = estadoReina[Math.floor(Math.random() * estadoReina.length)];

        items.push(`new Colmena { Id = ${i}, Identificador = "${ident}", CodigoEscaneo = "${codigo}", ApiarioId = ${apiarioId}, Estado = "${est}", PesoKg = ${peso}, TemperaturaInterna = ${temp}, HumedadInterna = ${hum}, ProduccionMielKg = ${prod}, EsPiloto = ${es_piloto}, EsNucleo = ${es_nucleo}, CantidadAbejas = ${abejas}, UbicacionIntraApiario = "${ubi}", ComportamientoAbejas = "${comp}", EstadoReina = "${reina}" }`);
    }

    code += "                " + items.join(",\n                ") + "\n            );\n";
    return code;
}

function generateNotas() {
    let code = "            // Seed inicial de Notas Tecnicas\n            modelBuilder.Entity<NotaTecnica>().HasData(\n";
    let items = [];
    items.push('new NotaTecnica { Id = 1, ColmenaId = 1, Detalles = "Revisión general, todo normal.", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-10) }');
    items.push('new NotaTecnica { Id = 2, ColmenaId = 2, Detalles = "Abejas defensivas, observar.", EstadoReina = "Presente", EstadoColmena = "Alerta", Fecha = DateTime.Now.AddDays(-15) }');
    items.push('new NotaTecnica { Id = 3, ColmenaId = 3, Detalles = "Reina no avistada. Posible enjambrazón.", EstadoReina = "Ausente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-2) }');
    items.push('new NotaTecnica { Id = 4, ColmenaId = 4, Detalles = "Excelente producción.", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-5) }');
    items.push('new NotaTecnica { Id = 5, ColmenaId = 5, Detalles = "Alza agregada.", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-7) }');
    items.push('new NotaTecnica { Id = 6, ColmenaId = 6, Detalles = "Normal.", EstadoReina = "Presente", EstadoColmena = "Óptimo", Fecha = DateTime.Now.AddDays(-20) }');
    items.push('new NotaTecnica { Id = 7, ColmenaId = 7, Detalles = "Humedad alta.", EstadoReina = "Presente", EstadoColmena = "Crítico", Fecha = DateTime.Now.AddDays(-35) }');

    const estados = ["Óptimo", "Alerta", "Crítico"];
    const estadoReina = ["Presente", "Ausente", "Cambiando"];
    const detallesOpts = ["Revisión de rutina", "Se alimentó", "Se limpió piso", "Alta presencia de zánganos", "Reina joven", "Sin novedades", "Cosecha parcial", "Se agregó cera"];

    for (let i = 8; i <= 80; i++) {
        let col_id = Math.floor(Math.random() * 50 + 1);
        let reina = estadoReina[Math.floor(Math.random() * estadoReina.length)];
        let est = estados[Math.floor(Math.random() * estados.length)];
        let det = detallesOpts[Math.floor(Math.random() * detallesOpts.length)];
        let dias = Math.floor(Math.random() * 100 + 1);
        items.push(`new NotaTecnica { Id = ${i}, ColmenaId = ${col_id}, Detalles = "${det}", EstadoReina = "${reina}", EstadoColmena = "${est}", Fecha = DateTime.Now.AddDays(-${dias}) }`);
    }

    code += "                " + items.join(",\n                ") + "\n            );\n";
    return code;
}

function generateTreatments() {
    let code = "            // Seed inicial de Treatments y TreatmentEquipments\n            modelBuilder.Entity<Treatment>().HasData(\n";
    let items = [];
    items.push('new Treatment { Id = 1, ColmenaId = 1, Titulo = "Aplicación Ácido Oxálico", Tipo = "Medicinal", Nota = "Tratamiento por goteo. Dosis estándar 50ml por colmena. Temperatura ambiente 18°C.", Fecha = new DateTime(2025, 10, 12, 14, 30, 0) }');
    items.push('new Treatment { Id = 2, ColmenaId = 1, Titulo = "Alimentación de Soporte", Tipo = "Mantenimiento", Nota = "Jarabe de azúcar 2:1. 2 Litros suministrados en alimentador de techo.", Fecha = new DateTime(2025, 08, 28, 9, 15, 0) }');

    const tipos = ["Medicinal", "Mantenimiento", "Preventivo"];
    const titulos = ["Aplicación Amitraz", "Alimentación Proteica", "Revisión Sanitaria", "Goteo Ácido", "Vitaminas"];

    for (let i = 3; i <= 40; i++) {
        let col_id = Math.floor(Math.random() * 50 + 1);
        let tipo = tipos[Math.floor(Math.random() * tipos.length)];
        let tit = titulos[Math.floor(Math.random() * titulos.length)];
        let dias = Math.floor(Math.random() * 195 + 5);
        items.push(`new Treatment { Id = ${i}, ColmenaId = ${col_id}, Titulo = "${tit}", Tipo = "${tipo}", Nota = "Tratamiento aplicado correctamente", Fecha = DateTime.Now.AddDays(-${dias}) }`);
    }

    code += "                " + items.join(",\n                ") + "\n            );\n\n";

    code += "            modelBuilder.Entity<TreatmentEquipment>().HasData(\n";
    let itemsEq = [];
    itemsEq.push('new TreatmentEquipment { Id = 1, TreatmentId = 1, EquipmentName = "Ácido Oxálico (Glicerina)", Cantidad = 1 }');

    for (let i = 2; i <= 40; i++) {
        itemsEq.push(`new TreatmentEquipment { Id = ${i}, TreatmentId = ${i}, EquipmentName = "Tratamiento Vario", Cantidad = 1 }`);
    }

    code += "                " + itemsEq.join(",\n                ") + "\n            );\n";
    return code;
}

function generateMovimientos() {
    let code = "            // Seed inicial de Movimientos\n            modelBuilder.Entity<Movimiento>().HasData(\n";
    let items = [];
    items.push('new Movimiento { Id = 1, ColmenaId = 1, ApiarioOrigenId = 1, ApiarioDestinoId = 2, Razon = "Polinización Alfalfa", FechaSalida = DateTime.Now.AddDays(-5), FechaRegreso = DateTime.Now.AddDays(15), Estado = "Vigente" }');
    items.push('new Movimiento { Id = 2, ColmenaId = 2, ApiarioOrigenId = 1, ApiarioDestinoId = 3, Razon = "Refugio Invernal", FechaSalida = DateTime.Now.AddDays(-20), FechaRegreso = DateTime.Now.AddDays(2), Estado = "Vigente" }');
    items.push('new Movimiento { Id = 3, ColmenaId = 3, ApiarioOrigenId = 1, ApiarioDestinoId = 2, Razon = "Prueba de campo", FechaSalida = DateTime.Now.AddDays(-10), FechaRegreso = DateTime.Now.AddDays(-2), Estado = "Vigente" }');
    items.push('new Movimiento { Id = 4, ColmenaId = 5, ApiarioOrigenId = 2, ApiarioDestinoId = 1, Razon = "Floración temprana", FechaSalida = DateTime.Now.AddDays(-40), FechaRegreso = DateTime.Now.AddDays(-10), Estado = "Completado" }');
    items.push('new Movimiento { Id = 5, ColmenaId = 6, ApiarioOrigenId = 2, ApiarioDestinoId = 3, Razon = "Error de registro", FechaSalida = DateTime.Now.AddDays(-5), FechaRegreso = DateTime.Now.AddDays(5), Estado = "Cancelado" }');

    const razones = ["Floración Eucalyptus", "Floración Pradera", "Refugio Invernal", "Venta/Préstamo", "Cuarentena"];
    const estados = ["Vigente", "Completado", "Cancelado"];

    for (let i = 6; i <= 30; i++) {
        let col = Math.floor(Math.random() * 50 + 1);
        let orig = Math.floor(Math.random() * 5 + 1);
        let dest = Math.floor(Math.random() * 5 + 1);
        while (orig === dest) {
            dest = Math.floor(Math.random() * 5 + 1);
        }
        let raz = razones[Math.floor(Math.random() * razones.length)];
        let est = estados[Math.floor(Math.random() * estados.length)];
        let salida = Math.floor(Math.random() * 90 + 10);
        let regreso = salida - Math.floor(Math.random() * 40 - 20);
        items.push(`new Movimiento { Id = ${i}, ColmenaId = ${col}, ApiarioOrigenId = ${orig}, ApiarioDestinoId = ${dest}, Razon = "${raz}", FechaSalida = DateTime.Now.AddDays(-${salida}), FechaRegreso = DateTime.Now.AddDays(-${regreso}), Estado = "${est}" }`);
    }

    code += "                " + items.join(",\n                ") + "\n            );\n";
    return code;
}

function generateAnalisis() {
    let code = "            // Seed inicial de Analisis Financieros\n            modelBuilder.Entity<Analisis>().HasData(\n";
    let items = [];
    items.push('new Analisis { Id = 1, FechaInicio = new DateTime(2025, 8, 1), FechaFin = new DateTime(2025, 11, 30) }');
    items.push('new Analisis { Id = 2, FechaInicio = new DateTime(2026, 5, 29), FechaFin = null }');
    items.push('new Analisis { Id = 3, FechaInicio = new DateTime(2024, 8, 1), FechaFin = new DateTime(2025, 3, 30) }');
    items.push('new Analisis { Id = 4, FechaInicio = new DateTime(2023, 9, 1), FechaFin = new DateTime(2024, 4, 15) }');
    code += "                " + items.join(",\n                ") + "\n            );\n\n";

    code += "            // Seed inicial de Inversiones\n            modelBuilder.Entity<Inversion>().HasData(\n";
    let itemsInv = [];
    itemsInv.push('new Inversion { Id = 1, AnalisisId = 1, Titulo = "Combustible por viaje", Nota = "Logística", Precio = 2400.0 }');
    itemsInv.push('new Inversion { Id = 2, AnalisisId = 1, Titulo = "Equipamiento nuevo", Nota = "Ahumadores, trajes", Precio = 3150.0 }');
    itemsInv.push('new Inversion { Id = 3, AnalisisId = 1, Titulo = "Tratamientos Varroa", Nota = "Suministros Médicos", Precio = 4200.0 }');
    itemsInv.push('new Inversion { Id = 4, AnalisisId = 1, Titulo = "Mantenimiento de Cajas", Nota = "Materiales", Precio = 4500.0 }');
    itemsInv.push('new Inversion { Id = 5, AnalisisId = 2, Titulo = "Compra de cera estampada", Nota = "Insumo inicial", Precio = 1200.0 }');

    const titulosInv = ["Combustible", "Reparación Camioneta", "Compra Reinas", "Suministros", "Herramientas", "Alimentación"];
    for (let i = 6; i <= 40; i++) {
        let an_id = Math.floor(Math.random() * 4 + 1);
        let tit = titulosInv[Math.floor(Math.random() * titulosInv.length)];
        let prec = (Math.random() * 9500 + 500).toFixed(2);
        itemsInv.push(`new Inversion { Id = ${i}, AnalisisId = ${an_id}, Titulo = "${tit}", Nota = "Gasto operativo", Precio = ${prec} }`);
    }
    code += "                " + itemsInv.join(",\n                ") + "\n            );\n\n";

    code += "            // Seed inicial de Ganancias\n            modelBuilder.Entity<Ganancia>().HasData(\n";
    let itemsGan = [];
    itemsGan.push('new Ganancia { Id = 1, AnalisisId = 1, Titulo = "Venta de Miel (850 kg)", Descripcion = "Precio: $35/kg", Monto = 29750.0 }');
    itemsGan.push('new Ganancia { Id = 2, AnalisisId = 1, Titulo = "Venta de Núcleos (20 u.)", Descripcion = "Precio: $350/u.", Monto = 7000.0 }');
    itemsGan.push('new Ganancia { Id = 3, AnalisisId = 1, Titulo = "Venta de Polen (50 kg)", Descripcion = "Precio: $35/kg", Monto = 1750.0 }');
    itemsGan.push('new Ganancia { Id = 4, AnalisisId = 2, Titulo = "Venta anticipada de propóleo", Descripcion = "Reserva de lote", Monto = 3500.0 }');

    const titulosGan = ["Venta Tambor Miel", "Venta Núcleos", "Polinización", "Cera", "Propóleo"];
    for (let i = 5; i <= 30; i++) {
        let an_id = Math.floor(Math.random() * 4 + 1);
        let tit = titulosGan[Math.floor(Math.random() * titulosGan.length)];
        let mont = (Math.random() * 28000 + 2000).toFixed(2);
        itemsGan.push(`new Ganancia { Id = ${i}, AnalisisId = ${an_id}, Titulo = "${tit}", Descripcion = "Ingreso operativo", Monto = ${mont} }`);
    }
    code += "                " + itemsGan.join(",\n                ") + "\n            );\n";

    return code;
}

const fullSeed = generateEquipments() + "\n" + 
                 "            // Relaciones de Movimiento para evitar ciclos de cascada\n" +
                 "            modelBuilder.Entity<Movimiento>().HasOne(m => m.ApiarioOrigen).WithMany().HasForeignKey(m => m.ApiarioOrigenId).OnDelete(DeleteBehavior.Restrict);\n" +
                 "            modelBuilder.Entity<Movimiento>().HasOne(m => m.ApiarioDestino).WithMany().HasForeignKey(m => m.ApiarioDestinoId).OnDelete(DeleteBehavior.Restrict);\n\n" +
                 "            // Relaciones de Inversion y Ganancia con Analisis\n" +
                 "            modelBuilder.Entity<Inversion>().HasOne(i => i.Analisis).WithMany(a => a.Inversiones).HasForeignKey(i => i.AnalisisId).OnDelete(DeleteBehavior.Cascade);\n" +
                 "            modelBuilder.Entity<Ganancia>().HasOne(g => g.Analisis).WithMany(a => a.Ganancias).HasForeignKey(g => g.AnalisisId).OnDelete(DeleteBehavior.Cascade);\n\n" +
                 generateColmenas() + "\n" + 
                 generateNotas() + "\n" + 
                 generateTreatments() + "\n" + 
                 generateMovimientos() + "\n" + 
                 generateAnalisis();

const file = 'Data/AppDbContext.cs';
const content = fs.readFileSync(file, 'utf8');

const lines = content.split('\n');
const start = lines.findIndex(l => l.includes('// Seed inicial de equipamiento'));
const end = lines.findIndex(l => l.includes('// Seed inicial de Declaraciones Juradas'));

if (start !== -1 && end !== -1) {
    lines.splice(start, end - start, fullSeed);
    fs.writeFileSync(file, lines.join('\n'));
    console.log("Success!");
} else {
    console.log("Error finding bounds", start, end);
}
