    using System;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        [Header("UI References")]
        [SerializeField] private Button spinButton;
        [SerializeField] private TextMeshProUGUI spinCountText;
        public CanvasGroup winCanvasGroup;
        public CanvasGroup loseCanvasGroup;
        
        private void Awake()
        {
            // Singleton implementation
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void OnEnable()
        {
            // Subscribe to Rotator events
            Rotator rotator = FindObjectOfType<Rotator>();
            if(rotator != null)
            {
                rotator.OnSpinEnd += ShowSpinButton;
                rotator.OnWinGame += OnWin;
                rotator.OnLoseGame += OnLoseGame;
                rotator.OnStartGame += ShowSpinButton;
                rotator.OnUpdateUI += UpdateSpinCounter;
            }

            if(spinButton != null)
            {
                spinButton.onClick.AddListener(OnSpinButtonClicked);
            }
        }

        private void OnDisable()
        {
            Rotator rotator = FindObjectOfType<Rotator>();
            if(rotator != null)
            {
                rotator.OnSpinEnd -= ShowSpinButton;
                rotator.OnWinGame -= OnWin;
                rotator.OnLoseGame -= OnLoseGame;
                rotator.OnStartGame -= ShowSpinButton;
                rotator.OnUpdateUI -= UpdateSpinCounter;
            }
            if(spinButton != null)
            {
                spinButton.onClick.RemoveListener(OnSpinButtonClicked);
            }
        }

        private void OnSpinButtonClicked()
        {
            // Trigger a spin via the Rotator
            Rotator rotator = FindObjectOfType<Rotator>();
            if(rotator != null)
            {
                rotator.Rotate();
            }
            HideSpinButton();
        }

        public void UpdateSpinCounter()
        {
            if(spinCountText != null)
                spinCountText.text = Rotator.currentSpin.ToString();
        }

        public void ShowSpinButton()
        {
            if(spinButton != null)
                spinButton.interactable = true;
        }

        public void HideSpinButton()
        {
            if(spinButton != null)
                spinButton.interactable = false;
        }

        private void OnWin()
        {
            // Show win canvas and update UI accordingly
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
            // Show lose canvas and update UI accordingly
            if(loseCanvasGroup != null)
            {
                loseCanvasGroup.alpha = 1;
                loseCanvasGroup.interactable = true;
                loseCanvasGroup.blocksRaycasts = true;
            }
            HideSpinButton();
            UpdateSpinCounter();
        }
    }
