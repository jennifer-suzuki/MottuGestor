using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MottuGestor.Domain.Entities;
using MottuGestor.Infrastructure.Data;

namespace MottuGestor.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly MongoDbContext _ctx;
    public UsuarioRepository(MongoDbContext ctx) => _ctx = ctx;

    public async Task<Usuario?> GetByIdAsync(string id) =>
        await _ctx.Usuarios.Find(u => u.Id == id).FirstOrDefaultAsync();

    public async Task<List<Usuario>> ListAsync() =>
        await _ctx.Usuarios.Find(_ => true).ToListAsync();

    public async Task AddAsync(Usuario usuario) =>
        await _ctx.Usuarios.InsertOneAsync(usuario);

    public async Task UpdateAsync(Usuario usuario) =>
        await _ctx.Usuarios.ReplaceOneAsync(u => u.Id == usuario.Id, usuario);

    public async Task DeleteAsync(string id) =>
        await _ctx.Usuarios.DeleteOneAsync(u => u.Id == id);
}