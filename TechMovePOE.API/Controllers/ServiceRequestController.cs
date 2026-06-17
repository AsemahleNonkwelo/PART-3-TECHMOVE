using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechMovePOE.API.Data;
using TechMovePOE.API.Models;

namespace TechMovePOE.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceRequestController : ControllerBase
{
    private readonly AppDbContext _context;

    public ServiceRequestController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceRequest>>> GetServiceRequests()
    {
        return await _context.ServiceRequests
            .Include(s => s.Contract)
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceRequest>> GetServiceRequest(int id)
    {
        var request = await _context.ServiceRequests
            .Include(s => s.Contract)
            .FirstOrDefaultAsync(s => s.ServiceRequestId == id);

        if (request == null)
            return NotFound();

        return request;
    }

    [HttpPost]
    public async Task<ActionResult<ServiceRequest>> CreateServiceRequest(ServiceRequest request)
    {
        _context.ServiceRequests.Add(request);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetServiceRequest),
            new { id = request.ServiceRequestId }, request);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateServiceRequest(int id, ServiceRequest request)
    {
        if (id != request.ServiceRequestId)
            return BadRequest();

        _context.Entry(request).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteServiceRequest(int id)
    {
        var request = await _context.ServiceRequests.FindAsync(id);

        if (request == null)
            return NotFound();

        _context.ServiceRequests.Remove(request);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}