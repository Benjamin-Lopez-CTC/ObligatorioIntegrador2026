# Prompt 6 (Felipe)

	Antes de crear nada, no olvides las reglas establecidas en la carpeta .agents\rules, que tienes que seguir en todo momento.

Necesito que implementes la vista de Inventario de Equipamiento, respetando el sistema de diseño unificado que ya fue establecido (esquinas semi-rectas, paleta oscura con amarillo, tipografía y componentes consistentes con el resto del sistema).

Listado de Equipamientos:

Cada equipamiento debe mostrar: Descripción/Nombre, Tipo, Stock, Estado (Alto / Medio / Bajo) y Categoría (Material, Herramienta, Medicamento u Otro).

Filtros y Búsqueda:

La vista debe incluir un campo de búsqueda por Descripción/Nombre y filtros por: Tipo, Stock, Estado y Categoría.

Registrar Nuevo Equipamiento:

El botón "Registrar Nuevo Equipo" debe abrir un popup/modal con un formulario que permita ingresar: Descripción/Nombre, Tipo, Stock, Estado (Alto / Medio / Bajo), Categoría (Material, Herramienta, Medicamento u Otro) y dos umbrales numéricos por ítem: uno para Stock Medio y otro para Stock Bajo. El sistema debe calcular y actualizar el Estado automáticamente cada vez que el stock se modifique, comparándolo contra esos umbrales. Cada ítem puede tener umbrales distintos según su naturaleza.

Validaciones del formulario:

Ningún campo obligatorio puede quedar vacío. El stock no puede ser negativo. Los umbrales deben ser coherentes entre sí, el umbral medio siempre debe ser mayor al umbral bajo. Al editar un ítem, el formulario debe venir precargado con todos sus datos actuales, incluyendo los umbrales.

Edición:

El usuario puede editar cualquier dato de un equipamiento en cualquier momento, incluyendo sus umbrales. Antes de confirmar el cambio, se le debe advertir si está seguro de realizarlo. El mensaje de confirmación debe indicar el nombre del ítem que está siendo modificado, no un mensaje genérico.

Eliminación:

El usuario puede eliminar cualquier equipamiento. Debe recibir dos alertas de confirmación antes de que la eliminación se ejecute. Ambos mensajes deben indicar el nombre del ítem que se está por eliminar.

Vista de Detalle:

Al hacer clic en cualquier ítem del listado, se debe abrir un panel lateral o modal de solo lectura que muestre todos los datos del equipamiento, incluyendo sus umbrales configurados. Desde este panel el usuario puede acceder a las acciones de editar o eliminar ese ítem.

Comportamiento general:

Tras cualquier acción exitosa (registrar, editar, eliminar) el listado debe actualizarse sin necesidad de recargar la página.

Si necesitas ajustes de estilo para mantener coherencia, consulta StitchUI\DESIGN.md.
