using Microsoft.AspNetCore.Mvc;
using Vagabond.API.Exceptions;
using Vagabond.API.Interfaces;
using Vagabond.API.Models;

[Route("api/[controller]")]
[ApiController]
public class DestinationsController : ControllerBase
{
    private readonly IDestinationRepository _repo;

    public DestinationsController(IDestinationRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _repo.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var dest = await _repo.GetByIdAsync(id);

        if (dest == null)
            throw new DestinationNotFoundException("Destination not found");

        return Ok(dest);
    }

    [HttpPost]
    public async Task<IActionResult> Add(Destination destination)
    {
        // Ensure we don't attempt to insert an explicit identity value
        destination.Id = 0;
        await _repo.AddAsync(destination);

        // EF will populate destination.Id after SaveChangesAsync
        return CreatedAtAction(nameof(GetById), new { id = destination.Id }, destination);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Destination destination)
    {
        if (id != destination.Id)
            return BadRequest();

        await _repo.UpdateAsync(destination);
        return Ok(destination);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repo.DeleteAsync(id);
        return Ok("Deleted");
    }
}
