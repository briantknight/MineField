using MineField.Models;

namespace MineField.Game;

public class GameBuilder : IGameBuilder
{
    public const int MaxRows = 8;
    public const int MaxColumns = 8;

    private readonly Options _gameOptions;

    public GameBuilder(Options gameOptions)
    {
        _gameOptions = gameOptions;
    }

    public (Board board, Player player) NewGame()
    {
        var seeder = new Random();
        var boardLocations = new List<Location>();

        var playerStart = new Location(seeder.Next(MaxRows), 0); // Player starts on first column

        boardLocations.Add(playerStart);

        while (boardLocations.Count < _gameOptions.NumberOfMines + 1)
        {
            var location = new Location(seeder.Next(MaxRows), seeder.Next(MaxColumns));

            if (!boardLocations.Contains(location)) // Only allow unique mine locations
            {
                boardLocations.Add(location);
            }
        }

        var player = new Player(_gameOptions.MaxLives, playerStart);
        var board = new Board(boardLocations.Skip(1));

        return (board, player);
    }
}