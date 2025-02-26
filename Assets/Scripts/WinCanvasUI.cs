using UnityEngine;
using UnityEngine.UI;

public class WinCanvasUI : MonoBehaviour, ICanvasVisibility
{
   [SerializeField] CanvasGroup canvasGroup;
   private void Start()
   {
      if (canvasGroup == null)
      {
         canvasGroup = GetComponent<CanvasGroup>();
      }

      Rotator.OnWin += ShowCanvas;
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
