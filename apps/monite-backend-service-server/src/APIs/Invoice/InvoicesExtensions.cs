using MoniteBackendService.APIs.Dtos;
using MoniteBackendService.Infrastructure.Models;

namespace MoniteBackendService.APIs.Extensions;

public static class InvoicesExtensions
{
    public static Invoice ToDto(this InvoiceDbModel model)
    {
        return new Invoice
        {
            Amount = model.Amount,
            CreatedAt = model.CreatedAt,
            Customer = model.CustomerId,
            DueDate = model.DueDate,
            Id = model.Id,
            InvoiceNumber = model.InvoiceNumber,
            IssueDate = model.IssueDate,
            Payments = model.Payments?.Select(x => x.Id).ToList(),
            Products = model.Products?.Select(x => x.Id).ToList(),
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static InvoiceDbModel ToModel(
        this InvoiceUpdateInput updateDto,
        InvoiceWhereUniqueInput uniqueId
    )
    {
        var invoice = new InvoiceDbModel
        {
            Id = uniqueId.Id,
            Amount = updateDto.Amount,
            DueDate = updateDto.DueDate,
            InvoiceNumber = updateDto.InvoiceNumber,
            IssueDate = updateDto.IssueDate
        };

        if (updateDto.CreatedAt != null)
        {
            invoice.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Customer != null)
        {
            invoice.CustomerId = updateDto.Customer;
        }
        if (updateDto.UpdatedAt != null)
        {
            invoice.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return invoice;
    }
}
