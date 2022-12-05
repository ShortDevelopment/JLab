package ShortDev.JLab.CompilerPipeline;

public record CompilationResult(String Id, boolean IsSuccess, String Error, JLabFileManager FileManager){}
