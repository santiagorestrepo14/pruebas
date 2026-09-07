namespace TechPartsHub.Application.Services.Payments;

public sealed class PaymentResult
{
    public bool IsSuccessful { get; }
    public string TransactionCode { get; }
    public string Message { get; }

    public PaymentResult(bool isSuccessful, string transactionCode, string message)
    {
        IsSuccessful = isSuccessful;
        TransactionCode = transactionCode;
        Message = message;
    }
}
