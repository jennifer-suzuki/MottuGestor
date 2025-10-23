using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MottuGestor.Domain.Entities;
using MottuGestor.Infrastructure.Data;

namespace MottuGestor.Infrastructure.Repositories;

public class PatioRepository : IPatioRepository
{
    private readonly MongoDbContext _ctx;
    public PatioRepository(MongoDbContext ctx) => _ctx = ctx;

    public async Task<Patio?> GetByIdAsync(string id) =>
        await _ctx.Patios.Find(p => p.Id == id).FirstOrDefaultAsync();

    public async Task<List<Patio>> ListAsync() =>
        await _ctx.Patios.Find(_ => true).ToListAsync();

    public async Task AddAsync(Patio patio) =>
        await _ctx.Patios.InsertOneAsync(patio);

    public async Task UpdateAsync(Patio patio) =>
        await _ctx.Patios.ReplaceOneAsync(p => p.Id == patio.Id, patio);

    public async Task DeleteAsync(string id) =>
        await _ctx.Patios.DeleteOneAsync(p => p.Id == id);
}
