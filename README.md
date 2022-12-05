# JLab
`JLab` is a simple Java language playground built with `asp.net core`.   
It's by no means production ready and has to be compiled locally.

## Capabilities
 - Compile java code
 - Decompile it to dig into compiler optimizations (See [`java-decompiler/jd-core`](https://github.com/java-decompiler/jd-core))
 - View bytecode (See [`JLab-JDK-Tools`](https://github.com/ShortDevelopment/JLab-JDK-Tools))
 - Execute the code
 
## Prerequisites
 - dotnet 6 sdk
 - jdk
 - node / npm
 
 ## Build from source
 `dotnet run`   
 Compilation takes time as frontend (svelte) is compiled as well (See [`ShortDev.JLab.csproj`](https://github.com/ShortDevelopment/JLab/blob/master/ShortDev.JLab/ShortDev.JLab.csproj))
