package ShortDev.JLab.CompilerPipeline;

import javax.tools.JavaFileManager;

public final record CompilationResult(String Id, boolean IsSuccess, String Error, JLabFileManager FileManager){}
