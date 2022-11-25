using ShortDev.JLab.JNI;

JavaVirtualMachine.SetupPlatform();
using (JavaVirtualMachine vm = JavaVirtualMachine.Create())
{
    Console.WriteLine(vm.Version);
    vm.CreateCompiler();
}
