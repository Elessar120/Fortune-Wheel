    public interface IGameStates
    {
        public bool IsGameOver();
        public void ResetSpinCounts();
        public int currentSpin { get;  set; }
        public void ResetGame();

    }
