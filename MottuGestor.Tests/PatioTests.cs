using Moq;
using MottuGestor.Application.DTOs;
using MottuGestor.Application.Services;
using MottuGestor.Domain.Entities;
using MottuGestor.Infrastructure.Repositories;

namespace MottuGestor.Tests;

public class PatioTests
{
    [Fact]
    public async Task CreateAsync_ReturnPatio()
    {
        var mock = new Mock<IPatioRepository>();
        mock.Setup(r => r.AddAsync(It.IsAny<Patio>()))
            .Callback<Patio>(m => m.GetType().GetProperty("Id")!.SetValue(m, "123"));
        var service = new PatioService(mock.Object);
        var dto = new PatioDto { Endereco = "ABC1234", UsuarioId = "1", MotoId = "1234" };
        var result = await service.CreateAsync(dto);
        
        Assert.Equal("ABC1234", result.Endereco);
        Assert.Equal("1", result.UsuarioId);
        Assert.Equal("1234", result.MotoId);
        Assert.Equal("123", result.Id);
    }
}