using HomeWork_03.GameManagers;

namespace HomeWork_03.Commands
{
    public class CommandOnKey1 : Command
    {
        private GameManager GameManager { get; }

        public CommandOnKey1(GameManager gameManager)
        {
            GameManager = gameManager;
        }


        public override void Execute()
        {
            GameManager.ChangeGameNum(GameManager.GameNum >= 4 ? -1 : GameManager.GameNum);
        }

        public override string ToString()
        {
            return (GameManager.GameNum >= 4 ? 1 : GameManager.GameNum).ToString();
        }
    }
}