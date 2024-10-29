using MoniteBackendService.APIs.Common;
using MoniteBackendService.APIs.Dtos;

namespace MoniteBackendService.APIs;

public interface ICustomersService
{
    /// <summary>
    /// Create one Customer
    /// </summary>
    public Task<Customer> CreateCustomer(CustomerCreateInput customer);

    /// <summary>
    /// Delete one Customer
    /// </summary>
    public Task DeleteCustomer(CustomerWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Customers
    /// </summary>
    public Task<List<Customer>> Customers(CustomerFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Customer records
    /// </summary>
    public Task<MetadataDto> CustomersMeta(CustomerFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Customer
    /// </summary>
    public Task<Customer> Customer(CustomerWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Customer
    /// </summary>
    public Task UpdateCustomer(CustomerWhereUniqueInput uniqueId, CustomerUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Invoices records to Customer
    /// </summary>
    public Task ConnectInvoices(
        CustomerWhereUniqueInput uniqueId,
        InvoiceWhereUniqueInput[] invoicesId
    );

    /// <summary>
    /// Disconnect multiple Invoices records from Customer
    /// </summary>
    public Task DisconnectInvoices(
        CustomerWhereUniqueInput uniqueId,
        InvoiceWhereUniqueInput[] invoicesId
    );

    /// <summary>
    /// Find multiple Invoices records for Customer
    /// </summary>
    public Task<List<Invoice>> FindInvoices(
        CustomerWhereUniqueInput uniqueId,
        InvoiceFindManyArgs InvoiceFindManyArgs
    );

    /// <summary>
    /// Update multiple Invoices records for Customer
    /// </summary>
    public Task UpdateInvoices(
        CustomerWhereUniqueInput uniqueId,
        InvoiceWhereUniqueInput[] invoicesId
    );
}
