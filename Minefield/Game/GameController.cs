using MineField.Models;

namespace MineField.Game;

public class GameController : IGameController

{
    private readonly Board _board;
    private readonly Player _player;

    public GameController(IGameBuilder builder)
    {
        var game = builder.NewGame();
        _board = game.board;
        _player = game.player;
    }

    public MoveResult Move(Direction direction)
    {
        // Moves player and guards board edges
        return direction switch
        {
            Direction.Down when _player.CurrentLocation.Row < GameBuilder.MaxRows => PlayMove(Direction.Down),
            Direction.Up when _player.CurrentLocation.Row > 0 => PlayMove(Direction.Up),
            Direction.Left when _player.CurrentLocation.Column > 0 => PlayMove(Direction.Left),
            Direction.Right when _player.CurrentLocation.Column < GameBuilder.MaxColumns => PlayMove(Direction.Right),
            _ => _player.VoidMove()
        };
    }

    private MoveResult PlayMove(Direction direction)
    {
        _player.IncrementMoves();

        var desiredLocation = direction switch 
        {
            Direction.Up => Location.MoveUp(_player.CurrentLocation),
            Direction.Down => Location.MoveDown(_player.CurrentLocation),
            Direction.Left=> Location.MoveLeft(_player.CurrentLocation),
            Direction.Right => Location.MoveRight(_player.CurrentLocation)
        };

        return IsHit(desiredLocation)
            ? _player.FailedMove()
            : _player.SuccessMove(desiredLocation);
    }

    public bool IsHit(Location location)
    {
        return _board.MineLocations.Any(loc => loc == location);
    }
}