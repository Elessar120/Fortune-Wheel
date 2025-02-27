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

        public Action OnSpinStart;

        private void Start()
        {
            if (spinButton == null)
            {
                spinButton = gameObject.GetComponent<Button>();
            }

            spinButton.onClick.AddListener(() =>
            {
                OnSpinStart?.Invoke();
                HideButton();
            });
            FindFirstObjectByType<Rotator>().OnLoseGame += HideButton;
            FindFirstObjectByType<Rotator>().OnWinGame += HideButton;
            FindFirstObjectByType<Rotator>().OnSpinEnd += ShowButton;
            FindFirstObjectByType<Rotator>().OnStartGame += ShowButton;
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