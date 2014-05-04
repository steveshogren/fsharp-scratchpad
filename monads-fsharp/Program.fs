
// NOTE: If warnings appear, you may need to retarget this project to .NET 4.0. Show the Solution
// Pad, right-click on the project node, choose 'Options --> Build --> General' and change the target
// framework to .NET 4.0 or .NET 4.5.

module monadsfsharp.Main

open System
open TestPatterns

let convert = function
        | Language.English, 0 -> "zero" 
        | Language.English, 1 -> "one"
        | Language.English, _ -> "..."
        | Language.Spanish, 0 -> "zero" 
        | Language.Spanish, 1 -> "uno" 
        | Language.Spanish, _ -> "~~~"
        
     
[<EntryPoint>]
let main args = 
    //Console.WriteLine("Converted to " +  TestPatterns.convert(1, Language.English))
    let x = convert(Language.English, 1)
    Console.WriteLine("Converted to " +  x)
    0

