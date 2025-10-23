using MottuGestor.Domain.Entities;

namespace MottuGestor.Infrastructure.Repositories;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByIdAsync(string id);
    Task<List<Usuario>> ListAsync();
    Task AddAsync(Usuario usuario);
    Task UpdateAsync(Usuario usuario);
    Task DeleteAsync(string id);
}