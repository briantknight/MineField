using System.Data;
using System.Drawing;
using MineField.Models;

namespace MineField.Views;

public class KeyToMoveConverter: IConverter<char, Direction>
{
    public (bool parsed, Direction value) TryConvert(char source) => source switch
    {
        'u' => (true, Direction.Up),
        'l' => (true, Direction.Left),
        'r' => (true, Direction.Right),
        'd' => (true, Direction.Down),
        _ => (false, Direction.Down)
    };
}