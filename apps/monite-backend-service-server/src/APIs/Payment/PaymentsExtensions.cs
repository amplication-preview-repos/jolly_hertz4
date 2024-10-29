using MoniteBackendService.APIs.Dtos;
using MoniteBackendService.Infrastructure.Models;

namespace MoniteBackendService.APIs.Extensions;

public static class PaymentsExtensions
{
    public static Payment ToDto(this PaymentDbModel model)
    {
        return new Payment
        {
            Amount = model.Amount,
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Invoice = model.InvoiceId,
            PaymentDate = model.PaymentDate,
            PaymentMethod = model.PaymentMethod,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static PaymentDbModel ToModel(
        this PaymentUpdateInput updateDto,
        PaymentWhereUniqueInput uniqueId
    )
    {
        var payment = new PaymentDbModel
        {
            Id = uniqueId.Id,
            Amount = updateDto.Amount,
            PaymentDate = updateDto.PaymentDate,
            PaymentMethod = updateDto.PaymentMethod
        };

        if (updateDto.CreatedAt != null)
        {
            payment.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Invoice != null)
        {
            payment.InvoiceId = updateDto.Invoice;
        }
        if (updateDto.UpdatedAt != null)
        {
            payment.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return payment;
    }
}
