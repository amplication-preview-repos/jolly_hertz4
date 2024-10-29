using MoniteBackendService.Infrastructure;

namespace MoniteBackendService.APIs;

public class ProductsService : ProductsServiceBase
{
    public ProductsService(MoniteBackendServiceDbContext context)
        : base(context) { }
}
