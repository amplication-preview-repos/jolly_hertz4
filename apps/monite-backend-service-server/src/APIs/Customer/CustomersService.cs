using MoniteBackendService.Infrastructure;

namespace MoniteBackendService.APIs;

public class CustomersService : CustomersServiceBase
{
    public CustomersService(MoniteBackendServiceDbContext context)
        : base(context) { }
}
