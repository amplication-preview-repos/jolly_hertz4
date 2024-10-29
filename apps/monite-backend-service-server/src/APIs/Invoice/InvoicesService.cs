using MoniteBackendService.Infrastructure;

namespace MoniteBackendService.APIs;

public class InvoicesService : InvoicesServiceBase
{
    public InvoicesService(MoniteBackendServiceDbContext context)
        : base(context) { }
}
