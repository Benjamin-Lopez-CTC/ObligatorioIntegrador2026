/** On-disk directory for cached model and dictionary files. */
export declare const CACHE_DIR: string;
/**
 * Downloads a resource from `url` and writes it to {@link CACHE_DIR}, or reads
 * from the cache if the file already exists.
 */
export declare function fetchAndCacheResource(url: string, verbose?: boolean): Promise<ArrayBuffer>;
