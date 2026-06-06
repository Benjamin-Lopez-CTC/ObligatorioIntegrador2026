# Zánganos S.A. - Sistema de Gestión Apícola (Obligatorio Integrador 2026)

Este proyecto es un **Sistema Integral de Gestión Apícola** diseñado específicamente para resolver las necesidades logísticas, técnicas y productivas de los apicultores en el campo. Se destaca por su capacidad de funcionar bajo condiciones de conectividad nula (Offline-First) y por integrar inteligencia artificial local para la identificación de colmenas. Aunque el sistema fue diseñado originalmente para escritorio, soporta completamente una vista responsiva adaptada para dispositivos móviles.

## 🚀 Características Principales

*   **📱 Arquitectura Offline-First (PWA):**
    El sistema utiliza Service Workers y cachés dinámicas (IndexedDB/CacheStorage) para permitir a los apicultores descargar su base de datos de colmenas y apiarios antes de ir al campo. Una vez en "Modo Avión" o zonas sin señal, la aplicación funciona de forma completamente transparente, permitiendo la lectura de datos históricos.
*   **📷 Escáner IA Híbrido (Gemini + PaddleOCR):**
    Para facilitar la identificación física de colmenas en pleno trabajo de campo, se implementó un motor de Reconocimiento Óptico de Caracteres (OCR) inteligente y adaptable:
    *   **Con Conexión (Online):** Utiliza la potente API de **Gemini 2.5 Flash** en la nube para un escaneo extremadamente rápido y versátil.
    *   **Sin Conexión (Offline):** Si no hay internet, el sistema hace fallback automáticamente a un motor local basado en **ONNX Runtime Web** y modelos pre-entrenados de **PaddleOCR**. El celular procesa la foto decodificando el ID numérico mediante WebAssembly sin necesidad de enviar un solo byte a la red, garantizando privacidad y que el trabajo de campo nunca se interrumpa.
*   **🐝 Gestión de Apiarios y Colmenas:**
    *   Mapeo de instalaciones y control poblacional.
    *   Seguimiento del estado de salud, docilidad y estado de la Reina.
    *   **Colmenas Piloto:** Capacidad para registrar temperatura y humedad interna para casos de estudio o trazabilidad especial.
*   **🍯 Trazabilidad de Producción y Extracciones:**
    Registro detallado de cosechas en masa, contabilizando alzas, alzas de 3/4 y medias alzas retiradas.
*   **📊 Análisis Financiero e Informes:**
    Las extracciones pueden vincularse al mercado actual, calculando las ganancias generadas por cada colmena y agrupando la rentabilidad global de la temporada.

## 🛠️ Stack Tecnológico

*   **Backend:** ASP.NET Core 9.0 MVC (C#)
*   **Base de Datos:** SQLite + Entity Framework Core
*   **Frontend:** HTML5, Vanilla JS, Tailwind CSS (Sistema de diseño "StitchUI" a medida), Material Icons
*   **Inteligencia Artificial (Edge Computing):** ONNX Runtime Web (`ort-wasm`), modelos pre-entrenados de PaddleOCR
*   **PWA:** Service Workers, Manifest, Cache API

## ⚙️ Requisitos y Ejecución

1.  Asegúrate de tener instalado el **.NET 9.0 SDK**.
2.  Clona el repositorio y abre la carpeta del proyecto en la terminal.
3.  Ejecuta el proyecto utilizando:
    ```bash
    dotnet run
    ```
4.  El servidor se levantará (usualmente en `http://localhost:5280` u otro puerto especificado).
5.  *Nota:* Para probar las características progresivas (PWA) y el acceso a la cámara web/celular en otros dispositivos de tu red, deberás exponer tu puerto local mediante un túnel seguro con HTTPS (por ejemplo, utilizando `ngrok`), ya que navegadores móviles exigen conexiones seguras para habilitar la cámara o Service Workers fuera de `localhost`.

## 🎨 Guía de Diseño (StitchUI)

Las vistas del sistema han sido desarrolladas siguiendo un conjunto estricto de lineamientos visuales y de usabilidad definidos en la carpeta `StitchUI/DESIGN`. El enfoque central es mantener un diseño de clase mundial, responsivo, amigable y con elementos generosos para facilitar toques rápidos con guantes de trabajo u operarios en movimiento.

*Nota de Créditos:* El estilo visual inicial, la conceptualización y la estructura base del sistema de diseño (StitchUI) fueron realizados en colaboración con **Stitch, la IA generativa de diseño de Google**, asegurando una interfaz moderna, limpia y altamente funcional desde el primer día.
