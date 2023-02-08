using MineField.Game;
using MineField.Models;

namespace MineField.Tests.Game;

[TestClass]
public class PlayerMovesTests
{
    private Fixture _fixture;

    [TestInitialize]
    public void TestInitialize()
    {
        _fixture = new Fixture();
    }

    [TestMethod]
    public void ShouldCreateVoidMove()
    {
        // Arrange
        var player = _fixture.Create<Player>();

        // Act
        var result = player.VoidMove();

        // Assert
        result.NewLocation.Should().BeEquivalentTo(player.CurrentLocation);
        result.PlayState.Should().Be(PlayState.Playing);
        result.LifeLost.Should().BeFalse();
    }


    [TestMethod]
    [DataRow(GameBuilder.MaxColumns - 2, PlayState.Playing)]
    [DataRow(GameBuilder.MaxColumns, PlayState.Won)]
    public void ShouldCreateSuccessMove(int column, PlayState expectedPlayState)
    {
        // Arrange
        var newLocation = new Location(0, column);

        var player = _fixture.Build<Player>()
            .With(p => p.CurrentLocation, () => new Location(0, column - 1))
            .Create();

        // Act
        var actualResult = player.SuccessMove(newLocation);

        // Assert
        actualResult.LifeLost.Should().BeFalse();
        actualResult.PlayState.Should().Be(expectedPlayState);
        actualResult.NewLocation.Should().BeEquivalentTo(newLocation);
    }


    [TestMethod]
    [DataRow(1, 0, PlayState.Lost)]
    [DataRow(2, 1, PlayState.Playing)]
    public void ShouldCreateFailedMove(int currentLives, int expectedLives, PlayState expectedState)
    {
        // Arrange
        var player = new Player(currentLives, new Location(int.MaxValue, int.MinValue));

        // Act
        var actualResult = player.FailedMove();

        // Assert
        actualResult.Player.Lives.Should().Be(expectedLives);
        actualResult.PlayState.Should().Be(expectedState);

    }
}