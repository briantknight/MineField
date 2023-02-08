namespace MineField.Models;

public class Player
{
    private int _lives;
    private int _moves;

    public Player(int maxLives, Location startLocation)
    {
        _lives = maxLives;
        StartLocation = startLocation;
        CurrentLocation = StartLocation;
    }

    public Location StartLocation { get; }
    public Location CurrentLocation { get; set; }

    public int Lives => _lives;

    public int Moves => _moves;

    public void DecrementLives() => _lives--;

    public void IncrementMoves() => _moves++;

    public override string ToString()
    {
        return $"Position : {CurrentLocation}, Lives left: {Lives}, Attempts : {Moves}";
    }
}