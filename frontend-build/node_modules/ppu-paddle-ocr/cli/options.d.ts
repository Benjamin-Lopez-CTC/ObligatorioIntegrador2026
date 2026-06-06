/**
 * Maps parsed CLI flags onto the library's option objects. Every flag here has
 * a 1:1 counterpart in `PaddleOptions` / `RecognizeOptions`.
 */
import type { ParseArgsConfig } from "node:util";
import type { BatchRecognizeOptions, PaddleOptions, RecognizeOptions } from "../interface.js";
/** `parseArgs` option spec shared by every command. */
export declare const PARSE_OPTIONS: NonNullable<ParseArgsConfig["options"]>;
/** Shape of `parseArgs().values` for the spec above. */
export type CliValues = Record<string, string | boolean | undefined>;
/** Build the constructor `PaddleOptions` from parsed flags. */
export declare function buildPaddleOptions(values: CliValues): PaddleOptions;
/** Per-call recognize options from parsed flags. */
export declare function buildRecognizeOptions(values: CliValues): RecognizeOptions;
/** Batch options: recognize options plus concurrency/settle. */
export declare function buildBatchOptions(values: CliValues): BatchRecognizeOptions;
