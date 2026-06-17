using TechMovePOE.API.Models;

namespace TechMovePOE.API.Repositories;

public interface IContractRepository
{
    Task<IEnumerable<Contract>> GetAllAsync();
    Task<Contract?> GetByIdAsync(int id);
    Task AddAsync(Contract contract);
    Task UpdateAsync(Contract contract);
    Task DeleteAsync(int id);
}