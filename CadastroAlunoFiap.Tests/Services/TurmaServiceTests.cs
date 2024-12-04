using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class TurmaServiceTests
{
    private readonly Mock<ITurmaRepository> _repositoryMock;
    private readonly TurmaService _turmaService;

    public TurmaServiceTests()
    {
        _repositoryMock = new Mock<ITurmaRepository>();
        _turmaService = new TurmaService(_repositoryMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllTurmas()
    {
        // Arrange
        var turmas = new List<Turma>
        {
            new Turma { Id = 1, Nome = "Turma A" },
            new Turma { Id = 2, Nome = "Turma B" }
        };
        _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(turmas);

        // Act
        var result = await _turmaService.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("Turma A", result.First().Nome);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnTurma_WhenIdIsValid()
    {
        // Arrange
        var turma = new Turma { Id = 1, Nome = "Turma A" };
        _repositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(turma);

        // Act
        var result = await _turmaService.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Turma A", result?.Nome);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenIdIsInvalid()
    {
        // Arrange
        _repositoryMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Turma?)null);

        // Act
        var result = await _turmaService.GetByIdAsync(99);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowException_WhenNomeIsNotUnique()
    {
        // Arrange
        var turma = new Turma { Nome = "Turma A" };
        _repositoryMock.Setup(repo => repo.GetByNomeAsync("Turma A")).ReturnsAsync(turma);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _turmaService.CreateAsync(turma));
    }

    [Fact]
    public async Task CreateAsync_ShouldSaveTurma_WhenNomeIsUnique()
    {
        // Arrange
        var turma = new Turma { Nome = "Turma A" };
        _repositoryMock.Setup(repo => repo.GetByNomeAsync("Turma A")).ReturnsAsync((Turma?)null);
        _repositoryMock.Setup(repo => repo.AddAsync(turma)).ReturnsAsync(1);

        // Act
        var result = await _turmaService.CreateAsync(turma);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowException_WhenNomeIsNotUnique()
    {
        // Arrange
        var turma = new Turma { Id = 1, Nome = "Turma A" };
        var existingTurma = new Turma { Id = 2, Nome = "Turma A" }; // Outra turma com o mesmo nome
        _repositoryMock.Setup(repo => repo.GetByNomeAsync("Turma A")).ReturnsAsync(existingTurma);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _turmaService.UpdateAsync(turma));
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateTurma_WhenNomeIsUnique()
    {
        // Arrange
        var turma = new Turma { Id = 1, Nome = "Turma A" };
        _repositoryMock.Setup(repo => repo.GetByNomeAsync("Turma A")).ReturnsAsync((Turma?)null);
        _repositoryMock.Setup(repo => repo.UpdateAsync(turma)).ReturnsAsync(1);

        // Act
        await _turmaService.UpdateAsync(turma);

        // Assert
        _repositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Turma>()), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowException_WhenRepositoryFails()
    {
        // Arrange
        var turma = new Turma { Id = 1, Nome = "Turma A" };
        _repositoryMock.Setup(repo => repo.UpdateAsync(turma)).ReturnsAsync(0);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _turmaService.UpdateAsync(turma));
    }

    [Fact]
    public async Task InactivateAsync_ShouldThrowException_WhenRepositoryFails()
    {
        // Arrange
        _repositoryMock.Setup(repo => repo.InactivateAsync(1)).ReturnsAsync(0);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _turmaService.InactivateAsync(1));
    }

    [Fact]
    public async Task InactivateAsync_ShouldInactivateTurma_WhenValid()
    {
        // Arrange
        _repositoryMock.Setup(repo => repo.InactivateAsync(1)).ReturnsAsync(1);

        // Act
        await _turmaService.InactivateAsync(1);

        // Assert
        _repositoryMock.Verify(repo => repo.InactivateAsync(1), Times.Once);
    }
}
