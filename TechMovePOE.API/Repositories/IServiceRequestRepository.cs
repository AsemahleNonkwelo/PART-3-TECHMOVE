using TechMovePOE.API.Models;

namespace TechMovePOE.API.Repositories;

public interface IServiceRequestRepository
{
    Task<IEnumerable<ServiceRequest>> GetAllAsync();
    Task<ServiceRequest?> GetByIdAsync(int id);
    Task AddAsync(ServiceRequest request);
    Task UpdateAsync(ServiceRequest request);
    Task DeleteAsync(int id);
}