using System;
using TMPro;
using UnityEngine;

public class SpinCounterUI : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI spinCountText;

   private void Start()
   {
      if (spinCountText == null) spinCountText = GetComponentInChildren<TextMeshProUGUI>();
      FindFirstObjectByType<Rotator>().OnUpdateUI += SetText;
   }

   private void SetText()
   {
      spinCountText.text = Rotator.currentSpin.ToString();
   }
}
