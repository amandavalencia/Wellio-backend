using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Wellio.DTO;
using Wellio.Services.IServices;

namespace Wellio.Controllers;

[Authorize]
[ApiController]
[Route("api/sleep")]
public class SleepController : ControllerBase
{
    private readonly ISleepService service;

    public SleepController(ISleepService service)
    {
        this.service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<SleepDto>>> GetAll()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return Unauthorized();
        }

        var entries = await service.GetAllAsync(userId);
        return Ok(entries);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<SleepDto>> GetById(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return Unauthorized();
        }

        var entry = await service.GetByIdAsync(id, userId);
        if (entry == null)
        {
            return NotFound();
        }

        return Ok(entry);
    }

    [HttpPost]
    public async Task<ActionResult<SleepDto>> Create(CreateSleepDto request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return Unauthorized();
        }

        var entry = await service.CreateAsync(request, userId);
        return CreatedAtAction(nameof(GetById), new { id = entry.Id }, entry);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateSleepDto request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return Unauthorized();
        }

        var updated = await service.UpdateAsync(id, request, userId);
        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return Unauthorized();
        }

        var deleted = await service.DeleteAsync(id, userId);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
