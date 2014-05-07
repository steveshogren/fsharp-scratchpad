module monadsfsharp.DatabaseFSharp

open System
open Helpers
open System.Collections.Generic

type Repository = 
   | InMemory of Dictionary<int, IPayment>
   | Postgres

let Add db (payment:IPayment) = 
    match db with
        | InMemory payments -> payments.Add(payment.GetId(),payment);
        | Postgres -> raise(NotImplementedException())
        
let GetAll = function
    | InMemory payments -> payments.Values;
    | Postgres -> raise(NotImplementedException())
        
let GetPaymentRepo = 
    match Config.configuration.["database"] with
        | "inmemory" -> InMemory(new Dictionary<int, IPayment>())
        | "postgres" -> Postgres 
     
[<EntryPoint>]
let main args = 
    let repo = GetPaymentRepo
    let payments = GetAll repo
    0

