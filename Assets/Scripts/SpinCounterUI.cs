using TMPro;
using UnityEngine;

public class SpinCounterUI : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI spinCountText;
   [SerializeField] private int maxSpins;
   private int currentSpin;

   private void Start()
   {
      if (spinCountText == null) spinCountText = GetComponentInChildren<TextMeshProUGUI>();
      currentSpin = maxSpins;
      spinCountText.text = maxSpins.ToString();
      FortuneWheelSpinClickHandler.OnUpdateUI += UpdateSpinCount;
   }
   private void UpdateSpinCount(int value)
   {
      currentSpin += value;
      if (currentSpin <= 0)
      {
         currentSpin = 0;
      }
      spinCountText.text = currentSpin.ToString();
   }
}
