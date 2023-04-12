package ShortDev.JLab.CompilerPipeline;

import ShortDev.JLab.CompilerPipeline.Compiler.JLabDiagnosticsListener;

public record CompilationResult(
        String Id,
        boolean IsSuccess,
        String Error,
        JLabFileManager FileManager,
        JLabDiagnosticsListener Diagnostics) {
    public String getDiagnosticJson() {
        return Diagnostics.toJson();
    }
}
