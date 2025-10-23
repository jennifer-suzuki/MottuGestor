using MottuGestor.Application.DTOs;
using MottuGestor.Application.Interfaces;
using MottuGestor.Domain.Entities;
using MottuGestor.Infrastructure.Repositories;

namespace MottuGestor.Application.Services;

public class MotoService : IMotoService
{
    private readonly IMotoRepository _repo;
    public MotoService(IMotoRepository repo) => _repo = repo;

    public async Task<List<MotoDto>> ListAsync()
    {
        var list = await _repo.ListAsync();
        return list.Select(m => new MotoDto { Id = m.Id, Rfid = m.Rfid, Placa = m.Placa, Status = m.Status, UsuarioId = m.UsuarioId }).ToList();
    }

    public async Task<MotoDto?> GetByIdAsync(string id)
    {
        var m = await _repo.GetByIdAsync(id);
        if (m is null) return null;
        return new MotoDto { Id = m.Id, Rfid = m.Rfid, Placa = m.Placa, Status = m.Status, UsuarioId = m.UsuarioId };
    }

    public async Task<MotoDto> CreateAsync(MotoDto dto)
    {
        var entity = new Moto(dto.Rfid, dto.Placa, dto.Status, dto.UsuarioId);
        await _repo.AddAsync(entity);
        dto.Id = entity.Id;
        return dto;
    }

    public async Task<bool> UpdateAsync(string id, MotoDto dto)
    {
        var m = await _repo.GetByIdAsync(id);
        if (m is null) return false;
        m.AtualizarRfid(dto.Rfid);
        m.AtualizarPlaca(dto.Placa);
        m.AtualizarStatus(dto.Status);
        await _repo.UpdateAsync(m);
        return true;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        await _repo.DeleteAsync(id);
        return true;
    }
}