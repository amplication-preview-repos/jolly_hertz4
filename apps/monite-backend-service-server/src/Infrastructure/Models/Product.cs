using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoniteBackendService.Infrastructure.Models;

[Table("Products")]
public class ProductDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? InvoiceId { get; set; }

    [ForeignKey(nameof(InvoiceId))]
    public InvoiceDbModel? Invoice { get; set; } = null;

    [Range(-999999999, 999999999)]
    public double? Price { get; set; }

    [StringLength(1000)]
    public string? ProductName { get; set; }

    [Range(-999999999, 999999999)]
    public int? Quantity { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
