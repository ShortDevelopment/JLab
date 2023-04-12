
export interface JLabResult {
    content: string;
    diagnostics: string;
}

export enum JLabDiagnosticType {
    /** Problem which prevents the tool's normal completion. */
    Error = 'ERROR',
    /** Problem similar to a warning, but is mandated by the tool's specification. */
    MandatoryWarning = 'MANDATORY_WARNING',
    /** Informative message from the tool. */
    Note = 'NOTE',
    /** Diagnostic which does not fit within the other kinds. */
    Other = 'OTHER',
    /** Problem which does not usually prevent the tool from completing normally.*/
    Warning = 'WARNING'
}

export interface JLabDiagnostic {
    kind: JLabDiagnosticType;
    start: number;
    end: number;
    msg: string;
}