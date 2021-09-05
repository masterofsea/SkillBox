using System;
using HomeWork_03.GameManagers.Fsm;

namespace HomeWork_03.GameManagers
{
    public class GameManager
    {
        private GameState GameState { get; set; }

        public DifficultMode DifficultMode { get; }

        public int GameNum { get; set; }

        public GameManager(DifficultMode difficultMode)
        {
            DifficultMode = difficultMode;
        }

        public void TransitionState(GameState state)
        {
            GameState = state;

            GameState.SetGameManager(this);
        }

        public void Start()
        {
            TransitionState(new StartGameState());

            StartGlobalCycle();
        }

        private void StartGlobalCycle()
        {
            while (GameState is StartGameState)
            {
                GameNum = new Random().Next(12 * (int) DifficultMode, 120 * (int) DifficultMode);
                do
                {
                    GameState.Next();
                } while (!(GameState is EndGameState)); //TODO EndGameState

                GameState.Next();
            }

            Console.WriteLine("Game over!");
        }

        public void ChangeGameNum(int difference)
        {
            GameNum += difference;
        }
    }
}