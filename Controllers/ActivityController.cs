using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Wellio.DTO;
using Wellio.Services.IServices;

namespace Wellio.Controllers;

[Authorize]
[ApiController]
[Route("api/activity")]
public class ActivityController : ControllerBase
{
    private readonly IActivityService service;

    public ActivityController(IActivityService service)
    {
        this.service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<ActivityDto>>> GetAll()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return Unauthorized();
        }

        var activities = await service.GetAllAsync(userId);
        return Ok(activities);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ActivityDto>> GetById(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return Unauthorized();
        }

        var activity = await service.GetByIdAsync(id, userId);
        if (activity == null)
        {
            return NotFound();
        }

        return Ok(activity);
    }

    [HttpPost]
    public async Task<ActionResult<ActivityDto>> Create(CreateActivityDto newActivity)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return Unauthorized();
        }

        var activity = await service.CreateAsync(newActivity, userId);
        //CreatedAtAction ger: 201, resursen skapades, och en länk till resursen
        return CreatedAtAction(nameof(GetById), new { id = activity.Id }, activity);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateActivityDto request)
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
