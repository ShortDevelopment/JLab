package ShortDev.JLab.CompilerPipeline;

import javax.tools.FileObject;
import javax.tools.ForwardingJavaFileManager;
import javax.tools.JavaFileManager;
import javax.tools.JavaFileObject;
import java.io.IOException;
import java.util.Dictionary;
import java.util.Hashtable;

public final class JLabFileManager extends ForwardingJavaFileManager<JavaFileManager> {

    public JLabFileManager(JavaFileManager fileManager) {
        super(fileManager);
    }

    private final Dictionary<String, JavaClassMemoryFile> _store = new Hashtable<String, JavaClassMemoryFile>();

    @Override
    public JavaFileObject getJavaFileForOutput(Location location, String className, JavaFileObject.Kind kind, FileObject sibling) throws IOException {
        System.out.println("Write java file \"" + className + "\"");
        if (kind == JavaFileObject.Kind.CLASS) {
            var result = new JavaClassMemoryFile(className);
            _store.put(className, result);
            return result;
        }
        throw new UnsupportedOperationException("Only class files are supported as output");
    }

    public String[] GetClassNames() {
        var result = new String[_store.size()];
        var keys = _store.keys();
        for (int i = 0; i < result.length; i++) {
            result[i] = keys.nextElement();
        }
        return result;
    }

    public JavaClassMemoryFile[] GetFiles() {
        var result = new JavaClassMemoryFile[_store.size()];
        var elements = _store.elements();
        for (int i = 0; i < result.length; i++) {
            result[i] = elements.nextElement();
        }
        return result;
    }

    @Override
    public FileObject getFileForOutput(Location location, String packageName, String relativeName, FileObject sibling) throws IOException {
        // return super.getFileForOutput(location, packageName, relativeName, sibling);
        throw new UnsupportedOperationException("No output allowed");
    }

    @Override
    public JavaFileObject getJavaFileForInput(Location location, String className, JavaFileObject.Kind kind) throws IOException {
        if (kind == JavaFileObject.Kind.CLASS) {
            var value = _store.get(className);
            if (value != null)
                return value;
        }
        return super.getJavaFileForInput(location, className, kind);
    }

    public JavaClassMemoryFile TryGetJavaClass(String name) {
        return _store.get(name);
    }

    @Override
    public void close() throws IOException {
        // Don't close underlying FileManager; we still need it!!
    }
}
