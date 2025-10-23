using System.ComponentModel.DataAnnotations;
using MottuGestor.Domain.Enums;

namespace MottuGestor.Application.DTOs;

public class MotoDto
{
    public string Id { get; set; } = string.Empty;

    [Required]
    public string Rfid { get; set; } = string.Empty;
    
    [Required]
    public string Placa { get; set; } = string.Empty;

    [Required]
    [EnumDataType(typeof(StatusMoto), ErrorMessage = "Status válidos: Disponivel, EmUso, EmManutencao, Inativa")]
    public StatusMoto Status { get; set; }
    
    [Required]
    public string UsuarioId { get; set; } = string.Empty;
    
    public List<NavigationDto> Links { get; set; } = new();
}