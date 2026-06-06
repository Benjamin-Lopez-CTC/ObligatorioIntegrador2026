// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

window.addEventListener('load', () => {
    // Si hay un Service Worker activo y estamos online, pre-cargamos las vistas pesadas en caché
    if ('serviceWorker' in navigator && navigator.serviceWorker.controller && navigator.onLine) {
        fetch('/Home/GetAllOfflineUrls')
            .then(r => r.json())
            .then(urls => {
                navigator.serviceWorker.controller.postMessage({
                    type: 'PRECACHE_URLS',
                    urls: urls
                });
            })
            .catch(err => console.error("Error obteniendo URLs offline", err));
    }
});
