import type { CanvasProcessor } from "ppu-ocv/canvas";
import type { CoreCanvas, ImageProcessorProvider } from "../platform.js";
/**
 * Preprocesses a cropped canvas region into a float tensor ready for the recognition model.
 *
 * Uses OpenCV resize when `imageProcessor` is supplied, otherwise falls back to canvas-native.
 */
export declare function preprocessImage(cropCanvas: CoreCanvas, targetHeight: number, imageProcessor: ImageProcessorProvider | undefined, createCanvasProcessor: (canvas: CoreCanvas) => CanvasProcessor): Promise<{
    imageTensor: Float32Array;
    tensorWidth: number;
    tensorHeight: number;
}>;
/**
 * Creates a normalized float tensor from a `CanvasProcessor`.
 */
export declare function createImageTensor(processor: CanvasProcessor, width: number, height: number): Float32Array;
/**
 * Creates a normalized float tensor from a canvas.
 *
 * The model expects three identical channels (grayscale replicated to RGB).
 * Fills channel 0, then `copyWithin` copies it to channels 1 and 2.
 */
export declare function createImageTensorFromCanvas(canvas: CoreCanvas, width: number, height: number): Float32Array;
