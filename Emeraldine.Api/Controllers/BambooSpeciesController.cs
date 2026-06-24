using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Emeraldine.Infrastructure.Persistence;
using Emeraldine.Domain.Entities;

namespace Emeraldine.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BambooSpeciesController : ControllerBase
{
    private readonly BambooEnterpriseDbContext _context;

    public BambooSpeciesController(BambooEnterpriseDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BambooSpecies>>> GetAll()
    {
        var species = await _context.BambooSpecies
            .Include(s => s.TechnicalProfile)
            .Include(s => s.PlantVariants)
                .ThenInclude(v => v.ShippingProfile)
            .ToListAsync();

        return Ok(species);
    }
    [HttpPost]
    public async Task<ActionResult<BambooSpecies>> Create(BambooSpecies species)
    {
        _context.BambooSpecies.Add(species);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new { id = species.Id }, species);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<BambooSpecies>> GetById(Guid id)
    {
        var species = await _context.BambooSpecies
            .Include(s => s.TechnicalProfile)
            .Include(s => s.PlantVariants)
                .ThenInclude(v => v.ShippingProfile)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (species == null)
        {
            return NotFound();
        }

        return Ok(species);
    }
}