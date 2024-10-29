using Microsoft.AspNetCore.Mvc;

namespace MoniteBackendService.APIs;

[ApiController()]
public class ProductsController : ProductsControllerBase
{
    public ProductsController(IProductsService service)
        : base(service) { }
}
