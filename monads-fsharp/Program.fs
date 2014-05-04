
// NOTE: If warnings appear, you may need to retarget this project to .NET 4.0. Show the Solution
// Pad, right-click on the project node, choose 'Options --> Build --> General' and change the target
// framework to .NET 4.0 or .NET 4.5.

module monadsfsharp.DatabaseFSharp

open System
open Helpers

type Repositories = 
   | InMemory
   | Postgres

let Add db (payment:IPayment) = 
    match db with
        | InMemory -> Config.payments.Add(payment.GetId(),payment);
        | Postgres -> raise(NotImplementedException())
let GetAll db = 
    match db with
        | InMemory -> Config.payments.Values;
        | Postgres -> raise(NotImplementedException())
        
let GetPaymentRepo = 
    match Config.configuration.["database"] with
        | "inmemory" -> InMemory   
        | "postgres" -> Postgres   
        
     
[<EntryPoint>]
let main args = 
    let repo = GetPaymentRepo
    let payments = GetAll repo
    Console.WriteLine("Converted to " +  "")
    0

