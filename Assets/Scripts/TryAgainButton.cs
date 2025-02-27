using UnityEngine;
using UnityEngine.UI;

public class TryAgainButton : MonoBehaviour
{
    [SerializeField] private Button tryAgainButton;

    private void Awake()
    {
        // If the button reference is not assigned, try to get it from the same GameObject.
        if (tryAgainButton == null)
            tryAgainButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        if (tryAgainButton != null)
            tryAgainButton.onClick.AddListener(HandleTryAgainButton);
    }

    private void OnDisable()
    {
        if (tryAgainButton != null)
            tryAgainButton.onClick.RemoveListener(HandleTryAgainButton);
    }

    private void HandleTryAgainButton()
    {
        // Hide win canvas if assigned
        if (UIManager.Instance.winCanvasGroup != null)
        {
            UIManager.Instance.winCanvasGroup.alpha = 0;
            UIManager.Instance.winCanvasGroup.interactable = false;
            UIManager.Instance.winCanvasGroup.blocksRaycasts = false;
        }

        // Hide lose canvas if assigned
        if (UIManager.Instance.loseCanvasGroup != null)
        {
            UIManager.Instance.loseCanvasGroup.alpha = 0;
            UIManager.Instance.loseCanvasGroup.interactable = false;
            UIManager.Instance.loseCanvasGroup.blocksRaycasts = false;
        }

        // Find the Rotator in the scene and reset the game
        Rotator rotator = FindObjectOfType<Rotator>();
        if (rotator != null)
        {
            rotator.ResetGame();
        }
    }
}
