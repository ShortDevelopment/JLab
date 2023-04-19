package ShortDev.JLab.CompilerPipeline.Compiler;

import ShortDev.JLab.CompilerPipeline.CompilationResult;
import ShortDev.JLab.CompilerPipeline.JLabFileManager;

import javax.tools.JavaCompiler;
import javax.tools.JavaFileManager;
import javax.tools.ToolProvider;
import java.io.Closeable;
import java.io.IOException;
import java.io.StringWriter;
import java.util.List;
import java.util.Locale;

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

    private List<String> _options = null;

    public void SetOptions(String[] options) {
        if (options == null)
            _options = null;
        else
            _options = List.of(options);
    }

    private Locale _locale = Locale.US;

    public void SetLocale(String locale) {
        _locale = Locale.forLanguageTag(locale);
    }

    public CompilationResult Compile(String id, String code) {
        var compilationUnits = List.of(new JavaStringSource(id, code));

        var fileManager = new JLabFileManager(_fileManager);

        var listener = new JLabDiagnosticsListener(_locale);

        var writer = new StringWriter();
        var task = _compiler.getTask(writer, fileManager, listener, _options, null, compilationUnits);
        var isSuccess = task.call();
        return new CompilationResult(id, isSuccess, writer.toString(), fileManager, listener);
    }

    @Override
    public void close() throws IOException {
        _fileManager.close();
    }
}