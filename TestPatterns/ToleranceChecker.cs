using System;

namespace TestPatterns
{
    public enum MsgType {
        AD = 0
    }
    public class AmountT {
        public int? Amount;
    }
    public class Message {
        public AmountT ATotal;
        public AmountT TotalValue;
        public AmountT CAmount;
        public MsgType MsgType;
        public AmountT CCAmount;
    }
    public class ToleranceChecker  {
        public bool ShouldCheckTolerance(decimal? toleranceFixedAmount, decimal? tolerancePercentage) {
            return toleranceFixedAmount.HasValue || tolerancePercentage.HasValue;
        }

        public bool PassesTotalTolerance(Message msg, decimal? autoApprovalToleranceFixedAmount, decimal? autoApprovalTolerancePercentage) {
            if (msg.ATotal == null || !msg.ATotal.Amount.HasValue || msg.TotalValue == null || !msg.TotalValue.Amount.HasValue)
                return false;

            return WithinTolerance(autoApprovalToleranceFixedAmount.GetValueOrDefault(), autoApprovalTolerancePercentage.GetValueOrDefault(), msg.TotalValue.Amount.Value, msg.ATotal.Amount.Value);
        }

        public bool PassesTotalATolerance(Message msg, decimal? aAmountToleranceFixedAmount, decimal? aAmountTolerancePercentage) {
            if (msg.ATotal == null || !msg.ATotal.Amount.HasValue)
                return false;

            var cAmountToUse = GetCAmount(msg.CAmount, msg.CCAmount, msg.MsgType);
            if (!cAmountToUse.HasValue)
                return false;

            return WithinTolerance(aAmountToleranceFixedAmount.GetValueOrDefault(), aAmountTolerancePercentage.GetValueOrDefault(), msg.ATotal.Amount.Value, cAmountToUse.Value);
        }

        private static bool WithinTolerance(decimal fixedAmountTolerance, decimal percentTolerance, decimal setTotal, decimal expectedTotal) {
            var delta = Math.Abs(setTotal - expectedTotal);
            return (delta <= fixedAmountTolerance) || ((delta / expectedTotal) * 100m <= percentTolerance);
        }

        private static decimal? GetCAmount(decimal? cAmount, decimal? cCAmount, MsgType msgType) {
            switch (msgType) {
                case MsgType.AD: {
                        if (!cAmount.HasValue && !cCAmount.HasValue)
                            return null;
                        if (!cAmount.HasValue)
                            return cCAmount.Value;
                        if (!cCAmount.HasValue)
                            return cAmount.Value;

                        return Math.Min(cAmount.Value, cCAmount.Value);
                    }
                default:
                    return cAmount;
            }
        }
    }
}

