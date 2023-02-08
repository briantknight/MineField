using MineField.Models;

namespace MineField.Views;

public class ResultToMessageConverter : IConverter<MoveResult, string>
{
    public (bool parsed, string value) TryConvert(MoveResult source) => source switch
    {
        { PlayState: PlayState.Playing } => (true, source.Player.ToString()),
        { PlayState: PlayState.Won } => (true,  $"You have won! {source.Player}"),
        { PlayState: PlayState.Lost } => (true, $"You have lost! {source.Player}")
    };
}