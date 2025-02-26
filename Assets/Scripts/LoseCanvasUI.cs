using UnityEngine;
using UnityEngine.UI;

public class LoseCanvasUI : MonoBehaviour, ICanvasVisibility
{
    [SerializeField] CanvasGroup canvasGroup;

    private void Start()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        Rotator.OnLoseGame += ShowCanvas;
    }

    public void ShowCanvas()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void HideCanvas()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}