namespace MoniteBackendService.APIs.Dtos;

public class Invoice
{
    public double? Amount { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Customer { get; set; }

    public DateTime? DueDate { get; set; }

    public string Id { get; set; }

    public string? InvoiceNumber { get; set; }

    public DateTime? IssueDate { get; set; }

    public List<string>? Payments { get; set; }

    public List<string>? Products { get; set; }

    public DateTime UpdatedAt { get; set; }
}
