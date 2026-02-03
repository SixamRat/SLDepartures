using Microsoft.AspNetCore.Mvc;
using SLDepartures.Services;

namespace SLDepartures.Controllers;

public class DeparturesController : Controller
{
    private readonly SLService _slService;

    public DeparturesController(SLService slService)
    {
        _slService = slService;
    }

    public async Task<IActionResult> Index()
    {
        var departures = await _slService.GetDepartures();
        return View(departures);
    }
}