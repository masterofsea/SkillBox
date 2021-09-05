using System;
using HomeWork_03.GameManagers.Fsm.MultiPlayerStates;
using HomeWork_03.GameManagers.Fsm.SinglePlayerStates;

namespace HomeWork_03.GameManagers.Fsm
{
    public class StartGameState : GameState
    {
        public override void Next()
        {
            Console.WriteLine("Choose game mode: Multiplayer - 1\tSingle player - 2");
            var key = Console.ReadKey(true).Key;
            while (true)
            {
                switch (key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        GameManager.TransitionState(new MultiplayerInitGameState());
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        GameManager.TransitionState(new SinglePlayerInitGameState());
                        break;

                    default:
                        continue;
                }

                break;
            }
        }
    }
}