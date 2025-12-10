using Cscore.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cscore.API.Controllers;

[ApiController]
[Route("public/championships")]
public class PublicChampionshipsController : ControllerBase
{
    private readonly IChampionshipService _championshipService;

    public PublicChampionshipsController(IChampionshipService championshipService)
    {
        _championshipService = championshipService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var championships = await _championshipService.GetAllAsync(page, pageSize);
        return Ok(championships);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var championship = await _championshipService.GetByIdAsync(id);
        if (championship == null)
            return NotFound();

        return Ok(championship);
    }
}
