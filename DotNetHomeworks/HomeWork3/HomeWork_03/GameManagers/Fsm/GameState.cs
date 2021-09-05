namespace HomeWork_03.GameManagers.Fsm
{
    public abstract class GameState
    {
        protected GameManager GameManager { get; set; }

        public void SetGameManager(GameManager gameManager)
        {
            GameManager = gameManager;
        }
        

        public abstract void Next();
    }
}