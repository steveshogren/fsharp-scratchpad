using System;

namespace TestPatterns
{
    public enum PositionType
    {
        Undefined = 0,
        Held = 1,
        Posted = 2,
    }

    public enum MovementType
    {
        Undefined = 0,
        Deliver = 1,
        Return = 2,
    }

    public enum ApplyToParty
    {
        Undefined = 0,
        Principal = 1,
        Counterparty = 2,
    }

    public enum WorkflowType
    {
        Undefined = 0,
        Demand = 1,
        AnticDemand = 2,
    }

    public class LookupMessage
    {
        public int? Id;
    }

    public class MovementMessage
    {
        public ApplyToParty ReceivingParty;
        public MovementType Type;
    }

    public static class Helper
    {
        public static PositionType GetPositionType (MovementType movementType, ApplyToParty applyToParty)
        {
            if ((movementType == MovementType.Deliver && applyToParty == ApplyToParty.Principal) || (movementType == MovementType.Return && applyToParty == ApplyToParty.Counterparty))
                return PositionType.Held;
            if ((movementType == MovementType.Return && applyToParty == ApplyToParty.Principal) || (movementType == MovementType.Deliver && applyToParty == ApplyToParty.Counterparty))
                return PositionType.Posted;

            return PositionType.Undefined;
        }

        public static PositionType GetPositionType (MovementType movementType, LookupMessage workflow)
        {
            return GetPositionType (movementType, (WorkflowType)workflow.Id.Value);
        }

        private static PositionType GetPositionType (MovementType movementType, WorkflowType workflowType)
        {
            if ((movementType == MovementType.Deliver && workflowType == WorkflowType.Demand) 
                || (movementType == MovementType.Return && workflowType == WorkflowType.AnticDemand))
                return PositionType.Held;
            if ((movementType == MovementType.Return && workflowType == WorkflowType.Demand) 
                || (movementType == MovementType.Deliver && workflowType == WorkflowType.AnticDemand))
                return PositionType.Posted;

            return PositionType.Undefined;
        }

        public static PositionType GetAffectedPosition (this MovementMessage movement)
        {
            switch (movement.ReceivingParty) {
            case ApplyToParty.Principal:
                if (movement.Type == MovementType.Deliver)
                    return PositionType.Held;
                if (movement.Type == MovementType.Return)
                    return PositionType.Posted;
                return PositionType.Undefined;
            case ApplyToParty.Counterparty:
                if (movement.Type == MovementType.Deliver)
                    return PositionType.Posted;
                if (movement.Type == MovementType.Return)
                    return PositionType.Held;
                return PositionType.Undefined;
            default:
                return PositionType.Undefined;
            }
        }
    }
}


