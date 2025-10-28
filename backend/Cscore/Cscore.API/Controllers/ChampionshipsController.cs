using Cscore.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cscore.API.Controllers;

[Route("championships")]
public class ChampionshipsController : ControllerBase
{
    private readonly ChampionshipRepository _repository;

    public ChampionshipsController(ChampionshipRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _repository.GetAllAsync());
    }
}