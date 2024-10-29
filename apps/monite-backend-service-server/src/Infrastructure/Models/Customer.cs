using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoniteBackendService.Infrastructure.Models;

[Table("Customers")]
public class CustomerDbModel
{
    [StringLength(1000)]
    public string? Address { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    [StringLength(1000)]
    public string? FirstName { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public List<InvoiceDbModel>? Invoices { get; set; } = new List<InvoiceDbModel>();

    [StringLength(1000)]
    public string? LastName { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
