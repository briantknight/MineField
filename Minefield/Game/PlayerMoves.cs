using MineField.Models;

namespace MineField.Game;

public static class PlayerMoves
{
    public static MoveResult VoidMove(this Player player)
    {
        return new MoveResult(false, player.CurrentLocation, PlayState.Playing, player);
    }
    public static MoveResult SuccessMove(this Player player, Location newLocation)
    {
        player.CurrentLocation = newLocation;
        var gameResult = newLocation.Column == GameBuilder.MaxColumns ? PlayState.Won : PlayState.Playing;
        return new MoveResult(false, newLocation, gameResult, player);
    }

    public static MoveResult FailedMove(this Player player)
    {
        player.DecrementLives();
        var gameResult = player.Lives > 0 ? PlayState.Playing : PlayState.Lost;
        return new MoveResult(true, player.StartLocation, gameResult, player);
    }
}