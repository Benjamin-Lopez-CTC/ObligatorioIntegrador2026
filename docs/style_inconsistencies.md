# Reporte de Inconsistencias de Estilo Globales

Tras analizar el código fuente de las distintas vistas del proyecto, he identificado varias inconsistencias visuales y de código en los estilos aplicados (principalmente usando Tailwind CSS). A continuación detallo cada una y presento una propuesta para su corrección.

## 1. Tipografía y Estilos de Texto

Se ha detectado que el uso de las fuentes no es consistente a lo largo de los diferentes archivos y secciones de la aplicación.

*   **Variación en el cuerpo de texto:** Algunas vistas y tablas utilizan `font-body-md text-body-md` mientras que otras reducen el tamaño usando `font-body-sm text-body-sm` sin un motivo claro de jerarquía.
*   **Encabezados:** Las páginas varían entre `font-display text-display` (ej. en `Movimientos` e `Inventario`) y `font-headline-lg-mobile text-headline-lg-mobile` sin una regla estandarizada para el título principal de una vista.
*   **Mayúsculas / Etiquetas:** La clase `font-label-caps text-label-caps` está muy extendida, pero en ocasiones se mezcla con clases como `text-sm` o se fuerza el `font-bold` adicional.

## 2. Botones y Llamadas a la Acción (CTAs)

Los botones presentan múltiples combinaciones de colores, bordes y redondeos para lo que debería ser el mismo nivel de acción (botón primario o secundario).

*   **Texto sobre fondos primarios:** El contraste de texto en botones con fondo primario (`bg-primary-container`) varía enormemente:
    *   `text-[#000000]` (en `Movimientos.cshtml`, `Colmenas/Index.cshtml`) - *Código de color "hardcodeado" (mala práctica).*
    *   `text-on-primary` (en `Informes/Index.cshtml`, `Apiarios/Index.cshtml`) - *Lo correcto usando el sistema de diseño.*
    *   `text-on-primary-container` (en `Inventario.cshtml`).
*   **Radios de Borde (Border Radius):** Los botones mezclan el uso de `rounded`, `rounded-lg` y `rounded-full` para botones de tamaño y propósito similar.
*   **Paddings:** Los tamaños de los botones difieren, algunos usan `px-6 py-3`, otros `px-4 py-2` o `py-4`.
*   **Efectos Hover:** La transición de color en el estado `:hover` cambia entre `hover:bg-primary-fixed`, `hover:bg-primary-fixed-dim` y variaciones de opacidad como `hover:opacity-90`.

## 3. Estilos de Tablas y Estructuras de Datos

Cada vista que implementa una tabla está definiendo su propia estructura de clases, en lugar de compartir una base común.

*   **Contenedor base de tablas:**
    *   En `Inventario`, `CompararColmenas`, `CompararApiarios`, `Colmenas`, y `Home` se usa `<table class="w-full text-left border-collapse">`.
    *   En `Informes` se usa `<table class="w-full text-left font-body-sm text-body-sm">` (sin `border-collapse` explícito).
*   **Filas y Celdas:** Las filas combinan de forma dispar colores de fondo en `:hover` (`hover:bg-surface-container`), bordes (`border-b border-surface-container-highest`) y los paddings (`p-4` vs celdas personalizadas).

## 4. Contenedores de Tarjetas y Elementos UI

Las "cards" o artículos también muestran disparidad en el uso de los bordes e interacciones:

*   Algunos contenedores de tarjetas cambian en el hover el borde entero (`hover:border-primary-container`).
*   Otras tarjetas usan una barra o borde izquierdo o superior de color (como `bg-error` o un pseudoelemento de `bg-primary-container`).
*   Los bordes varían en transparencia e intensidad (`border-outline-variant` vs colores específicos del contenedor).

---

## 🛠️ Propuesta de Corrección

Para estandarizar el diseño web de la plataforma "Zánganos S.A." y mejorar su estética premium, deberíamos aplicar las siguientes acciones:

1.  **Eliminación de Colores Hardcodeados:**
    Rastrear y eliminar todos los colores "hardcodeados" como `text-[#000000]` y reemplazarlos por las variables de sistema correspondientes (`text-on-primary-container` o `text-on-surface`).

2.  **Estandarización de Botones (Creación de Componente o Clases Utilitarias):**
    Unificar los botones en tres niveles básicos usando clases consistentes a lo largo del CSS global, por ejemplo en `_Layout.cshtml.css` o `index.css`:
    *   **Primarios:** `bg-primary-container text-on-primary-container hover:bg-primary-fixed px-6 py-3 rounded-lg font-label-caps text-label-caps transition-colors`.
    *   **Secundarios / Outlined:** `bg-transparent border border-primary-container text-primary-container hover:bg-primary-container/10 px-4 py-2 rounded-lg font-label-caps text-label-caps transition-colors`.
    *   **Terciarios / Iconos:** Solo el texto o el icono con un `hover` sutil en el fondo y `rounded-full`.

3.  **Unificación de Componentes de Tabla:**
    Establecer una sola sintaxis para las tablas, donde todas tengan los mismos paddings, la misma clase de `border-collapse`, la misma jerarquía de fuentes en el `<thead>` y transiciones iguales al hacer hover sobre un `<tr>`.

4.  **Jerarquía de Textos (Typography System):**
    Asegurarnos de que el texto descriptivo siempre sea `text-body-md` y dejar `text-body-sm` solo para subtítulos secundarios, tooltips o leyendas muy pequeñas. Cada vista debería tener su `h1` en `font-display` y sus subtítulos de sección en `font-headline-*`.

Podemos comenzar abordando los botones y los textos en toda la aplicación cuando lo indiques, lo cual le dará un salto de calidad enorme a la interfaz en muy pocos pasos.
