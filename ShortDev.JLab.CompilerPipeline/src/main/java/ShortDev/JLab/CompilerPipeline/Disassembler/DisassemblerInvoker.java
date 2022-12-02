package ShortDev.JLab.CompilerPipeline.Disassembler;

// https://github.com/openjdk/jdk8u/tree/master/langtools/src/share/classes/com/sun/tools/javap

import ShortDev.JLab.CompilerPipeline.CompilationResult;
import com.sun.tools.javap.JavapTask;

import java.io.StringWriter;
import java.util.ArrayList;
import java.util.List;

public final class DisassemblerInvoker {
    private DisassemblerInvoker() {
    }

    public static DisassemblerInvoker Create() {
        return new DisassemblerInvoker();
    }

    private String[] _flags = new String[]{"-c"};

    public void SetFlag(String[] flags) {
        this._flags = flags;
    }

    public String Disassemble(CompilationResult compilationResult) throws JavapTask.BadArgs {
        if (!compilationResult.IsSuccess())
            throw new IllegalArgumentException("Compilation was not successful!");

        var flags = this._flags.clone();
        var fileManager = compilationResult.FileManager();

        var writer = new StringWriter();
        for (var id : fileManager.GetClassNames()) {
            var task = new JavapTask(writer, fileManager, null);
            var options = new ArrayList<String>(List.of(flags));
            options.add(id);
            task.handleOptions(options.toArray(new String[0]));
            task.run();

            writer.write("\r\n\r\n");
        }
        return writer.toString();
    }
}
