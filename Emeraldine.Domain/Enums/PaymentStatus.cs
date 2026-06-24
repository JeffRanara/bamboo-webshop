namespace Emeraldine.Domain.Enums;

public enum PaymentStatus
{
    PendingPayment,
    Paid,
    PaymentFailed,
    Refunded,
    PartiallyRefunded,
    Cancelled
}