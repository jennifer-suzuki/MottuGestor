using Moq;
using MottuGestor.Application.DTOs;
using MottuGestor.Application.Services;
using MottuGestor.Domain.Entities;
using MottuGestor.Domain.Enums;
using MottuGestor.Infrastructure.Repositories;

namespace MottuGestor.Tests;

public class MotoTests
{
    [Fact]
    public async Task CreateAsync_ReturnMoto()
    {
        var mock = new Mock<IMotoRepository>();
        mock.Setup(r => r.AddAsync(It.IsAny<Moto>()))
            .Callback<Moto>(m => m.GetType().GetProperty("Id")!.SetValue(m, "123"));
        var service = new MotoService(mock.Object);
        var dto = new MotoDto { Rfid = "1234", Placa = "ABC1234", Status = StatusMoto.Disponivel, UsuarioId = "1" };
        var result = await service.CreateAsync(dto);
        
        Assert.Equal("1234", result.Rfid);
        Assert.Equal("ABC1234", result.Placa);
        Assert.Equal(StatusMoto.Disponivel, result.Status);
        Assert.Equal("1", result.UsuarioId);
        Assert.Equal("123", result.Id);
    }
}