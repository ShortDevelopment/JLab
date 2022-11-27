using Microsoft.AspNetCore.Mvc;
using ShortDev.JLab.Services;
using System.Text;

namespace ShortDev.JLab.Controllers;

[Controller]
public class DecompileController : Controller
{
    readonly JvmService _jvm;
    public DecompileController(JvmService jvm)
        => _jvm = jvm;

    [HttpPost, Route("api/v1.0/JavaCode")]
    [Consumes("text/plain")]
    public async Task<IActionResult> Decompile()
    {
        using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
        {
            string source = await reader.ReadToEndAsync();
            return Content(await _jvm.DecompileAsync(source));
        }
    }

    [HttpPost, Route("api/v1.0/ByteCode")]
    [Consumes("text/plain")]
    public async Task<IActionResult> ByteCode()
    {
        using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
        {
            string source = await reader.ReadToEndAsync();
            return Content(await _jvm.DisassembleAsync(source, "-c", "-l", "-p", "-s", "-constants"));
        }
    }

    [HttpPost, Route("api/v1.0/ByteCode-Verbose")]
    [Consumes("text/plain")]
    public async Task<IActionResult> ByteCodeVerbose()
    {
        using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
        {
            string source = await reader.ReadToEndAsync();
            return Content(await _jvm.DisassembleAsync(source, "-c", "-l", "-p", "-s", "-constants", "-v"));
        }
    }
}
