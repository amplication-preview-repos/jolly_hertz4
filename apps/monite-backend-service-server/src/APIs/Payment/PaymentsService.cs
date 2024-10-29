using MoniteBackendService.Infrastructure;

namespace MoniteBackendService.APIs;

public class PaymentsService : PaymentsServiceBase
{
    public PaymentsService(MoniteBackendServiceDbContext context)
        : base(context) { }
}
