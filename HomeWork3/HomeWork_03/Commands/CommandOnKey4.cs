using HomeWork_03.GameManagers;

namespace HomeWork_03.Commands
{
    public class CommandOnKey4 : Command
    {
        private GameManager GameManager { get; }

        public CommandOnKey4(GameManager gameManager)
        {
            GameManager = gameManager;
        }


        public override void Execute()
        {
            GameManager.ChangeGameNum(GameManager.GameNum >= 4 ? -4 : GameManager.GameNum);
        }

        public override string ToString()
        {
            return (GameManager.GameNum >= 4 ? 4 : GameManager.GameNum).ToString();
        }
    }
}