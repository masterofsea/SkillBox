using System;
using System.Linq;
using HomeWork_03.Commands;
using HomeWork_03.GameManagers.Fsm.SinglePlayerStates;

namespace HomeWork_03.GameManagers.Fsm.MultiPlayerStates
{
    public class MultiplayerInitGameState : GameState
    {
        public override void Next()
        {
            var maxPlayersNum = (int) GameManager.DifficultMode * 2;
            Console.WriteLine($"Enter the number of players (1 - {maxPlayersNum})");


            try
            {
                var playersNum = Convert.ToUInt32(Console.ReadLine());

                if (playersNum > maxPlayersNum)
                {
                    Console.WriteLine("Too many players");

                    return;
                }

                if (playersNum == 1)
                {
                    GameManager.TransitionState(new SinglePlayerInitGameState());

                    return;
                }

                var inputManager = new InputManager();

                inputManager.SetOrUpdateCommand(ConsoleKey.D1, new CommandOnKey1(GameManager));
                inputManager.SetOrUpdateCommand(ConsoleKey.D2, new CommandOnKey2(GameManager));
                inputManager.SetOrUpdateCommand(ConsoleKey.D3, new CommandOnKey3(GameManager));
                inputManager.SetOrUpdateCommand(ConsoleKey.D4, new CommandOnKey4(GameManager));
                
                var players = new Player[playersNum];

                for (var i = 0; i < playersNum; ++i)
                {
                    
                    players[i] = new RealPlayer()
                    {
                        InputManager = inputManager
                    };

                    while (true)
                    {
                        Console.WriteLine($"Choose your name player {i}");

                        var name = Console.ReadLine();

                        if (players.Any(p => p != null && string.CompareOrdinal(p.Name, name) == 0))
                        {
                            Console.WriteLine($"Player with name {name} already exists. Please choose other name");

                            continue;
                        }

                        players[i].Name = name;
                        break;
                    }
                }

                GameManager.TransitionState(new MultiplayerPlayingGameState(players, 0));
            }
            catch (Exception)
            {
                GameManager.TransitionState(new MultiplayerInitGameState());
            }
        }
    }
}