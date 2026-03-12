using Application.Suppliers;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/suppliers/[action]")]
public sealed class SupplierController(ISupplierService supplierService) : ControllerBase
{
    [HttpGet("top")]
    public async Task<ActionResult<IReadOnlyList<SupplierDto>>> GetTopSuppliersAsync(
        [FromQuery] int count,
        CancellationToken cancellationToken)
    {
        var normalizedCount = count <= 0 ? 3 : count;
        var suppliers = await supplierService.GetTopSuppliersAsync(normalizedCount, cancellationToken);
        return Ok(suppliers);
    }
}
