using Moq;
using MottuGestor.Application.DTOs;
using MottuGestor.Application.Services;
using MottuGestor.Domain.Entities;
using MottuGestor.Infrastructure.Repositories;

namespace MottuGestor.Tests;

public class UsuarioTests
{
    [Fact]
    public async Task CreateAsync_ReturnUsuario()
    {
        var mock = new Mock<IUsuarioRepository>();
        mock.Setup(r => r.AddAsync(It.IsAny<Usuario>()))
            .Callback<Usuario>(m => m.GetType().GetProperty("Id")!.SetValue(m, "123"));
        var service = new UsuarioService(mock.Object);
        var dto = new UsuarioDto { Nome = "Jennifer", Email = "jennifer@gmail.com" };
        var result = await service.CreateAsync(dto);
        
        Assert.Equal("Jennifer", result.Nome);
        Assert.Equal("jennifer@gmail.com", result.Email);
        Assert.Equal("123", result.Id);
    }
}