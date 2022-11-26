package ShortDev.JLab.CompilerPipeline.Decompiler;

import ShortDev.JLab.CompilerPipeline.JLabFileManager;
import org.jd.core.v1.api.loader.*;

final class JLabLoader implements Loader {
    private final JLabFileManager _fileManager;

    public JLabLoader(JLabFileManager fileManager) {
        this._fileManager = fileManager;
    }

    @Override
    public byte[] load(String internalName) throws LoaderException {
        return _fileManager.TryGetJavaClass(internalName).GetClassData();
    }

    @Override
    public boolean canLoad(String internalName) {
        return _fileManager.TryGetJavaClass(internalName) != null;
    }
}
