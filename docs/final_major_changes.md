# Resumen de Arquitectura y Optimización Offline

## [PRIORIDAD 1] Preparación Crítica para Despliegue en Render (Migración a PostgreSQL)
El usuario ha solicitado preparar la arquitectura para ser desplegada en **Render.com** utilizando **PostgreSQL** en un futuro cercano. Esta tarea se considera de **máxima prioridad** y debe realizarse **antes** de comenzar la refactorización offline.

Los pasos necesarios a ejecutar por la nueva instancia son:

1.  **Cambio de Proveedor EF Core:**
    - Desinstalar el paquete actual de SQLite (`Microsoft.EntityFrameworkCore.Sqlite`).
    - Instalar el proveedor oficial de Postgres: `dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL`.

2.  **Refactorización de `Program.cs` (RIESGO CRÍTICO DE PÉRDIDA DE DATOS):**
    - Modificar la inyección del DbContext de `UseSqlite()` a `UseNpgsql()`.
    - **URGENTE:** Aislar el código actual que ejecuta `context.Database.EnsureDeleted()` y `context.Database.EnsureCreated()`. Este código está diseñado para desarrollo local rápido y **borrará toda la base de datos de producción cada vez que la aplicación se reinicie en Render**.
    - Debe moverse esa lógica estrictamente dentro de un condicional `if (app.Environment.IsDevelopment())`.

3.  **Implementación de Migraciones Reales:**
    - Generar la primera migración de EF Core para crear el esquema inicial de Postgres (`dotnet ef migrations add InitialCreate`).
    - Configurar la aplicación para que, en producción, aplique estas migraciones de manera segura (ej. usando `context.Database.Migrate()`).

4.  **Dockerización (Recomendado):**
    - Crear un archivo `Dockerfile` en la raíz del proyecto para facilitar, estabilizar y acelerar el proceso de construcción y despliegue que Render utiliza para aplicaciones .NET 9.

---

## [PRIORIDAD 2] Propuesta de Optimización: Arquitectura de "JSON Consolidado" (API-First)

### Contexto Actual
- **Proyecto:** Sistema de gestión apícola construido con **ASP.NET Core MVC**.
- **Problema de Rendimiento Offline:** El Service Worker actual descarga el HTML completo de cada vista para el funcionamiento offline. En un escenario realista con 300 colmenas y 10 apiarios, esto se traduce en casi 1000 peticiones HTTP individuales (detalles de colmenas, apiarios, tratamientos, extracciones, etc.). Esto causa un cuello de botella con una latencia de red de aproximadamente 20-30 segundos.
- **Últimos Cambios Implementados:** 
  - Se actualizó el endpoint `GetAllOfflineUrls` en `HomeController.cs` para indexar todas las URLs relevantes del sistema para disponibilidad offline.
  - Se centralizó la lógica de precarga en `wwwroot/js/offline-sync.js` usando una llamada dinámica y se limpió el código redundante en `site.js`.

### El Nuevo Modelo Offline
Para reducir el tiempo de carga offline de 20-30 segundos a **menos de 2 segundos**, se debatió implementar la siguiente arquitectura:

1.  **Transición de MVC a API:** En lugar de devolver las vistas HTML procesadas en el servidor (`return View()`), los controladores (ej. `ColmenasController`, `ExtraccionesController`) deben exponer endpoints que devuelvan la información puramente en formato **JSON**.
2.  **Sincronización Consolidada:** Durante la pantalla de carga, el frontend solicitaría un "payload" masivo o consolidado en JSON con todos los datos necesarios, reduciendo de 1000 peticiones HTTP a 1 o unas pocas peticiones optimizadas.
3.  **Almacenamiento Local Estructurado:** Los datos JSON recibidos se guardarían en una base de datos local en el navegador del usuario utilizando **IndexedDB** en lugar de almacenar páginas HTML estáticas en la caché del Service Worker.
4.  **Renderizado en el Cliente (Client-Side Rendering):** El frontend (Javascript) tomaría la responsabilidad de construir el HTML dinámicamente en base a los datos extraídos de IndexedDB.
5.  **Alcance Limitado (Solo Offline):** Por petición específica del usuario, esta optimización debe aplicarse **exclusivamente al modo offline**. El sistema en modo online debe seguir funcionando con el renderizado clásico de MVC (vistas de Razor generadas en el servidor). Por lo tanto, se requerirá una arquitectura dual donde el Service Worker intercepte las peticiones cuando no haya red y sirva las vistas "híbridas" alimentadas por la API y IndexedDB.

### Próximos Pasos Técnicos para la Nueva Instancia
Si la nueva instancia de Antigravity decide tomar y ejecutar este camino de optimización, el roadmap sugerido es:

1.  **Fase 1 (Refactorización de Backend):** Crear o modificar los controladores actuales para exponer APIs RESTful que devuelvan los modelos en JSON.
2.  **Fase 2 (Capa de Datos Cliente):** Integrar **IndexedDB** (usando librerías como `idb` para promesas) para gestionar la base de datos local de manera estructurada.
3.  **Fase 3 (Adaptación de UI):** Modificar las vistas actuales para que no dependan del renderizado de Razor, sino de plantillas o renderizado dinámico mediante JavaScript alimentado por los datos de IndexedDB.
4.  **Fase 4 (Registro Offline / Sincronización Bidireccional):** Modificar los formularios (ej. registrar tratamiento, nueva colmena) para que, si no hay conexión, se guarden las transacciones en una "cola de sincronización" en IndexedDB, y usar la API de Background Sync (o un mecanismo manual de reconexión) para hacer el POST al servidor principal más tarde.
