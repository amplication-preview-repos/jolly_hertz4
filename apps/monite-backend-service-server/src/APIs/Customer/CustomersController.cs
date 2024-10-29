using Microsoft.AspNetCore.Mvc;

namespace MoniteBackendService.APIs;

[ApiController()]
public class CustomersController : CustomersControllerBase
{
    public CustomersController(ICustomersService service)
        : base(service) { }
}
