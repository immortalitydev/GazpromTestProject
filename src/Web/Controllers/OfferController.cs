using Application.Offers;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/offers/[action]")]
public sealed class OfferController : ControllerBase
{
    private readonly IOfferService _offerService;

    public OfferController(IOfferService offerService)
    {
        _offerService = offerService;
    }

    [HttpGet]
    public async Task<ActionResult<OfferSearchResponse>> SearchAsync(
        [FromQuery] OfferSearchRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _offerService.SearchAsync(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<OfferDto>> CreateAsync(
        [FromBody] CreateOfferRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var created = await _offerService.CreateAsync(request, cancellationToken);
            return Ok(created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }
}
