package ShortDev.JLab.CompilerPipeline.Decompiler;

// https://github.com/java-decompiler/jd-core/

import ShortDev.JLab.CompilerPipeline.CompilationResult;
import org.jd.core.v1.ClassFileToJavaSourceDecompiler;
import org.jd.core.v1.api.loader.Loader;
import org.jd.core.v1.api.printer.Printer;

import java.util.Iterator;

public final class DecompilerInvoker {
    private final Printer _printer = new JLabPrinter();

    private DecompilerInvoker() {
    }

    public static DecompilerInvoker Create() {
        return new DecompilerInvoker();
    }

    public String Decompile(CompilationResult result) throws Exception {
        var fileManager = result.FileManager();
        var loader = new JLabLoader(fileManager);
        ClassFileToJavaSourceDecompiler decompiler = new ClassFileToJavaSourceDecompiler();
        for (var id : fileManager.GetClassNames()) {
            decompiler.decompile(loader, _printer, id);
        }
        return _printer.toString();
    }
}
