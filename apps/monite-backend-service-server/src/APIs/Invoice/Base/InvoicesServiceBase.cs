using Microsoft.EntityFrameworkCore;
using MoniteBackendService.APIs;
using MoniteBackendService.APIs.Common;
using MoniteBackendService.APIs.Dtos;
using MoniteBackendService.APIs.Errors;
using MoniteBackendService.APIs.Extensions;
using MoniteBackendService.Infrastructure;
using MoniteBackendService.Infrastructure.Models;

namespace MoniteBackendService.APIs;

public abstract class InvoicesServiceBase : IInvoicesService
{
    protected readonly MoniteBackendServiceDbContext _context;

    public InvoicesServiceBase(MoniteBackendServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Invoice
    /// </summary>
    public async Task<Invoice> CreateInvoice(InvoiceCreateInput createDto)
    {
        var invoice = new InvoiceDbModel
        {
            Amount = createDto.Amount,
            CreatedAt = createDto.CreatedAt,
            DueDate = createDto.DueDate,
            InvoiceNumber = createDto.InvoiceNumber,
            IssueDate = createDto.IssueDate,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            invoice.Id = createDto.Id;
        }
        if (createDto.Customer != null)
        {
            invoice.Customer = await _context
                .Customers.Where(customer => createDto.Customer.Id == customer.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.Payments != null)
        {
            invoice.Payments = await _context
                .Payments.Where(payment =>
                    createDto.Payments.Select(t => t.Id).Contains(payment.Id)
                )
                .ToListAsync();
        }

        if (createDto.Products != null)
        {
            invoice.Products = await _context
                .Products.Where(product =>
                    createDto.Products.Select(t => t.Id).Contains(product.Id)
                )
                .ToListAsync();
        }

        _context.Invoices.Add(invoice);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<InvoiceDbModel>(invoice.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Invoice
    /// </summary>
    public async Task DeleteInvoice(InvoiceWhereUniqueInput uniqueId)
    {
        var invoice = await _context.Invoices.FindAsync(uniqueId.Id);
        if (invoice == null)
        {
            throw new NotFoundException();
        }

        _context.Invoices.Remove(invoice);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Invoices
    /// </summary>
    public async Task<List<Invoice>> Invoices(InvoiceFindManyArgs findManyArgs)
    {
        var invoices = await _context
            .Invoices.Include(x => x.Customer)
            .Include(x => x.Payments)
            .Include(x => x.Products)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return invoices.ConvertAll(invoice => invoice.ToDto());
    }

    /// <summary>
    /// Meta data about Invoice records
    /// </summary>
    public async Task<MetadataDto> InvoicesMeta(InvoiceFindManyArgs findManyArgs)
    {
        var count = await _context.Invoices.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Invoice
    /// </summary>
    public async Task<Invoice> Invoice(InvoiceWhereUniqueInput uniqueId)
    {
        var invoices = await this.Invoices(
            new InvoiceFindManyArgs { Where = new InvoiceWhereInput { Id = uniqueId.Id } }
        );
        var invoice = invoices.FirstOrDefault();
        if (invoice == null)
        {
            throw new NotFoundException();
        }

        return invoice;
    }

    /// <summary>
    /// Update one Invoice
    /// </summary>
    public async Task UpdateInvoice(InvoiceWhereUniqueInput uniqueId, InvoiceUpdateInput updateDto)
    {
        var invoice = updateDto.ToModel(uniqueId);

        if (updateDto.Customer != null)
        {
            invoice.Customer = await _context
                .Customers.Where(customer => updateDto.Customer == customer.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.Payments != null)
        {
            invoice.Payments = await _context
                .Payments.Where(payment => updateDto.Payments.Select(t => t).Contains(payment.Id))
                .ToListAsync();
        }

        if (updateDto.Products != null)
        {
            invoice.Products = await _context
                .Products.Where(product => updateDto.Products.Select(t => t).Contains(product.Id))
                .ToListAsync();
        }

        _context.Entry(invoice).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Invoices.Any(e => e.Id == invoice.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Get a Customer record for Invoice
    /// </summary>
    public async Task<Customer> GetCustomer(InvoiceWhereUniqueInput uniqueId)
    {
        var invoice = await _context
            .Invoices.Where(invoice => invoice.Id == uniqueId.Id)
            .Include(invoice => invoice.Customer)
            .FirstOrDefaultAsync();
        if (invoice == null)
        {
            throw new NotFoundException();
        }
        return invoice.Customer.ToDto();
    }

    /// <summary>
    /// Connect multiple Payments records to Invoice
    /// </summary>
    public async Task ConnectPayments(
        InvoiceWhereUniqueInput uniqueId,
        PaymentWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Invoices.Include(x => x.Payments)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Payments.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Payments);

        foreach (var child in childrenToConnect)
        {
            parent.Payments.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Payments records from Invoice
    /// </summary>
    public async Task DisconnectPayments(
        InvoiceWhereUniqueInput uniqueId,
        PaymentWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Invoices.Include(x => x.Payments)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Payments.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Payments?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Payments records for Invoice
    /// </summary>
    public async Task<List<Payment>> FindPayments(
        InvoiceWhereUniqueInput uniqueId,
        PaymentFindManyArgs invoiceFindManyArgs
    )
    {
        var payments = await _context
            .Payments.Where(m => m.InvoiceId == uniqueId.Id)
            .ApplyWhere(invoiceFindManyArgs.Where)
            .ApplySkip(invoiceFindManyArgs.Skip)
            .ApplyTake(invoiceFindManyArgs.Take)
            .ApplyOrderBy(invoiceFindManyArgs.SortBy)
            .ToListAsync();

        return payments.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Payments records for Invoice
    /// </summary>
    public async Task UpdatePayments(
        InvoiceWhereUniqueInput uniqueId,
        PaymentWhereUniqueInput[] childrenIds
    )
    {
        var invoice = await _context
            .Invoices.Include(t => t.Payments)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (invoice == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Payments.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        invoice.Payments = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Connect multiple Products records to Invoice
    /// </summary>
    public async Task ConnectProducts(
        InvoiceWhereUniqueInput uniqueId,
        ProductWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Invoices.Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Products.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Products);

        foreach (var child in childrenToConnect)
        {
            parent.Products.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Products records from Invoice
    /// </summary>
    public async Task DisconnectProducts(
        InvoiceWhereUniqueInput uniqueId,
        ProductWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Invoices.Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Products.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Products?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Products records for Invoice
    /// </summary>
    public async Task<List<Product>> FindProducts(
        InvoiceWhereUniqueInput uniqueId,
        ProductFindManyArgs invoiceFindManyArgs
    )
    {
        var products = await _context
            .Products.Where(m => m.InvoiceId == uniqueId.Id)
            .ApplyWhere(invoiceFindManyArgs.Where)
            .ApplySkip(invoiceFindManyArgs.Skip)
            .ApplyTake(invoiceFindManyArgs.Take)
            .ApplyOrderBy(invoiceFindManyArgs.SortBy)
            .ToListAsync();

        return products.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Products records for Invoice
    /// </summary>
    public async Task UpdateProducts(
        InvoiceWhereUniqueInput uniqueId,
        ProductWhereUniqueInput[] childrenIds
    )
    {
        var invoice = await _context
            .Invoices.Include(t => t.Products)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (invoice == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Products.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        invoice.Products = children;
        await _context.SaveChangesAsync();
    }
}
