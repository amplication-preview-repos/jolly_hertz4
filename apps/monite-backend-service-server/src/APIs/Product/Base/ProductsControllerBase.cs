using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoniteBackendService.APIs;
using MoniteBackendService.APIs.Common;
using MoniteBackendService.APIs.Dtos;
using MoniteBackendService.APIs.Errors;

namespace MoniteBackendService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ProductsControllerBase : ControllerBase
{
    protected readonly IProductsService _service;

    public ProductsControllerBase(IProductsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Product
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Product>> CreateProduct(ProductCreateInput input)
    {
        var product = await _service.CreateProduct(input);

        return CreatedAtAction(nameof(Product), new { id = product.Id }, product);
    }

    /// <summary>
    /// Delete one Product
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> DeleteProduct([FromRoute()] ProductWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteProduct(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Products
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<List<Product>>> Products(
        [FromQuery()] ProductFindManyArgs filter
    )
    {
        return Ok(await _service.Products(filter));
    }

    /// <summary>
    /// Meta data about Product records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ProductsMeta(
        [FromQuery()] ProductFindManyArgs filter
    )
    {
        return Ok(await _service.ProductsMeta(filter));
    }

    /// <summary>
    /// Get one Product
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult<Product>> Product([FromRoute()] ProductWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Product(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Product
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "user")]
    public async Task<ActionResult> UpdateProduct(
        [FromRoute()] ProductWhereUniqueInput uniqueId,
        [FromQuery()] ProductUpdateInput productUpdateDto
    )
    {
        try
        {
            await _service.UpdateProduct(uniqueId, productUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Invoice record for Product
    /// </summary>
    [HttpGet("{Id}/invoice")]
    public async Task<ActionResult<List<Invoice>>> GetInvoice(
        [FromRoute()] ProductWhereUniqueInput uniqueId
    )
    {
        var invoice = await _service.GetInvoice(uniqueId);
        return Ok(invoice);
    }
}
