# Prompt 17 (Felipe)

Navbar:

Cuando el usuario se encuentre en la página Comparar Apiarios, marcar en amarillo la opción "Apiarios" de la navbar como ítem activo. Actualmente se resalta "Más", lo cual es incorrecto.

Selector de apiarios:

El usuario puede elegir dos apiarios para comparar mediante dos selects que listan todos los apiarios disponibles. Entre ambos selects debe aparecer un botón de intercambio que al presionarse intercambie los apiarios seleccionados entre sí. Si un apiario ya está seleccionado en un select, debe aparecer en el otro select pero con su opción deshabilitada, sin posibilidad de seleccionarse.

Comparaciones principales:

Debajo de los selects se muestran dos métricas destacadas comparando ambos apiarios:

- Producción total de miel en KG.

- Cantidad de colmenas activas.

Comparaciones detalladas:

Debajo de las comparaciones principales se muestra una tabla con las columnas: métrica, nombre del apiario 1, nombre del apiario 2 y diferencia. Los nombres de las columnas de cada apiario deben ser dinámicos según los apiarios seleccionados. Las filas de la tabla son:

- Última inspección.

- Fecha de creación.

- Última edición de nota de estado.

- Ubicación.

- Temperatura.

- Humedad.

- Cantidad de colmenas en estado Óptimo.

- Cantidad de colmenas en estado Alerta.

- Cantidad de colmenas en estado Crítico.

La columna "Diferencia" debe calcular y mostrar la diferencia numérica donde aplique. Para campos de texto o fecha, mostrar un indicador visual que señale si los valores son iguales o distintos.
