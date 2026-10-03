using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Wellio.DTO;
using Wellio.Services.IServices;

namespace Wellio.Controllers;

[Authorize]
[ApiController]
[Route("api/mood")]
public class MoodController : ControllerBase
{
    private readonly IMoodService service;

    public MoodController(IMoodService service)
    {
        this.service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<MoodDto>>> GetAll()
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
    public async Task<ActionResult<MoodDto>> GetById(int id)
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
    public async Task<ActionResult<MoodDto>> Create(CreateMoodDto request)
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
    public async Task<IActionResult> Update(int id, UpdateMoodDto request)
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
