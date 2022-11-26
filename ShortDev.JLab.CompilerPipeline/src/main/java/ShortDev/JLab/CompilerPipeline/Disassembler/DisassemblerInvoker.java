package ShortDev.JLab.CompilerPipeline.Disassembler;

// https://github.com/openjdk/jdk8u/tree/master/langtools/src/share/classes/com/sun/tools/javap

import ShortDev.JLab.CompilerPipeline.CompilationResult;
import com.sun.tools.javap.*;

import java.io.StringWriter;

public final class DisassemblerInvoker {
    private DisassemblerInvoker() {
    }

    public static DisassemblerInvoker Create() {
        return new DisassemblerInvoker();
    }

    public String Disassemble(CompilationResult compilationResult) throws JavapTask.BadArgs {
        if (!compilationResult.IsSuccess())
            throw new IllegalArgumentException("Compilation was not successful!");

        var task = new JavapTask();
        var writer = new StringWriter();
        task.setLog(writer);
        task.handleOptions(new String[]{"-c", "-l", compilationResult.Id() + ".class"});
        task.run();
        return writer.toString();
    }
}
