    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class UIManager : MonoBehaviour
    {

        [Header("UI References")]
        [SerializeField] private Button spinButton;
        [SerializeField] private TextMeshProUGUI spinCountText;
        public CanvasGroup winCanvasGroup;
        public CanvasGroup loseCanvasGroup;
        private IGameStates gameState;
        private IRotate rotate;
        
        private void Awake()
        {
            GameObject wheel = GameObject.FindWithTag("Wheel");

            if (wheel != null)
            {
                gameState = wheel.GetComponent<IGameStates>();
                rotate = wheel.GetComponent<IRotate>();
            }
            else
            {
                Debug.LogError("Wheel GameObject not found! Ensure it has the correct tag.");
            }
        }

        private void OnEnable()
        {
            if(rotate != null)
            {
                rotate.OnSpinEnd += ShowSpinButton;
                rotate.OnWinGame += OnWin;
                rotate.OnLoseGame += OnLoseGame;
                rotate.OnStartGame += ShowSpinButton;
                rotate.OnUpdateUI += UpdateSpinCounter;
            }

            if(spinButton != null)
            {
                spinButton.onClick.AddListener(OnSpinButtonClicked);
            }
        }

        private void OnDisable()
        {
            if(rotate != null)
            {
                rotate.OnSpinEnd -= ShowSpinButton;
                rotate.OnWinGame -= OnWin;
                rotate.OnLoseGame -= OnLoseGame;
                rotate.OnStartGame -= ShowSpinButton;
                rotate.OnUpdateUI -= UpdateSpinCounter;
            }
            if(spinButton != null)
            {
                spinButton.onClick.RemoveListener(OnSpinButtonClicked);
            }
        }

        private void OnSpinButtonClicked()
        {
            if(rotate != null)
            {
                rotate.Rotate();
            }
            HideSpinButton();
        }

        private void UpdateSpinCounter()
        {
            if(spinCountText != null)
                spinCountText.text = gameState.currentSpin.ToString();
        }

        private void ShowSpinButton()
        {
            if(spinButton != null)
                spinButton.interactable = true;
        }

        private void HideSpinButton()
        {
            if(spinButton != null)
                spinButton.interactable = false;
        }

        private void OnWin()
        {
            if(winCanvasGroup != null)
            {
                winCanvasGroup.alpha = 1;
                winCanvasGroup.interactable = true;
                winCanvasGroup.blocksRaycasts = true;
            }
            HideSpinButton();
            UpdateSpinCounter();
        }

        private void OnLoseGame()
        {
            if(loseCanvasGroup != null)
            {
                loseCanvasGroup.alpha = 1;
                loseCanvasGroup.interactable = true;
                loseCanvasGroup.blocksRaycasts = true;
            }
            HideSpinButton();
            UpdateSpinCounter();
        }
        public void HandleTryAgainButton()
        {
            if (winCanvasGroup != null)
            {
                winCanvasGroup.alpha = 0;
                winCanvasGroup.interactable = false;
                winCanvasGroup.blocksRaycasts = false;
            }

            if (loseCanvasGroup != null)
            {
                loseCanvasGroup.alpha = 0;
                loseCanvasGroup.interactable = false;
                loseCanvasGroup.blocksRaycasts = false;
            }

            if (gameState != null)
            {
                gameState.ResetGame();
            }
        }
    }
