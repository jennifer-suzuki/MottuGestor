using System.ComponentModel.DataAnnotations;

namespace MottuGestor.Application.DTOs;

public class PatioDto
{
    public string Id { get; set; } = string.Empty;

    [Required]
    public string Endereco { get; set; } = string.Empty;

    [Required]
    public string UsuarioId { get; set; } = string.Empty;

    [Required]
    public string MotoId { get; set; } = string.Empty;

    public List<NavigationDto> Links { get; set; } = new();
}