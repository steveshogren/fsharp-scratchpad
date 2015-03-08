module Tolerance
open System

type MsgTypeT = | AD 
type AmountT = decimal option
type Percentage = decimal 
type FixedAmount = decimal 
type Message = { 
        TotalValue : AmountT;
        CAmount : AmountT;
        cCAmount : AmountT;
        MsgType : MsgTypeT;
        ATotal : AmountT}
        
let ShouldCheckTolerance = function 
    | Some _, Some _ -> true
    | _, _ -> false
        
let WithinTolerance (fixedAmountTolerance:FixedAmount) (percentTolerance:Percentage) (setTotal:decimal) (expectedTotal:decimal) =
   let delta = Math.Abs(setTotal - expectedTotal);
   (delta <= fixedAmountTolerance) || ((delta / expectedTotal) * 100m <= percentTolerance);
   
let decimalDefault = function | Some x -> x | None -> 0m
   
let PassesTotalTolerance msg (autoApprovalToleranceFixedAmount:FixedAmount option) (autoApprovalTolerancePercentage: Percentage option) =
    match (msg.ATotal, msg.TotalValue) with
        | Some aTotal, Some totalValue -> 
            WithinTolerance (decimalDefault autoApprovalToleranceFixedAmount) 
                            (decimalDefault autoApprovalTolerancePercentage) aTotal totalValue
        | _, _ -> false

let GetCAmount msg =
    match msg.MsgType with 
        | AD -> 
            match msg.CAmount, msg.cCAmount with
                | Some cAmount, Some cCAmount -> Some (Math.Min(cAmount, cCAmount))
                | Some cAmount, None          -> Some cAmount
                | None, Some cCAmount         -> Some cCAmount
                | None, None                  -> None
        | _ -> msg.CAmount

let PassesTotalATolerance msg (aAmountToleranceFixedAmount: decimal option) (aAmountTolerancePercentage: decimal option) =
    match msg.ATotal with
        | Some aTotal ->
            match GetCAmount msg with
                | Some cAmount -> 
                    WithinTolerance (decimalDefault aAmountToleranceFixedAmount) 
                                    (decimalDefault aAmountTolerancePercentage) aTotal cAmount
                | None -> false
        | None -> false
