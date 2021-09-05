using System;

namespace HomeWork_03.GameManagers.Fsm.MultiPlayerStates
{
    public class MultiplayerPlayingGameState : GameState
    {
        private Player[] Players { get; }

        private int MoveNum { get; set; }

        public MultiplayerPlayingGameState(Player[] players, int moveNum)
        {
            Players = players;

            MoveNum = moveNum;
        }

        public override void Next()
        {
            var playerIndex = MoveNum % Players.Length;
            Console.WriteLine($"Number: {GameManager.GameNum}");

            if (Players[playerIndex] is RealPlayer)
            {
                var command = ((RealPlayer) (Players[playerIndex])).InputManager.WaitUser();
                
                command.Execute();

                Console.WriteLine($"Player {Players[playerIndex].Name}: {command}");
            }
            else
            {
                //Вот здесь я понял, что смысла особо стараться не было, и забил, так как, чтобы сделать архитектуру
                //адекватной придется лезть гораздо глубже и менять почти все. Уже и повторяющийся код, и жесткие связи
                //хардкод...
                //С другой стороны для 3-го ДЗ на скорую руку то что нужно)

                //Sense Think Act
                int move;
                if (GameManager.GameNum <= 4)
                {
                    move = GameManager.GameNum;
                }
                else if (GameManager.GameNum % 5 != 0)
                {
                    move = GameManager.GameNum % 5;
                }
                else
                {
                    move = new Random().Next(1, 5);
                }


                GameManager.ChangeGameNum(-move);

                Console.WriteLine($"AI Player {Players[playerIndex].Name}: {move}");
            }

            var nextState = GameManager.GameNum <= 0
                ? (GameState) new EndGameState(Players[playerIndex])
                : new MultiplayerPlayingGameState(Players, ++MoveNum);

            GameManager.TransitionState(nextState);
        }
    }
}