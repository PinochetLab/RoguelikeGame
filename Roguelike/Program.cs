global using static Roguelike.Consts;
using System;
using Roguelike;


while (true)
{
    using var game = new RoguelikeGame();
    game.Run();
    if (!game.IsGameOver)
    {
        break;
    }
}