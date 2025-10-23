using MottuGestor.Application.DTOs;
using MottuGestor.Application.Interfaces;
using MottuGestor.Domain.Entities;
using MottuGestor.Infrastructure.Repositories;

namespace MottuGestor.Application.Services;

public class PatioService : IPatioService
{
    private readonly IPatioRepository _repo;
    public PatioService(IPatioRepository repo) => _repo = repo;

    public async Task<List<PatioDto>> ListAsync()
    {
        var list = await _repo.ListAsync();
        return list.Select(p => new PatioDto()
        {
            Id = p.Id,
            Endereco = p.Endereco,
            UsuarioId = p.UsuarioId,
            MotoId = p.MotoId
        }).ToList();
    }

    public async Task<PatioDto?> GetByIdAsync(string id)
    {
        var p = await _repo.GetByIdAsync(id);
        if (p is null) return null;
        return new PatioDto
        {
            Id = p.Id,
            Endereco = p.Endereco,
            UsuarioId = p.UsuarioId,
            MotoId = p.MotoId
        };
    }

    public async Task<PatioDto> CreateAsync(PatioDto dto)
    {
        var entity = new Patio(dto.Endereco, dto.UsuarioId, dto.MotoId);
        await _repo.AddAsync(entity);
        dto.Id = entity.Id;
        return dto;
    }

    public async Task<bool> UpdateAsync(string id, PatioDto dto)
    {
        var p = await _repo.GetByIdAsync(id);
        if (p is null) return false;
        await _repo.UpdateAsync(p);
        return true;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        await _repo.DeleteAsync(id);
        return true;
    }
}