using MottuGestor.Application.DTOs;

namespace MottuGestor.Application.Interfaces;

public interface IPatioService
{
    Task<List<PatioDto>> ListAsync();
    Task<PatioDto?> GetByIdAsync(string id);
    Task<PatioDto> CreateAsync(PatioDto dto);
    Task<bool> UpdateAsync(string id, PatioDto dto);
    Task<bool> DeleteAsync(string id);
}