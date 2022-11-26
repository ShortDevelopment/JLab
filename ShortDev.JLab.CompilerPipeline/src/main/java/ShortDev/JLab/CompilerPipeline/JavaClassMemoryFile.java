package ShortDev.JLab.CompilerPipeline;

import javax.tools.SimpleJavaFileObject;
import java.io.*;
import java.net.URI;

public final class JavaClassMemoryFile extends SimpleJavaFileObject {
    private final String _name;
    private byte[] _classData;

    protected JavaClassMemoryFile(String name) {
        super(URI.create("class:///" + name.replace('.', '/') + Kind.CLASS.extension), Kind.CLASS);

        this._name = name;
    }

    public byte[] GetClassData() {
        return this._classData;
    }

    @Override
    public OutputStream openOutputStream() throws IOException {
        var outputStream = new ByteArrayOutputStream();
        return new FilterOutputStream(outputStream) {
            @Override
            public void close() throws IOException {
                outputStream.close();
                _classData = outputStream.toByteArray();
            }
        };
    }

    @Override
    public InputStream openInputStream() throws IOException {
        return new ByteArrayInputStream(_classData);
    }
}
