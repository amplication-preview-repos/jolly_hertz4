namespace MoniteBackendService.APIs.Dtos;

public class InvoiceCreateInput
{
    public double? Amount { get; set; }

    public DateTime CreatedAt { get; set; }

    public Customer? Customer { get; set; }

    public DateTime? DueDate { get; set; }

    public string? Id { get; set; }

    public string? InvoiceNumber { get; set; }

    public DateTime? IssueDate { get; set; }

    public List<Payment>? Payments { get; set; }

    public List<Product>? Products { get; set; }

    public DateTime UpdatedAt { get; set; }
}
