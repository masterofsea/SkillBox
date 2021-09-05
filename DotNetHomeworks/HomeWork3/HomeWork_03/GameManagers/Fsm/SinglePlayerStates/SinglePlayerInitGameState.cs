using System;
using HomeWork_03.Commands;
using HomeWork_03.GameManagers.Fsm.MultiPlayerStates;

namespace HomeWork_03.GameManagers.Fsm.SinglePlayerStates
{
    public class SinglePlayerInitGameState : GameState
    {
        public override void Next()
        {
            var players = new Player[2];

            var inputManager = new InputManager();

            inputManager.SetOrUpdateCommand(ConsoleKey.D1, new CommandOnKey1(GameManager));
            inputManager.SetOrUpdateCommand(ConsoleKey.NumPad1, new CommandOnKey1(GameManager));
            
            inputManager.SetOrUpdateCommand(ConsoleKey.D2, new CommandOnKey2(GameManager));
            inputManager.SetOrUpdateCommand(ConsoleKey.NumPad2, new CommandOnKey2(GameManager));
            
            inputManager.SetOrUpdateCommand(ConsoleKey.D3, new CommandOnKey3(GameManager));
            inputManager.SetOrUpdateCommand(ConsoleKey.NumPad3, new CommandOnKey3(GameManager));
            
            inputManager.SetOrUpdateCommand(ConsoleKey.D4, new CommandOnKey4(GameManager));
            inputManager.SetOrUpdateCommand(ConsoleKey.NumPad4, new CommandOnKey4(GameManager));

            players[0] = new RealPlayer
            {
                InputManager = inputManager
            };

            Console.WriteLine($"Choose your name");

            var name = Console.ReadLine();

            players[0].Name = name;

            players[1] = new AiPlayer()
            {
                Name = "AiPlayerOne"
            };
            

            GameManager.TransitionState(new MultiplayerPlayingGameState(players, 0));
        }
    }
}