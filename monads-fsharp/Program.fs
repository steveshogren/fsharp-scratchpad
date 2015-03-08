module monadsfsharp.DatabaseFSharp

open System
open System.Linq
open System.Collections.Generic
open FSharpx.Option

let add x y = x + y

let p = match 1 with 
        | _ when 1 > 9 -> 8
        | _ when 5 = 5 -> 5
        | _ -> 1
        
let x = Some "test"
let inline (<*>) f x = Option.bind (fun x -> Some (f x)) x
let t = (fun y -> y.ToString) <*> x

let listElement =
    let l = [1;2;3;4]
    if l.Count() > 10 then
        l.[10]
    else 0 

type X (ai:int) =
    member x.a = ai

let runner = 
    let a = Some (X 1) 
    let t = X 1
    (fun (x:X) -> x.a + 1) <*> a
    
type ApprovalStatus =
    Pending
    | Approved of int*int
    | Rejected of int
    
let y = Approved(1, 1)
let r = match y with | Approved (n,_) -> Some n | _ -> None
 
  
[<EntryPoint>]
let main args = 
    let jolly = 4
    Console.WriteLine(runner)
    Console.WriteLine(listElement)
    0
