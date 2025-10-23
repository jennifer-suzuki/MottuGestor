using System.ComponentModel.DataAnnotations;

namespace MottuGestor.Application.DTOs;

public class UsuarioDto
{
    public string Id { get; set; } = string.Empty;

    [Required]
    public string Nome { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    public List<NavigationDto> Links { get; set; } = new();
}