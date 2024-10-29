using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoniteBackendService.Infrastructure.Models;

[Table("Invoices")]
public class InvoiceDbModel
{
    [Range(-999999999, 999999999)]
    public double? Amount { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public CustomerDbModel? Customer { get; set; } = null;

    public DateTime? DueDate { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? InvoiceNumber { get; set; }

    public DateTime? IssueDate { get; set; }

    public List<PaymentDbModel>? Payments { get; set; } = new List<PaymentDbModel>();

    public List<ProductDbModel>? Products { get; set; } = new List<ProductDbModel>();

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
