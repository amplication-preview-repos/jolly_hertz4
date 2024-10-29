using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoniteBackendService.Infrastructure.Models;

namespace MoniteBackendService.Infrastructure;

public class MoniteBackendServiceDbContext : IdentityDbContext<IdentityUser>
{
    public MoniteBackendServiceDbContext(DbContextOptions<MoniteBackendServiceDbContext> options)
        : base(options) { }

    public DbSet<InvoiceDbModel> Invoices { get; set; }

    public DbSet<CustomerDbModel> Customers { get; set; }

    public DbSet<PaymentDbModel> Payments { get; set; }

    public DbSet<ProductDbModel> Products { get; set; }
}
