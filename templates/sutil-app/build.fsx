//
// FAKE script adapter for FSI
//
// Adapter details from here:
//      https://github.com/fsharp/FAKE/issues/2517
//
// Use this adapter template if you're suffering from this:
//      Error: Package manager key 'paket' was not registered
// Explanation here:
//      https://stackoverflow.com/questions/66665009/fix-for-package-manager-key-paket-was-not-registered-in-build-fsx
//
// Usage:
//   % dotnet fsi build.fsx
//   % dotnet fsi build.fsx --target <target>

#r "nuget: System.Reactive" // Prevent "Could not load file or assembly ..." error when using adapter
#r "nuget: Fake.Core.Target"
#load "node_modules/fable-publish-utils/PublishUtils.fs"

//open PublishUtils
open System
open System.IO
open Fake.Core
open PublishUtils
open Fake.Core.TargetOperators

// Boilerplate for adapter
System.Environment.GetCommandLineArgs()
|> Array.skip 2 // skip fsi.exe; build.fsx
|> Array.toList
|> Fake.Core.Context.FakeExecutionContext.Create false __SOURCE_FILE__
|> Fake.Core.Context.RuntimeContext.Fake
|> Fake.Core.Context.setExecutionContext

// ---------------------------------------------------
// -- Your targets and regular FAKE code goes below --

Target.create "publish:website" (fun _ ->
    PublishUtils.run("node publish.js")
)

Target.create "usage" (fun _ ->
    Console.WriteLine("Targets: clean, publish:website")
) 

Target.create "clean" (fun _ ->
    run("dotnet fable clean --yes")
    run("dotnet clean src/App")
)

Target.runOrDefault "usage"
