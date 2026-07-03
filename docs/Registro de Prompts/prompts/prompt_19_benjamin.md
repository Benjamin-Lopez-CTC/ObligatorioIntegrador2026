# Prompt 19 (B)

**Implementación de Escaneo de Imágenes**

En la página de listado de colmenas, hay un botón desactivado que dice "Escanear codigo ID", el propósito de ese boton es que en la vista de celular que abra la camara y permita escanear un codigo de 6 digitos, el cual es el que te pedí que le dieras a cada colmena de forma única, ya que el codigo será escrito a mano, debe haber alguna forma de que la imagen se pueda analizar y reconozca la escritura humana, del lado fisico se intentará escribir los numeros grandes y rectos con ayuda de casillas en papel blanco con marcador negro, pero sigue estando la posibilidad de error humano, había considerado utilizar un API de google que ofrece "Document Text Detection" y ofrece 1000 escaneos gratis al mes, sería Google Cloud Vision, esa fue una de las opciones, pero tambien me gustaría que me dieras tu opinion sobre que podría ser mejor, cuando el codigo es escaneado, el sistema debe mandar al usuario a la pagina de detalles de la colmena con el codigo correspondiente.

Si el botón se apreta desde un navegador en escritorio, en vez de mostrar la vista de la camara simplemente debe permitir digitar el codigo con el teclado, sirviendo una similar funcion a la busqueda ID que hay en los filtros del listado de colmenas.

Con dicha implementación tambien me gustaría pedirte que elabores una vista de celular para el sistema que sea cómoda para la gran mayoría de dispositivos, ya que la vista actual es apretada y varias cosas se salen de la pantalla o no funcionan, pero lo principal es el escaneo, igual dale prioridad a hacer una vista de celular al escaneo mismo.
