# Nota de funcionalidad

Al final el sistema de Reconocimiento Óptico de Caracteres (OCR) funciona a través de Gemini, gracias a una clave de API generada, el sistema es capaz de mandar una foto a los servidores de Google y analizarla con el modelo de Gemini 2.5 flash, el cual ha logrado hacer más de 15 escaneos en distintas condiciones de calidad de escritura, iluminación, y posicionamiento de cámara, sin ningún tipo de problema, también tiene límites muy altos, con una capacidad de alrededor de más de 2000 escaneos por día, con límites de 10 escaneos por minuto.

Debido a un límite físico es posible que el escáner no use la cámara de mayor calidad desde el comienzo, lo que puede llevar a una calidad disminuida y una mayor chance de tiempos de respuesta altos, como también la posibilidad de que no se reconozca la escritura. La solución a la que se llegó fue implementar un botón en el escáner que permite ir cambiando la cámara activa en el momento, por ende si el usuario siente que la calidad no es muy buena, puede apretar el botón y probar con otra cámara de su dispositivo móvil.

Si el usuario intenta escanear un código desde un dispositivo de escritorio como una PC o una laptop, el sistema en vez de activar el escáner simplemente muestra un popup con un campo para ingresar los dígitos manualmente.
