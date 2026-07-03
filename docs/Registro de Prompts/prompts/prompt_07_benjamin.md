# Prompt 7 (Benjamin)

Trabajemos sobre la sección de Apiarios

La seccion de apiarios tiene varias vistas, la primera que se ve es el listado, va a contener todos los apiarios ingresados al sistema y cada carta mostrará la informacion principal del apiario, como el nombre, el id, la ubicación, la cantidad de colmenas, la ultima vez que se ingresó a ese apiario, y una barra detallando el porcentaje de producción de miel que produjeron las colmenas de ese apiario, el limite serán 300kg, ya que ese es el tamaño de los tanques de miel que el apicultor exporta.

La segunda vista tiene los detalles de cada apiario, en esa vista se ve toda la informacion, la cual incluye:

Información general (ubicación con texto y cordenadas, la fecha de creación y las notas de estado)

La temperatura promedio (Toma la temperatura de todas las colmenas piloto de ese apiario y da una comparación)

La humedad interna (La humedad detectada en el apiario, hace lo mismo que temperatura promedio, en vez de una comparacion, establece si está estable, ver imagen de referencia)

Y la producción estimada (Misma funcion que la barra de listado de apiarios pero com mas detalles, muestra tambien una barra, la cantidad en kilos, y el porcentaje de completitud a la meta de 300kg)

Despues de los datos importantes, tendrá abajo el listado de colmenas perteneciente a ese apiario, muestra informacion basica como el id, el apiario de origen, el estado, el peso, y la temperatura interna, junto con acciones rapidas como ver los detalles de esa colmena, el listado no tendrá scroll, será por páginas de a 10 colmenas por página, con botones para navegar entre páginas.

La ultima parte será el bot´ón de eliminar el apiario, este abajo tendrá un texto que dirá "Eliminar el apiario también eliminará todas las colmenas asignadas"

Al ser algo muy grande para eliminar, tendrá una doble confirmación con popups.

Asegurate de mantener la coherencia visual y adaptar los elementos de la carpeta de StitchUI al estilo unificado establecido anteriormente.
