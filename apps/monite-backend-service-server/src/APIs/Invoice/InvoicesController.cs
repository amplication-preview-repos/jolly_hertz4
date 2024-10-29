using Microsoft.AspNetCore.Mvc;

namespace MoniteBackendService.APIs;

[ApiController()]
public class InvoicesController : InvoicesControllerBase
{
    public InvoicesController(IInvoicesService service)
        : base(service) { }
}
