module Helpers

type PositionType =
    | Undefined 
    | Held 
    | Posted
        
type MovementType =
   | Undefined
   | Deliver 
   | Return 
type ApplyToParty =
   | Undefined 
   | Principal
   | Counterparty
type WorkflowType =
   | Undefined 
   | Demand
   | AnticDemand 
type LookupMessage = {
    Id: WorkflowType option
}
type MovementMessage = {
    ReceivingParty: ApplyToParty;
    Type: MovementType
}

type Direction = 
    | Held_Deliver_Prin
    | Held_Return_Counterparty
    | Posted_Return_Prin
    | Posted_Deliver_Counterparty
    | Unknown
    
let GetDirectionClass = function
   | Deliver, Principal  -> new HeldReturnCounterpary()
   | Return, Counterparty -> new Held_Return_Counterparty
   | Return, Principal -> Posted_Return_Prin
   | Deliver, Counterparty -> Posted_Deliver_Counterparty
   | MovementType.Undefined, _ | _, ApplyToParty.Undefined -> Unknown
   
let GetDirectionC#Class = function
   | Deliver, Principal  -> new Held_Deliver_Prin()
   | Return, Counterparty -> new Held_Return_Counterparty()
   | Return, Principal -> new Posted_Return_Prin()
   | Deliver, Counterparty -> new Posted_Deliver_Counterparty()
   
let GetApply = function
   | Held_Deliver_Prin | Posted_Return_Prin -> Principal
   | Held_Return_Counterparty | Posted_Deliver_Counterparty -> Counterparty
   | Unknown -> ApplyToParty.Undefined 

let GetPositionType (movementType:MovementType) (applyToParty: ApplyToParty) =
    match (movementType, applyToParty) with
       | Deliver, Principal | Return, Counterparty -> Held
       | Return, Principal | Deliver, Counterparty -> Posted
       | MovementType.Undefined, _ | _, ApplyToParty.Undefined -> PositionType.Undefined

let GetPositionTypeFromMessage (movementType: MovementType) (workflow:LookupMessage) =
    match (movementType, workflow.Id) with 
        | Deliver, Some Demand | Return, Some AnticDemand -> Held
        | Return, Some Demand | Deliver, Some AnticDemand -> Posted
        | _, None | _, Some Undefined | MovementType.Undefined, _ -> PositionType.Undefined
        
let GetAffectedPosition (movement:MovementMessage) =
    match (movement.ReceivingParty, movement.Type) with
        | Principal, Deliver | Counterparty, Return -> Held
        | Principal, Return | Counterparty, Deliver -> Posted
        | ApplyToParty.Undefined, _ | _, MovementType.Undefined -> PositionType.Undefined
