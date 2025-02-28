using UnityEngine;

[RequireComponent(typeof(IRotate))]
[RequireComponent(typeof(IGameStates))]
    public class Reward : MonoBehaviour, IReward
    {
        [SerializeField] PrizeData[] prizes;
        private IGameStates _gameGameState;
        private IRotate rotate;
        private void Start()
        {
            _gameGameState = GetComponent<IGameStates>();
            rotate = GetComponent<IRotate>();
        }

        public void GetReward()
            {
                float rotation = transform.eulerAngles.z;
                int prizeArea = (int)(rotation / 30) + 1; // +1 because PrizeData IDs start at 1
                bool isWinner = false;
                
                foreach (var prize in prizes)
                {
                    if (prize.ID == prizeArea)
                    {
                        switch (prize.Type)
                        {
                            case PrizeType.Lose:
                                Lose(prize);
                                break;
                            case PrizeType.ExtraChance:
                                ExtraChance(prize);
                                break;
                            case PrizeType.Win:
                                Win(prize);
                                isWinner = true;
                                break;
                        }
                    }
                }
        
                // Check if the game is over.
                if (_gameGameState.IsGameOver())
                {
                    rotate.OnSaveGameResult?.Invoke(false);
                    rotate.OnLoseGame?.Invoke();
                }
                // If no win/ Game Over occurred, call OnSpinEnd to re-enable the spin button.
                else if (!isWinner)
                {
                    rotate.OnSpinEnd?.Invoke();
                    rotate.OnUpdateUI?.Invoke();
                }
                // If win occurred, update the UI but don't re-enable the spin button.
                else
                {
                    rotate.OnSaveGameResult?.Invoke(false);
                    rotate.OnUpdateUI?.Invoke();
                }
            }

            private void ExtraChance(PrizeData prize)
            {
                rotate.OnExtraChance?.Invoke();
                UpdateSpinChances(prize.Amount);
            }

            private void UpdateSpinChances(int spinChanceCount)
            {
                _gameGameState.currentSpin += spinChanceCount;
                rotate.OnUpdateUI?.Invoke();
            }
            private void Lose(PrizeData prize)
            {
                rotate.OnLoseOneSpin?.Invoke();
                UpdateSpinChances(prize.Amount);
            }

            private void Win(PrizeData prize)
            {
                rotate.OnWinGame?.Invoke();
            }
        
    }
