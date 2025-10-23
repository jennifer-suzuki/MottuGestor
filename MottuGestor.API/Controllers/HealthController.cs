using Microsoft.AspNetCore.Mvc;
using MottuGestor.Infrastructure.Data;

namespace GestMottu.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    private readonly MongoDbContext _mongoDb;

    public HealthController(MongoDbContext mongoDb)
    {
        _mongoDb = mongoDb;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            await _mongoDb.Motos.EstimatedDocumentCountAsync();
            return Ok(new { status = "Healthy", mongo = "Connected" });
        }
        catch
        {
            return StatusCode(500, new { status = "Unhealthy", mongo = "Disconnected" });
        }
    }
}