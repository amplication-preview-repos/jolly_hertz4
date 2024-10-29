namespace MoniteBackendService.APIs.Dtos;

public class ProductCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public Invoice? Invoice { get; set; }

    public double? Price { get; set; }

    public string? ProductName { get; set; }

    public int? Quantity { get; set; }

    public DateTime UpdatedAt { get; set; }
}
