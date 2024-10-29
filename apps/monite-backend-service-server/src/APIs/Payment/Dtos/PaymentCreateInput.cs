using MoniteBackendService.Core.Enums;

namespace MoniteBackendService.APIs.Dtos;

public class PaymentCreateInput
{
    public double? Amount { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public Invoice? Invoice { get; set; }

    public DateTime? PaymentDate { get; set; }

    public PaymentMethodEnum? PaymentMethod { get; set; }

    public DateTime UpdatedAt { get; set; }
}
