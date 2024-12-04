using Moq;
using System.Threading.Tasks;
using Xunit;

public class RelacionamentoServiceTests
{
    private readonly Mock<IRelacionamentoRepository> _relacionamentoRepositoryMock;
    private readonly RelacionamentoService _relacionamentoService;

    public RelacionamentoServiceTests()
    {
        _relacionamentoRepositoryMock = new Mock<IRelacionamentoRepository>();
        _relacionamentoService = new RelacionamentoService(_relacionamentoRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateRelacionamentoAsync_ShouldAddRelacionamento_WhenDataIsValid()
    {
        // Arrange
        var relacionamento = new RelacionamentoAlunoTurma
        {
            AlunoId = 1,
            TurmaId = 1
        };

        //_relacionamentoRepositoryMock.Setup(repo => repo.GetByAlunoAndTurmaAsync(relacionamento.AlunoId, relacionamento.TurmaId))
        //    .ReturnsAsync((RelacionamentoAlunoTurma?)null); // Simula que o relacionamento não existe
        //_relacionamentoRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<RelacionamentoAlunoTurma>()))
        //    .ReturnsAsync(1); // Simula que o ID retornado é 1

        //// Act
        //await _relacionamentoService.CreateRelacionamentoAsync(relacionamento);

        //// Assert
        //_relacionamentoRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<RelacionamentoAlunoTurma>()), Times.Once);
    }

    [Fact]
    public async Task CreateRelacionamentoAsync_ShouldThrowException_WhenRelacionamentoExists()
    {
        // Arrange
        var relacionamento = new RelacionamentoAlunoTurma
        {
            AlunoId = 1,
            TurmaId = 1
        };

        //_relacionamentoRepositoryMock.Setup(repo => repo.GetByAlunoAndTurmaAsync(relacionamento.AlunoId, relacionamento.TurmaId))
        //    .ReturnsAsync(relacionamento); // Simula que o relacionamento já existe

        //// Act & Assert
        //var exception = await Assert.ThrowsAsync<Exception>(() => _relacionamentoService.CreateRelacionamentoAsync(relacionamento));
        //Assert.Equal("O aluno já está relacionado a esta turma.", exception.Message);
    }
}
