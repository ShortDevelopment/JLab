package ShortDev.JLab.CompilerPipeline.Decompiler;

// https://github.com/java-decompiler/jd-core/

import org.jd.core.v1.ClassFileToJavaSourceDecompiler;
import org.jd.core.v1.api.loader.Loader;
import org.jd.core.v1.api.printer.Printer;

public final class DecompilerInvoker {
    private final Loader _loader = new JLabLoader();
    private final Printer _printer = new JLabPrinter();

    private DecompilerInvoker() {
    }

    public static DecompilerInvoker Create() {
        return new DecompilerInvoker();
    }

    public String Decompile(String path) throws Exception {
        ClassFileToJavaSourceDecompiler decompiler = new ClassFileToJavaSourceDecompiler();
        decompiler.decompile(_loader, _printer, path);
        return _printer.toString();
    }
}
