using MottuGestor.Domain.Entities;

namespace MottuGestor.Infrastructure.Repositories;

public interface IPatioRepository
{
    Task<Patio?> GetByIdAsync(string id);
    Task<List<Patio>> ListAsync();
    Task AddAsync(Patio patio);
    Task UpdateAsync(Patio patio);
    Task DeleteAsync(string id);
}