const CACHE_NAME = 'zanganos-cache-v5';
const DYNAMIC_CACHE_NAME = 'zanganos-dynamic-v5';

// Recursos estáticos básicos para la app shell
const STATIC_ASSETS = [
    '/',
    '/offline.html',
    '/Logo.png',
    '/favicon.ico',
    '/lib/jquery/dist/jquery.min.js',
    '/css/material-symbols-outlined.css',
    '/fonts/material-symbols-outlined.woff2'
];

// Instalación: Precargar assets estáticos
self.addEventListener('install', event => {
    event.waitUntil(
        caches.open(CACHE_NAME).then(cache => {
            console.log('[ServiceWorker] Precaching App Shell');
            return cache.addAll(STATIC_ASSETS);
        })
    );
    self.skipWaiting();
});

// Activación: Limpiar cachés viejas
self.addEventListener('activate', event => {
    event.waitUntil(
        caches.keys().then(keyList => {
            return Promise.all(keyList.map(key => {
                if (key !== CACHE_NAME && key !== DYNAMIC_CACHE_NAME) {
                    console.log('[ServiceWorker] Removing old cache', key);
                    return caches.delete(key);
                }
            }));
        })
    );
    self.clients.claim();
});

// Fetch: Network First, fallback to Cache
self.addEventListener('fetch', event => {
    if (event.request.method !== 'GET') {
        return;
    }

    event.respondWith(
        fetch(event.request)
            .then(networkResponse => {
                // Copia en caché dinámico
                const responseClone = networkResponse.clone();
                caches.open(DYNAMIC_CACHE_NAME).then(cache => {
                    // Evitar requests a extensiones u origines raros
                    if(event.request.url.startsWith('http') && !event.request.url.includes('browser-sync')) {
                        cache.put(event.request, responseClone);
                    }
                });
                return networkResponse;
            })
            .catch(async () => {
                // Intento en caché
                const cachedResponse = await caches.match(event.request);
                if (cachedResponse) {
                    return cachedResponse;
                }
                
                // Fallback para navegación HTML
                if (event.request.mode === 'navigate') {
                    return caches.match('/offline.html');
                }
                
                return new Response('Network error happened', { status: 408, headers: { 'Content-Type': 'text/plain' } });
            })
    );
});

// Escuchar mensaje del cliente para precargar colmenas u otras URLs
self.addEventListener('message', event => {
    if (event.data && event.data.type === 'PRECACHE_URLS') {
        const urls = event.data.urls;
        console.log('[ServiceWorker] Precaching URLs:', urls.length);
        event.waitUntil(
            caches.open(DYNAMIC_CACHE_NAME).then(cache => {
                return Promise.all(urls.map(url => fetch(url).then(res => cache.put(url, res)).catch(err => console.log('Error precaching', url, err))));
            })
        );
    }
});
