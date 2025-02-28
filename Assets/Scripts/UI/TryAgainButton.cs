using UnityEngine;
using UnityEngine.UI;

public class TryAgainButton : MonoBehaviour
{
    [SerializeField] private Button tryAgainButton;
    [SerializeField] UIManager uiManager;
    IGameStates gameStates;
    private void Awake()
    {
        if (tryAgainButton == null)
            tryAgainButton = GetComponent<Button>();
        if (uiManager == null)
        {
            uiManager = FindObjectOfType<UIManager>();
        }
    }

    private void OnEnable()
    {
        if (tryAgainButton != null)
            tryAgainButton.onClick.AddListener(TryAgain);
    }

    private void OnDisable()
    {
        if (tryAgainButton != null)
            tryAgainButton.onClick.RemoveListener(TryAgain);
    }
    private void TryAgain()
    {
        if (uiManager != null)
        {
            uiManager.HandleTryAgainButton(); 
        }
        else
        {
            Debug.LogError("UIManager not found");
        }
    }

}
