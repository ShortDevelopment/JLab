package ShortDev.JLab.CompilerPipeline;

import javax.tools.JavaFileObject;
import java.net.URI;

public class Main {
    public static void main(String[] args) {
        String id = "abc";
        var uri = URI.create("string:///" + id.replace('.', '/') + JavaFileObject.Kind.SOURCE.extension);
        System.out.println(uri.getHost());
    }
}