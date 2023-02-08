using MineField.Game;
using MineField.Models;

namespace MineField.Views;

public class View : IView
{
    private readonly IGameController _controller;
    private readonly IConverter<char, Direction> _keyToMoveConverter = new KeyToMoveConverter();
    private readonly IConverter<MoveResult, string> _resultToMessageConverter = new ResultToMessageConverter();

    public View(IGameController controller)
    {
        _controller = controller;
    }

    public void Play()
    {
        Console.WriteLine("press l for left, r for right, u for up, d for down. Good luck!");
        var playState = PlayState.Playing;
        do
        {
            var key = Console.Read();

            var input = _keyToMoveConverter.TryConvert((char)key);

            if (input.parsed)
            {
                var latestMoveResult = _controller.Move(input.value);

                playState = latestMoveResult.PlayState;

                var message = _resultToMessageConverter.TryConvert(latestMoveResult);
                Console.WriteLine(message.value);
            }
        }
        while (playState != PlayState.Lost);
    }
}