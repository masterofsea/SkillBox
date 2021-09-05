using System;

namespace HomeWork_03.GameManagers.Fsm
{
    public class EndGameState : GameState
    {   
        public Player Winner { get; }

        public EndGameState(Player player)
        {
            Winner = player;
        }

        public override void Next()
        {
            Console.WriteLine($"Player {Winner.Name} won!");
            
            Console.WriteLine("Do you want play again? Y/N");
            

            while (true)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Y:
                        GameManager.TransitionState(new StartGameState());
                        break;
                    case ConsoleKey.N:
                        break;
                    default: continue;
                }
                
                break;
            }
        }
    }
}