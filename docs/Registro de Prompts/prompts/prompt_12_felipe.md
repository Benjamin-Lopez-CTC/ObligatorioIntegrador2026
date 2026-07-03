# Prompt 12 (Felipe)

	Antes de crear nada, revisá todas las reglas en la carpeta .agents\rules y respetá el sistema de diseño definido en StitchUI\DESIGN.md en todo momento.

Vamos a trabajar sobre la opcion de Gestionar Tratamientos.

Cuando el usuario hace clic en "Gestionar Tratamientos" desde la página individual de una colmena, debe navegar a la vista de Gestión de Tratamientos de esa colmena.

Tomá como referencia el código y los lineamientos visuales que se encuentran en StitchUI > Colmenas > Gestion Tratamientos.

Respetá el sistema de diseño unificado del sistema: esquinas semi-rectas, paleta oscura con amarillo, tipografía y componentes consistentes con el resto de las vistas.

En la parte superior de la vista, mostrá un panel de solo lectura con la información de estado de la colmena correspondiente. Los datos a mostrar son:

Estado de la reina

Estado general de la colmena

Cantidad de abejas

Comportamiento de las abejas

Incluí un botón "Registrar Nuevo Tratamiento" que abra un modal/popup similar en estilo y comportamiento al del listado de equipamientos. El formulario debe contener:

Tipo (desplegable con opciones: Mantenimiento, Medicinal, Otro) — obligatorio

Nota (campo de texto libre)

Fecha — se asigna automáticamente con la fecha y hora actual, no es editable por el usuario

Equipamientos utilizados — no obligatorio, pero con la siguiente lógica:

Por defecto se muestra una fila compuesta por un select/dropdown para elegir el equipamiento y un campo numérico al lado para ingresar la cantidad

Todos los equipamientos del inventario deben aparecer en el dropdown, tengan o no stock disponible, con el formato: Nombre del equipamiento (12u)

Si el equipamiento tiene stock 0, debe mostrarse igual en el listado con (0u) pero la cantidad ingresada no puede superar el stock disponible — si el stock es 0, no se puede ingresar una cantidad válida y el formulario no puede enviarse con esa fila

Debajo de la fila, un botón "+ Agregar equipamiento" que al hacer clic añade una nueva fila con el mismo formato (select + campo de cantidad)

Cada fila adicional puede eliminarse individualmente

No puede haber dos filas con el mismo equipamiento seleccionado

Validaciones del formulario:

Los campos obligatorios (Tipo) no pueden estar vacíos

Si se completa una fila de equipamiento, tanto el select como la cantidad son obligatorios en esa fila — no puede enviarse el formulario con una fila incompleta

La cantidad no puede ser negativa ni cero, y no puede superar el stock disponible del equipamiento seleccionado

Al confirmar el registro:

Se guarda el tratamiento vinculado a la colmena con todos sus datos tal como fueron ingresados — estos datos son inmutables, es decir, si en el futuro un equipamiento es eliminado del inventario, el tratamiento ya registrado no cambia en absoluto

Se descuenta del stock de cada equipamiento utilizado la cantidad indicada, manteniendo la consistencia con el inventario

El listado de tratamientos se actualiza sin recargar la página

Debajo del resumen, mostrá el historial de tratamientos registrados para esa colmena. Si la colmena no tiene tratamientos aún, mostrar un mensaje amigable del estilo "No hay tratamientos registrados para esta colmena."

Cada ítem del listado debe mostrar:

Fecha

Tipo (Mantenimiento, Medicinal u Otro)

Nota

Equipamientos utilizados (nombre y cantidad de cada uno)

Columna "Acciones" con dos opciones: Ver detalles y Eliminar

Filtros del listado:

Rango de fechas (campo desde / hasta) para filtrar tratamientos dentro de un período específico

Por tipo (Mantenimiento, Medicinal, Otro)

Por cantidad de equipamientos utilizados (sin equipamientos, con 1, con 2 o más)

Por antiguedad (tratamiento mas nuevo ó tratamiento mas antiguo)

Al hacer clic en "Ver detalles" en la columna de acciones, se abre un panel lateral o modal de solo lectura con todos los datos del tratamiento: fecha, tipo, nota y equipamientos utilizados con sus cantidades.

Al hacer clic en "Eliminar" en la columna de acciones, se muestra un alert con el estilo del sistema (consistente con los demás alerts de confirmación) que debe:

Indicar el nombre o fecha del tratamiento que se está por eliminar, no un mensaje genérico

Incluir un checkbox obligatorio que el usuario debe marcar antes de poder confirmar, con la pregunta: "¿Devolver el stock de los equipamientos utilizados?" con opciones Sí / No

El usuario no puede confirmar la eliminación sin haber marcado una de las dos opciones del checkbox

Si elige Sí, el stock de cada equipamiento utilizado en ese tratamiento se restaura con la cantidad correspondiente

Si elige No, el stock no se modifica

Una vez confirmado, el tratamiento se elimina y el listado se actualiza sin recargar la página.

Tras un registro o eliminacion, mostrar las notificaciones adecuadas, teniendo consistencia con las demas páginas del sitio ademas tras cualquier acción exitosa (registrar o eliminar un tratamiento), el listado debe actualizarse automáticamente sin recargar la página. Si necesitás ajustes de estilo para mantener coherencia visual, consultá StitchUI\DESIGN.md.
