# Repository to demonstrate the issue related to how the code analyzer's satellite libraries

## Setup

The analyzer project has a NuGet dependency, and declares a class that relies on that dependency.

The consumer project references the analyzer project, and uses the class declared in the analyzer project. 
The consumer project also references the NuGet package that the analyzer project depends on - but this has no effect on the issue.

## Issue
Rebuild the solution. The analyzer will be called, and in of the hits it will try to instantiate the class declared in the analyzer project.
At this point it will fail, because the satellite assembly can1t be loaded.
The analyzer emits its own location.

Note, that the location is like this: `%TEMP%\VBCSCompiler\AnalyzerAssemblyLoader\{somerandomid}\{guid}\analyzer.dll`

When inspecting the folder, thge only file residing in it is the analyzer.dll, and the satellite assembly is not present.
