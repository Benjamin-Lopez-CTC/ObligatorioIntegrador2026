# Prompt 11 (Benjamin)

	Todo bien, pasemos a detalles de colmena.

Todo deberá ser similar a como se ve en la imagen, obviamente substituyendo los datos viejos por los nuevos, como el identificador de la colmena, los supuestos sensores de humedad y temperatura no existirán por ende la unica forma de cambiar la temperatura y humedad será a través de la edición de colmena, si la colmena no es piloto, no estarán tanto estos datos como las casillas al momento de editar la colmena, en ese caso, reemplaza las cartas faltantes con datos importantes que puedan ocupar el lugar vacío.

Eso me hizo acordarme que al momento de crear una colmena, hay que añadir un checkbox que diga si la colmena es piloto o no, que afectara los datos antes mencionados.

La cuarta carta en vez de decir estado deberá decir reina, y en vez de confirmada, presente o ausente, diciendo cuando fue la ultima vez que se vio, tomando la fecha de la ultima nota técnica

La información principal es toda como se muestra ahí, menos la de comportamiento, quita lo que sigue de la barra diagonal, la valoración de producción se adaptará a los precios de la miel para exportadores, con una vista de precio por kilo y la valoración total de cuanto vale la totalidad de la miel de esa colmena.

El historial de inspecciones será implementado posteriormente, asi que por ahora tendrá cartas inertes.

Las notas técnicas permitiran saber el estado general de la colmena, al darle a añadir nota técnica, habrá un popup que pedirá los detalles en texto , un dropdown para establecer el estado de la reina y un dropdown que tendrá el estado de la colmena, será Óptimo, Alerta y Critico, si bien se establecen por aquí, en colmenas piloto si la temperatura o humedad llegan a niveles peligrosos, la reina desaparece o no se realizó en cierto tiempo una inspección (Esas ultimas dos para todas las colmenas, no solo piloto), entonces el estado automaticamente cambiará a alerta.
