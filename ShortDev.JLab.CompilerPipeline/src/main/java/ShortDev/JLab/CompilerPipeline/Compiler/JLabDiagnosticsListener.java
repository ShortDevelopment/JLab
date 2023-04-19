package ShortDev.JLab.CompilerPipeline.Compiler;

import javax.tools.Diagnostic;
import javax.tools.DiagnosticListener;
import javax.tools.JavaFileObject;
import java.util.ArrayList;
import java.util.List;
import java.util.Locale;

public final class JLabDiagnosticsListener implements DiagnosticListener<JavaFileObject> {
    private final Locale _locale;

    public JLabDiagnosticsListener(Locale locale) {
        _locale = locale;
    }

    final List<String> _entries = new ArrayList<>();

    public String toJson() {
        return "[" + String.join(",", _entries) + "]";
    }

    @Override
    public void report(Diagnostic<? extends JavaFileObject> diagnostic) {
        String jsonBuilder = "{" +
                "\"start\":" + diagnostic.getStartPosition() + "," +
                "\"end\":" + diagnostic.getEndPosition() + "," +
                "\"kind\":" + '"' + diagnostic.getKind() + '"' + "," +
                "\"msg\":" + '"' + diagnostic.getMessage(_locale).replace("\"", "\\u0022") + '"' +
                "}";
        _entries.add(jsonBuilder);
    }
}
