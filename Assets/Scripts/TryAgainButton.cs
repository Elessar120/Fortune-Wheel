using UnityEngine;
using UnityEngine.UI;

public class TryAgainButton : MonoBehaviour
{
    [SerializeField] Button tryAgainButton;
    private ICanvasVisibility canvasVisibility;
    private void Start()
    {
        canvasVisibility = transform.parent.GetComponent<ICanvasVisibility>();
        if (tryAgainButton == null)
        {
            tryAgainButton = GetComponent<Button>();
        }
        
        tryAgainButton.onClick.AddListener(TryAgain);
    }

    private void TryAgain()
    {
        canvasVisibility.HideCanvas();
        FindFirstObjectByType<Rotator>().ResetGame();
    }

   
}