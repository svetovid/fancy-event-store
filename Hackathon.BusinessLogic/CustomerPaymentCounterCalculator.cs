using System.Linq;
using Hackaton.Shared.Messages;

namespace Hackathon.BusinessLogic
{
    public class CustomerPaymentCounterCalculator
    {
        public virtual PaymentStatisticIncrement GetPaymentChanges(PaymentStatusChangedMessage paymentChangeMsg)
        {
            if (paymentChangeMsg.StatusHistory.Count == 0)
            {
                return null;
            }

            var increment = new PaymentStatisticIncrement();

            // Update total count
            PopulatePaymentTotalCounters(increment, paymentChangeMsg);

            //skip message processing if current status is intermediate
            if (!IsCurrentStatusFinal(paymentChangeMsg))
            {
                return increment;
            }

            var sortedStatusHistories =
                paymentChangeMsg.StatusHistory.OrderByDescending(x => x.PaymentStatusHistoryId).ToList();

            var currentStatus = sortedStatusHistories[0].Status;

            var previousFinalStatusHistory = sortedStatusHistories.Skip(1).FirstOrDefault(x => x.Status.IsFinal());
            var previousStatus = previousFinalStatusHistory?.Status;

            // Here we work with payments in final status ONLY!
            PopulatePaymentSuccessCounters(increment, paymentChangeMsg, previousStatus, currentStatus);

            return increment;
        }

        private static void PopulatePaymentTotalCounters(PaymentStatisticIncrement increment, PaymentStatusChangedMessage paymentChangeMsg)
        {
            if (paymentChangeMsg.StatusHistory.Count == 1)
            {
                increment.TotalCount++;
                increment.TotalSum = paymentChangeMsg.Payment.PaymentAmount;
                increment.TotalBaseSum = paymentChangeMsg.Payment.BaseAmount;
            }
        }

        private static void PopulatePaymentSuccessCounters(PaymentStatisticIncrement increment,
            PaymentStatusChangedMessage paymentChangeMsg, string previousStatus, string currentStatus)
        {
            var sign = CalculatePaymentStatusIncrementSign(previousStatus, currentStatus);

            increment.SuccessCount += sign;
            increment.SuccessSum += sign * paymentChangeMsg.Payment.PaymentAmount;
            increment.SuccessBaseSum += sign * paymentChangeMsg.Payment.BaseAmount;
        }

        /// <summary>
        ///     Calculates the payment status increment sign.
        /// </summary>
        /// <param name="previousStatus">The previous payment status if any.</param>
        /// <param name="currentStatus">The current payment status.</param>
        /// <returns>
        ///     The sign of status changes:
        ///     +1: increase counter,
        ///     0:  do not affect counter,
        ///     -1:  decrease counter.
        /// </returns>
        private static int CalculatePaymentStatusIncrementSign(string previousStatus, string currentStatus)
        {
            if (!string.IsNullOrWhiteSpace(previousStatus))
            {
                if (previousStatus.IsSuccessful() && !currentStatus.IsSuccessful())
                {
                    return -1; // OK->NOK => decrease counter
                }
                if (!previousStatus.IsSuccessful() && currentStatus.IsSuccessful())
                {
                    return 1; // NOK->OK => increase counter
                }
                return 0; // OK->OK or NOK->NOK => do not affect counter
            }
            if (currentStatus.IsSuccessful())
            {
                return 1; // OK => increase counter
            }
            return 0; // NOK => do not affect counter
        }

        /// <summary>
        /// Checks whether current status in message is final.
        /// </summary>
        /// <param name="message"><see cref="PaymentStatusChangedMessage"/>The event message.</param>
        /// <returns>Value indicating whether status is final.</returns>
        private static bool IsCurrentStatusFinal(PaymentStatusChangedMessage message)
        {
            var currentStatus = message.StatusHistory.OrderBy(x => x.PaymentStatusHistoryId).Last().Status;
            return currentStatus.IsFinal();
        }
    }
}
