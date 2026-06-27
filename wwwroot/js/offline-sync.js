// offline-sync.js

// DB_NAME ya está declarado en db.js que se carga antes, usamos ese.
const STORE_NAME = 'sync-queue';

function openDB() {
    return new Promise((resolve, reject) => {
        const request = indexedDB.open(DB_NAME, 1);
        request.onupgradeneeded = (event) => {
            const db = event.target.result;
            if (!db.objectStoreNames.contains(STORE_NAME)) {
                db.createObjectStore(STORE_NAME, { keyPath: 'id', autoIncrement: true });
            }
        };
        request.onsuccess = () => resolve(request.result);
        request.onerror = () => reject(request.error);
    });
}

function saveToQueue(url, method, formDataObj) {
    return openDB().then(db => {
        return new Promise((resolve, reject) => {
            const tx = db.transaction(STORE_NAME, 'readwrite');
            const store = tx.objectStore(STORE_NAME);
            const request = store.add({ url, method, data: formDataObj, timestamp: Date.now() });
            request.onsuccess = () => resolve();
            request.onerror = () => reject(request.error);
        });
    });
}

function getQueue() {
    return openDB().then(db => {
        return new Promise((resolve, reject) => {
            const tx = db.transaction(STORE_NAME, 'readonly');
            const store = tx.objectStore(STORE_NAME);
            const request = store.getAll();
            request.onsuccess = () => resolve(request.result);
            request.onerror = () => reject(request.error);
        });
    });
}

function deleteFromQueue(id) {
    return openDB().then(db => {
        return new Promise((resolve, reject) => {
            const tx = db.transaction(STORE_NAME, 'readwrite');
            const store = tx.objectStore(STORE_NAME);
            const request = store.delete(id);
            request.onsuccess = () => resolve();
            request.onerror = () => reject(request.error);
        });
    });
}

async function syncOfflineQueue() {
    if (!navigator.onLine) return;
    
    try {
        const queue = await getQueue();
        if (queue.length === 0) return;
        
        console.log(`[Offline Sync] Found ${queue.length} items to sync.`);
        let syncedCount = 0;
        
        for (const item of queue) {
            try {
                const formBody = new URLSearchParams(item.data).toString();
                
                const response = await fetch(item.url, {
                    method: item.method,
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded'
                    },
                    body: formBody
                });
                
                // Fetch follows redirects by default (which MVC uses on success)
                if (response.ok || response.type === 'opaqueredirect') {
                    console.log(`[Offline Sync] Successfully synced item ${item.id}`);
                    await deleteFromQueue(item.id);
                    syncedCount++;
                } else {
                    console.error(`[Offline Sync] Server returned status ${response.status} for item ${item.id}`);
                }
            } catch (err) {
                console.error(`[Offline Sync] Error syncing item ${item.id}:`, err);
            }
        }
        
        if (syncedCount > 0) {
            if(window.showToast) {
                window.showToast("Sincronización completada. Recarga la página para ver los cambios.");
            }
        }
    } catch (err) {
        console.error('[Offline Sync] Error accessing queue:', err);
    }
}

window.addEventListener('online', () => {
    console.log('[Network] Back online, triggering sync...');
    syncOfflineQueue();
});

// Intercepción Global de Formularios
document.addEventListener('submit', async (e) => {
    if (!navigator.onLine) {
        const form = e.target;
        if (form.method && form.method.toUpperCase() === 'POST') {
            e.preventDefault(); // Detiene el envío real
            
            const formData = new FormData(form);
            const dataObj = {};
            formData.forEach((value, key) => { dataObj[key] = value; });
            
            try {
                await saveToQueue(form.action || window.location.pathname, form.method, dataObj);
                
                // Ocultar modal si el formulario estaba en uno
                const modal = form.closest('.modal-backdrop');
                if (modal && window.hideModal) {
                    window.hideModal(modal);
                }
                
                // Avisar al usuario
                if (window.showToast) {
                    window.showToast("Sin conexión. Los cambios se han guardado localmente y serán visibles cuando recupere la conexión y recargue la página.", "success");
                } else {
                    alert("Sin conexión. Los cambios se han guardado localmente.");
                }
                
                form.reset();
            } catch (err) {
                console.error('Error saving to offline queue:', err);
                if (window.showToast) window.showToast("Error al guardar localmente", "error");
            }
        }
    }
});

// Precarga y sincronización al iniciar
window.addEventListener('load', async () => {
    if (navigator.onLine) {
        syncOfflineQueue();
        
        // Descargar datos consolidados masivos y guardarlos en IndexedDB
        if (window.ZanganosDB) {
            try {
                if (window.updateOfflineProgress) window.updateOfflineProgress(20);
                console.log('[Sync] Fetching consolidated data from /Home/SyncApiariosColmenas...');
                const response = await fetch('/Home/SyncApiariosColmenas');
                if (response.ok) {
                    if (window.updateOfflineProgress) window.updateOfflineProgress(50);
                    const data = await response.json();
                    
                    if (data.apiarios) {
                        await window.ZanganosDB.saveToStore('apiarios', data.apiarios);
                    }
                    if (data.colmenas) {
                        await window.ZanganosDB.saveToStore('colmenas', data.colmenas);
                    }
                    console.log('[Sync] Database populated successfully for offline use.');
                    if (window.updateOfflineProgress) window.updateOfflineProgress(70);
                }
            } catch (err) {
                console.error('[Sync] Error fetching consolidated data:', err);
            }
        }
    }
    
    // Precarga automática de páginas para lectura offline (App Shell y Detalles)
    if ('serviceWorker' in navigator && navigator.onLine) {
        try {
            const urlsResponse = await fetch('/Home/GetAllOfflineUrls');
            if (urlsResponse.ok) {
                const urlsToPrecache = await urlsResponse.json();
                navigator.serviceWorker.ready.then(registration => {
                    if (registration.active) {
                        registration.active.postMessage({
                            type: 'PRECACHE_URLS',
                            urls: urlsToPrecache
                        });
                    }
                });
            }
            if (window.updateOfflineProgress) window.updateOfflineProgress(100);
            
            setTimeout(() => {
                if (window.finishOfflineLoading) window.finishOfflineLoading();
            }, 600); // Dar un momento visual para ver el 100%
        } catch (err) {
            console.error('[Sync] Error fetching offline urls:', err);
            if (window.finishOfflineLoading) window.finishOfflineLoading();
        }
    } else {
        if (window.finishOfflineLoading) window.finishOfflineLoading();
    }
});
