package ShortDev.JLab.CompilerPipeline;

import javax.tools.SimpleJavaFileObject;
import java.io.IOException;
import java.net.URI;

public final class JavaStringSource extends SimpleJavaFileObject {
    private final String _code;

    public JavaStringSource(String id, String code) {
        super(URI.create("string:///" + id.replace('.', '/') + Kind.SOURCE.extension), Kind.SOURCE);
        _code = code;
    }

    @Override
    public CharSequence getCharContent(boolean ignoreEncodingErrors) throws IOException {
        return _code;
    }
}
