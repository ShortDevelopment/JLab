package ShortDev.JLab.CompilerPipeline.Compiler;

import ShortDev.JLab.CompilerPipeline.CompilationResult;
import ShortDev.JLab.CompilerPipeline.JLabFileManager;

import javax.tools.JavaCompiler;
import javax.tools.JavaFileManager;
import javax.tools.ToolProvider;
import java.io.Closeable;
import java.io.IOException;
import java.io.StringWriter;
import java.util.Arrays;

public final class CompilerInvoker implements Closeable {
    private final JavaCompiler _compiler;
    private final JavaFileManager _fileManager;

    private CompilerInvoker(JavaCompiler compiler, JavaFileManager fileManager) {
        _compiler = compiler;
        _fileManager = fileManager;
    }

    public static CompilerInvoker Create() {
        var systemCompiler = ToolProvider.getSystemJavaCompiler();
        if (systemCompiler == null)
            throw new IllegalStateException("Could not find system compiler");

        var fileManager = systemCompiler.getStandardFileManager(null, null, null);
        return new CompilerInvoker(systemCompiler, fileManager);
    }

    public CompilationResult Compile(String id, String code) {
        var compilationUnits = Arrays.asList(new JavaStringSource(id, code));

        var fileManager = new JLabFileManager(_fileManager);

        var writer = new StringWriter();
        var task = _compiler.getTask(writer, fileManager, null, null, null, compilationUnits);
        var isSuccess = task.call();
        return new CompilationResult(id, isSuccess, writer.toString(), fileManager);
    }

    @Override
    public void close() throws IOException {
        _fileManager.close();
    }
}