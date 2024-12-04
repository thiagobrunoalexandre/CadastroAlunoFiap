using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class AlunoServiceTests
{
    private readonly Mock<IAlunoRepository> _repositoryMock;
    private readonly AlunoService _alunoService;

    public AlunoServiceTests()
    {
        _repositoryMock = new Mock<IAlunoRepository>();
        _alunoService = new AlunoService(_repositoryMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllAlunos()
    {
        // Arrange
        var alunos = new List<Aluno>
        {
            new Aluno { Id = 1, Nome = "João Silva", Usuario = "joao.silva" },
            new Aluno { Id = 2, Nome = "Maria Oliveira", Usuario = "maria.oliveira" }
        };
        _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(alunos);

        // Act
        var result = await _alunoService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("João Silva", result.First().Nome);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnAluno_WhenIdIsValid()
    {
        // Arrange
        var aluno = new Aluno { Id = 1, Nome = "João Silva", Usuario = "joao.silva" };
        _repositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(aluno);

        // Act
        var result = await _alunoService.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("João Silva", result?.Nome);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenIdIsInvalid()
    {
        // Arrange
        _repositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Aluno?)null);

        // Act
        var result = await _alunoService.GetByIdAsync(99);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowException_WhenUsuarioIsNotUnique()
    {
        // Arrange
        var aluno = new Aluno { Nome = "João Silva", Usuario = "joao.silva", Senha = "Senha@123" };
        _repositoryMock.Setup(repo => repo.GetByUsuarioAsync("joao.silva")).ReturnsAsync(aluno);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _alunoService.CreateAsync(aluno));
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowException_WhenPasswordIsWeak()
    {
        // Arrange
        var aluno = new Aluno { Nome = "João Silva", Usuario = "joao.silva", Senha = "123456" };
        _repositoryMock.Setup(repo => repo.GetByUsuarioAsync("joao.silva")).ReturnsAsync((Aluno?)null);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _alunoService.CreateAsync(aluno));
    }

    [Fact]
    public async Task CreateAsync_ShouldHashPasswordAndSaveAluno_WhenValid()
    {
        // Arrange
        var aluno = new Aluno { Nome = "João Silva", Usuario = "joao.silva", Senha = "Senha@123" };
        _repositoryMock.Setup(repo => repo.GetByUsuarioAsync("joao.silva")).ReturnsAsync((Aluno?)null);
        _repositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Aluno>())).ReturnsAsync(1);

        // Act
        var result = await _alunoService.CreateAsync(aluno);

        // Assert
        Assert.Equal(1, result);
        Assert.True(BCrypt.Net.BCrypt.Verify("Senha@123", aluno.Senha));
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowException_WhenPasswordIsWeak()
    {
        // Arrange
        var aluno = new Aluno { Id = 1, Nome = "João Silva", Usuario = "joao.silva", Senha = "123456" };

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _alunoService.UpdateAsync(aluno));
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateAluno_WhenValid()
    {
        // Arrange
        var aluno = new Aluno { Id = 1, Nome = "João Silva", Usuario = "joao.silva", Senha = "Senha@123" };
        _repositoryMock.Setup(repo => repo.UpdateAsync(aluno)).ReturnsAsync(1);

        // Act
        await _alunoService.UpdateAsync(aluno);

        // Assert
        _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Aluno>()), Times.Once);
    }

    [Fact]
    public async Task InactivateAsync_ShouldThrowException_WhenRepositoryFails()
    {
        // Arrange
        _repositoryMock.Setup(repo => repo.InactivateAsync(1)).ReturnsAsync(0);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _alunoService.InactivateAsync(1));
    }

    [Fact]
    public async Task InactivateAsync_ShouldInactivateAluno_WhenValid()
    {
        // Arrange
        _repositoryMock.Setup(repo => repo.InactivateAsync(1)).ReturnsAsync(1);

        // Act
        await _alunoService.InactivateAsync(1);

        // Assert
        _repositoryMock.Verify(repo => repo.InactivateAsync(1), Times.Once);
    }
}
