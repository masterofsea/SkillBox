using HomeWork_03.GameManagers;

namespace HomeWork_03.Commands
{
    public class CommandOnKey2 : Command
    {
        private GameManager GameManager { get; }

        public CommandOnKey2(GameManager gameManager)
        {
            GameManager = gameManager;
        }


        public override void Execute()
        {
            GameManager.ChangeGameNum(GameManager.GameNum >= 4 ? -2 : GameManager.GameNum);
        }

        public override string ToString()
        {
            return (GameManager.GameNum >= 4 ? 2 : GameManager.GameNum).ToString();
        }
    }
}