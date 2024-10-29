using MoniteBackendService.APIs.Dtos;
using MoniteBackendService.Infrastructure.Models;

namespace MoniteBackendService.APIs.Extensions;

public static class ProductsExtensions
{
    public static Product ToDto(this ProductDbModel model)
    {
        return new Product
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Invoice = model.InvoiceId,
            Price = model.Price,
            ProductName = model.ProductName,
            Quantity = model.Quantity,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static ProductDbModel ToModel(
        this ProductUpdateInput updateDto,
        ProductWhereUniqueInput uniqueId
    )
    {
        var product = new ProductDbModel
        {
            Id = uniqueId.Id,
            Price = updateDto.Price,
            ProductName = updateDto.ProductName,
            Quantity = updateDto.Quantity
        };

        if (updateDto.CreatedAt != null)
        {
            product.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Invoice != null)
        {
            product.InvoiceId = updateDto.Invoice;
        }
        if (updateDto.UpdatedAt != null)
        {
            product.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return product;
    }
}
