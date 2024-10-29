using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoniteBackendService.APIs;
using MoniteBackendService.APIs.Common;
using MoniteBackendService.APIs.Dtos;
using MoniteBackendService.APIs.Errors;

namespace MoniteBackendService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class InvoicesControllerBase : ControllerBase
{
    protected readonly IInvoicesService _service;

    public InvoicesControllerBase(IInvoicesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Invoice
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Invoice>> CreateInvoice(InvoiceCreateInput input)
    {
        var invoice = await _service.CreateInvoice(input);

        return CreatedAtAction(nameof(Invoice), new { id = invoice.Id }, invoice);
    }

    /// <summary>
    /// Delete one Invoice
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteInvoice([FromRoute()] InvoiceWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteInvoice(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Invoices
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Invoice>>> Invoices(
        [FromQuery()] InvoiceFindManyArgs filter
    )
    {
        return Ok(await _service.Invoices(filter));
    }

    /// <summary>
    /// Meta data about Invoice records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> InvoicesMeta(
        [FromQuery()] InvoiceFindManyArgs filter
    )
    {
        return Ok(await _service.InvoicesMeta(filter));
    }

    /// <summary>
    /// Get one Invoice
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Invoice>> Invoice([FromRoute()] InvoiceWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Invoice(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Invoice
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateInvoice(
        [FromRoute()] InvoiceWhereUniqueInput uniqueId,
        [FromQuery()] InvoiceUpdateInput invoiceUpdateDto
    )
    {
        try
        {
            await _service.UpdateInvoice(uniqueId, invoiceUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Customer record for Invoice
    /// </summary>
    [HttpGet("{Id}/customer")]
    public async Task<ActionResult<List<Customer>>> GetCustomer(
        [FromRoute()] InvoiceWhereUniqueInput uniqueId
    )
    {
        var customer = await _service.GetCustomer(uniqueId);
        return Ok(customer);
    }

    /// <summary>
    /// Connect multiple Payments records to Invoice
    /// </summary>
    [HttpPost("{Id}/payments")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectPayments(
        [FromRoute()] InvoiceWhereUniqueInput uniqueId,
        [FromQuery()] PaymentWhereUniqueInput[] paymentsId
    )
    {
        try
        {
            await _service.ConnectPayments(uniqueId, paymentsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Payments records from Invoice
    /// </summary>
    [HttpDelete("{Id}/payments")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectPayments(
        [FromRoute()] InvoiceWhereUniqueInput uniqueId,
        [FromBody()] PaymentWhereUniqueInput[] paymentsId
    )
    {
        try
        {
            await _service.DisconnectPayments(uniqueId, paymentsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Payments records for Invoice
    /// </summary>
    [HttpGet("{Id}/payments")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Payment>>> FindPayments(
        [FromRoute()] InvoiceWhereUniqueInput uniqueId,
        [FromQuery()] PaymentFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindPayments(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Payments records for Invoice
    /// </summary>
    [HttpPatch("{Id}/payments")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdatePayments(
        [FromRoute()] InvoiceWhereUniqueInput uniqueId,
        [FromBody()] PaymentWhereUniqueInput[] paymentsId
    )
    {
        try
        {
            await _service.UpdatePayments(uniqueId, paymentsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Products records to Invoice
    /// </summary>
    [HttpPost("{Id}/products")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> ConnectProducts(
        [FromRoute()] InvoiceWhereUniqueInput uniqueId,
        [FromQuery()] ProductWhereUniqueInput[] productsId
    )
    {
        try
        {
            await _service.ConnectProducts(uniqueId, productsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Products records from Invoice
    /// </summary>
    [HttpDelete("{Id}/products")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DisconnectProducts(
        [FromRoute()] InvoiceWhereUniqueInput uniqueId,
        [FromBody()] ProductWhereUniqueInput[] productsId
    )
    {
        try
        {
            await _service.DisconnectProducts(uniqueId, productsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Products records for Invoice
    /// </summary>
    [HttpGet("{Id}/products")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Product>>> FindProducts(
        [FromRoute()] InvoiceWhereUniqueInput uniqueId,
        [FromQuery()] ProductFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindProducts(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Products records for Invoice
    /// </summary>
    [HttpPatch("{Id}/products")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateProducts(
        [FromRoute()] InvoiceWhereUniqueInput uniqueId,
        [FromBody()] ProductWhereUniqueInput[] productsId
    )
    {
        try
        {
            await _service.UpdateProducts(uniqueId, productsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
