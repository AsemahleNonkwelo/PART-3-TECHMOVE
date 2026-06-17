using Microsoft.EntityFrameworkCore;
using TechMovePOE.API.Data;
using TechMovePOE.API.Models;

namespace TechMovePOE.API.Repositories;

public class ServiceRequestRepository : IServiceRequestRepository
{
    private readonly AppDbContext _context;

    public ServiceRequestRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ServiceRequest>> GetAllAsync()
    {
        return await _context.ServiceRequests.ToListAsync();
    }

    public async Task<ServiceRequest?> GetByIdAsync(int id)
    {
        return await _context.ServiceRequests.FindAsync(id);
    }

    public async Task AddAsync(ServiceRequest request)
    {
        _context.ServiceRequests.Add(request);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ServiceRequest request)
    {
        _context.ServiceRequests.Update(request);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var request = await _context.ServiceRequests.FindAsync(id);

        if (request != null)
        {
            _context.ServiceRequests.Remove(request);
            await _context.SaveChangesAsync();
        }
    }
}