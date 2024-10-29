using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoniteBackendService.APIs;
using MoniteBackendService.APIs.Common;
using MoniteBackendService.APIs.Dtos;
using MoniteBackendService.APIs.Errors;

namespace MoniteBackendService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CustomersControllerBase : ControllerBase
{
    protected readonly ICustomersService _service;

    public CustomersControllerBase(ICustomersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Customer
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Customer>> CreateCustomer(CustomerCreateInput input)
    {
        var customer = await _service.CreateCustomer(input);

        return CreatedAtAction(nameof(Customer), new { id = customer.Id }, customer);
    }

    /// <summary>
    /// Delete one Customer
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteCustomer([FromRoute()] CustomerWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteCustomer(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Customers
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Customer>>> Customers(
        [FromQuery()] CustomerFindManyArgs filter
    )
    {
        return Ok(await _service.Customers(filter));
    }

    /// <summary>
    /// Meta data about Customer records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> CustomersMeta(
        [FromQuery()] CustomerFindManyArgs filter
    )
    {
        return Ok(await _service.CustomersMeta(filter));
    }

    /// <summary>
    /// Get one Customer
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Customer>> Customer(
        [FromRoute()] CustomerWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.Customer(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Customer
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateCustomer(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] CustomerUpdateInput customerUpdateDto
    )
    {
        try
        {
            await _service.UpdateCustomer(uniqueId, customerUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Invoices records to Customer
    /// </summary>
    [HttpPost("{Id}/invoices")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectInvoices(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] InvoiceWhereUniqueInput[] invoicesId
    )
    {
        try
        {
            await _service.ConnectInvoices(uniqueId, invoicesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Invoices records from Customer
    /// </summary>
    [HttpDelete("{Id}/invoices")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectInvoices(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] InvoiceWhereUniqueInput[] invoicesId
    )
    {
        try
        {
            await _service.DisconnectInvoices(uniqueId, invoicesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Invoices records for Customer
    /// </summary>
    [HttpGet("{Id}/invoices")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Invoice>>> FindInvoices(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromQuery()] InvoiceFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindInvoices(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Invoices records for Customer
    /// </summary>
    [HttpPatch("{Id}/invoices")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateInvoices(
        [FromRoute()] CustomerWhereUniqueInput uniqueId,
        [FromBody()] InvoiceWhereUniqueInput[] invoicesId
    )
    {
        try
        {
            await _service.UpdateInvoices(uniqueId, invoicesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
