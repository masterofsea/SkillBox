using HomeWork_03.GameManagers;

namespace HomeWork_03.Commands
{
    public class CommandOnKey3 : Command
    {
        private GameManager GameManager { get; }

        public CommandOnKey3(GameManager gameManager)
        {
            GameManager = gameManager;
        }


        public override void Execute()
        {
            GameManager.ChangeGameNum(GameManager.GameNum >= 4 ? -3 : GameManager.GameNum);
        }

        public override string ToString()
        {
            return (GameManager.GameNum >= 4 ? 3 : GameManager.GameNum).ToString();
        }
    }
}