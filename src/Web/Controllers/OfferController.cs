using Application.Offers;
using Application.Offers.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/offers/[action]")]
public sealed class OfferController(IOfferService offerService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<OfferSearchResponse>> SearchAsync(
        [FromQuery] OfferSearchRequest request,
        CancellationToken cancellationToken)
    {
        var response = await offerService.SearchAsync(request, cancellationToken);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<OfferDto>> CreateAsync(
        [FromBody] CreateOfferRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var created = await offerService.CreateAsync(request, cancellationToken);
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
