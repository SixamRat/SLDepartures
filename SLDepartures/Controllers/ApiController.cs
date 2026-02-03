using Microsoft.AspNetCore.Mvc;
using SLDepartures.Services;
using System.Runtime.InteropServices.Marshalling;

namespace SLDepartures.Controllers;

[Route("api")]

public class ApiController : Controller 
{
    private readonly SLService _slService;

    public ApiController(SLService slService)
    {
        _slService = slService;
    }

    [HttpGet("departures")]
    public async Task<IActionResult> GetDepartures(string siteId = "9116")
    {
        var departures = await _slService.GetDepartures(siteId);
        return Json(departures);
    }

}
