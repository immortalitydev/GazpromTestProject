using Application.Suppliers;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/suppliers/[action]")]
public sealed class SupplierController : ControllerBase
{
    private readonly ISupplierService _supplierService;

    public SupplierController(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    [HttpGet("top")]
    public async Task<ActionResult<IReadOnlyList<SupplierDto>>> GetTopSuppliers(
        [FromQuery] int count,
        CancellationToken cancellationToken)
    {
        var normalizedCount = count <= 0 ? 3 : count;
        var suppliers = await _supplierService.GetTopSuppliersAsync(normalizedCount, cancellationToken);
        return Ok(suppliers);
    }
}
