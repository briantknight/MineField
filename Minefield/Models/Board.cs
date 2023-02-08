namespace MineField.Models;

public class Board
{
    public Board(IEnumerable<Location> locations)
    {
        MineLocations = new List<Location>(locations);
    }

    public IReadOnlyList<Location> MineLocations { get; }
}

