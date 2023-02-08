using MineField.Game;
using MineField.Models;

namespace MineField.Tests.Game;

[TestClass]
public class GameControllerTests
{

    [TestMethod]
    [DataRow(1, 0, Direction.Up, 0, 0, "Can move up")]
    [DataRow(0, 0, Direction.Up, 0, 0, "Cannot exceed upper bound")]
    [DataRow(0, 0, Direction.Down, 1, 0, "Can move down")]
    [DataRow(GameBuilder.MaxRows, 0, Direction.Down, GameBuilder.MaxRows, 0, "cannot exceed lower bound")]
    [DataRow(0, 1, Direction.Right, 0, 2, "Can move Right")]
    [DataRow(0, 1, Direction.Left, 0, 0, "Can move Right")]
    [DataRow(0, 0, Direction.Left, 0, 0, "Cannot exceed left bound")]
    public void ShouldMoveAroundBoard(int startRow, int startColumn, Direction direction, int expectedRow, int expectedColumn, string because)
    {
        // Arrange
        var board = new Board(new Location[] { });
        var player = new Player(100, new Location(startRow, startColumn));
        var expected = new Location(expectedRow, expectedColumn);

        var builder = new Mock<IGameBuilder>();
        builder.Setup(b => b.NewGame()).Returns((board, player));

        var controller = new GameController(builder.Object);

        // Act
        var result = controller.Move(direction);

        // Assert
        result.NewLocation.Should().BeEquivalentTo(expected, because);
    }


    [TestMethod]
    public void ShouldWinGame()
    {
        // Arrange
        var board = new Board(new Location[] { });
        var player = new Player(100, new Location(0, GameBuilder.MaxColumns - 1));

        var builder = new Mock<IGameBuilder>();
        builder.Setup(b => b.NewGame()).Returns((board, player));

        var controller = new GameController(builder.Object);

        // Act
        var result = controller.Move(Direction.Right);

        // Assert
        result.PlayState.Should().Be(PlayState.Won);
    }


    [TestMethod]
    [DataRow(1, 0, Direction.Up, 2, 0)]
    [DataRow(1, 0, Direction.Down, 0, 0)]
    [DataRow(0, 1, Direction.Left, 0, 2)]
    [DataRow(0, 1, Direction.Right, 0, 0)]
    public void ShouldLoseGame(int mineRow, int mineColumn, Direction direction, int playerRow, int playerColumn)
    {
        // Arrange
        var mineLocation = new Location(mineRow, mineColumn);
        var playerLocation = new Location(playerRow, playerColumn);

        var board = new Board(new[] { mineLocation });
        var player = new Player(1, playerLocation);

        var builder = new Mock<IGameBuilder>();
        builder.Setup(b => b.NewGame()).Returns((board, player));

        var controller = new GameController(builder.Object);

        // Act
        var result = controller.Move(direction);

        // Assert
        result.PlayState.Should().Be(PlayState.Lost);
    }
}