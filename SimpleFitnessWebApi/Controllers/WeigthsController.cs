using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleFitnessWebApi.Data;
using SimpleFitnessWebApi.Data.Dtos;
using SimpleFitnessWebApi.Models;

namespace SimpleFitnessWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeightsController : ControllerBase
{
    private readonly AppDbContext _context;

    public WeightsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Weight>>> GetWeights()
    {
        var weights = await _context.Weights.ToListAsync();
        return Ok(weights);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Weight>> GetWeight(int id)
    {
        var weight = await _context.Weights.FindAsync(id);
        if (weight == null)
        {
            return NotFound();
        }
        return Ok(weight);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditWeight(int id, EditWeightDto weightDto)
    {
        if (id != weightDto.Id)
        {
            return BadRequest();
        }
        
        var weight = await _context.Weights.FindAsync(id);
        if (weight == null)
        {
            return NotFound();
        }
       
        weight.Date = weightDto.Date.Value;
        weight.Value = weightDto.Value.Value;
    
        await _context.SaveChangesAsync();
        
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Weight>> CreateWeight(CreateWeightDto weightDto)
    {
        var weight = new Weight()
        {
            Date = weightDto.Date.Value,
            Value = weightDto.Value.Value,
        };
        
        _context.Weights.Add(weight);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetWeight), new { id = weight.Id }, weight);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Weight>> DeleteWeight(int id)
    {
        var weight = await _context.Weights.FindAsync(id);
        if (weight == null)
        {
            return NotFound();
        }
        _context.Weights.Remove(weight);
        await _context.SaveChangesAsync();
        
        return Ok();
    }
    
}