using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MineField.Game;
using MineField.Models;
using MineField.Views;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton(new Options { MaxLives = 3, NumberOfMines = 5});
        services.AddSingleton<IGameBuilder, GameBuilder>();
        services.AddSingleton<IGameController, GameController>();
        services.AddSingleton<IView, View>();
    }).Build();

host.Services.GetService<IView>()!.Play();
    
