using ShortDev.JLab.JNI.Compiler;
using System.Diagnostics;

namespace ShortDev.JLab.Host;

public sealed class HostLauncher
{
    readonly List<JavaClassData> _classes = new();

    public void AddClasses(params JavaClassData[] classes)
        => _classes.AddRange(classes);

    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(3);

    public async Task<string> LaunchForResultAsync()
    {
        var currentProccess = Process.GetCurrentProcess();
        using (Process p = new())
        using (StringWriter stdOutputWriter = new())
        {
            p.StartInfo.FileName = currentProccess.MainModule!.FileName;
            p.StartInfo.Arguments = HostBootstrap.CmdArgumentName;
            p.StartInfo.UseShellExecute = false;

            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;

            p.EnableRaisingEvents = true;
            p.ErrorDataReceived += (s, e) => stdOutputWriter.WriteLine(e.Data);
            p.OutputDataReceived += (s, e) => stdOutputWriter.WriteLine(e.Data);

            TaskCompletionSource promise = new();
            p.Exited += (s, e) => promise.SetResult();

            var startTime = DateTime.Now;
            p.Start();
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();

            try
            {
                JavaClassData.Write(p.StandardInput.BaseStream, _classes);
            }
            catch (Exception ex)
            {
                stdOutputWriter.WriteLine($"❗ {nameof(HostBootstrap)}: {ex.Message}");
            }

            await Task.WhenAny(
                Task.Delay(Timeout),
                promise.Task
            );

            stdOutputWriter.Write("\n\n === Status Info === \n\n");
            if (!p.HasExited)
            {
                p.Kill();
                stdOutputWriter.WriteLine($"Process has been killed due to the timeout of {Timeout}");
            }
            else
            {
                stdOutputWriter.WriteLine($"Process terminated with {p.ExitCode} after {p.ExitTime - startTime}");
            }

            return stdOutputWriter.ToString();
        }
    }
}
