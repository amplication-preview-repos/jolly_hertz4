namespace MoniteBackendService.APIs.Dtos;

public class CustomerWhereInput
{
    public string? Address { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Email { get; set; }

    public string? FirstName { get; set; }

    public string? Id { get; set; }

    public List<string>? Invoices { get; set; }

    public string? LastName { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
