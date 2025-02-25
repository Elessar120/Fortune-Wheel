using System;
using UnityEngine;
using UnityEngine.UI;

    public class FortuneWheelSpinClickHandler : MonoBehaviour
    {
        [SerializeField] private Button spinButton;
        /// <summary>
         /// positive number for extra spins and negative for lose spins
        /// </summary>
        /// <param name="value"></param>

        public static Action<int> OnUpdateUI;
        public static Action OnSpinStart;

        private void Start()
        {
            if (spinButton == null)
            {
                spinButton = gameObject.GetComponent<Button>();
            }

            spinButton.onClick.AddListener(() =>
            {
                OnUpdateUI?.Invoke(-1);
                OnSpinStart?.Invoke();
                HideButton();
            });
            Roulette.OnSpinEnd += ShowButton;
            
        }

        private void OnDestroy()
        {
            spinButton.onClick.RemoveAllListeners();
        }

        private void ShowButton()
        {
            spinButton.interactable = true;
        }

        private void HideButton()
        {
            spinButton.interactable = false;
        }
    }