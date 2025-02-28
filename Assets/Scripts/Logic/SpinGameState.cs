using UnityEngine;

[RequireComponent(typeof(IRotate))]
[RequireComponent(typeof(IReward))]
    public class SpinGameState : MonoBehaviour, IGameStates
    {
        public int currentSpin { get; set; }
        [SerializeField] private int maxSpins = 10;
        private IRotate rotate;

        private void Awake()
        {
            if (rotate == null)
               rotate = GetComponent<IRotate>();
        }

        public bool IsGameOver()
        {
            if (currentSpin <= 0 )
            {
                return true;
            }
            return false;
        }
       
        public void ResetGame()
        {
            ResetSpinCounts();
            rotate.OnStartGame?.Invoke();
            rotate.OnUpdateUI?.Invoke();
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        public void ResetSpinCounts()
        {
            currentSpin = maxSpins;
        }
    }
