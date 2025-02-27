using System;
using TMPro;
using UnityEngine;

public class WinRateTextShow : MonoBehaviour
{
    private TextMeshProUGUI winRateText;
    private void Start()
    {
        if (winRateText == null)
        {
            winRateText = GetComponent<TextMeshProUGUI>();
        }
        Rotator rotator = FindObjectOfType<Rotator>();
        if(rotator != null)
        {
            rotator.OnWinGame += UpdateWinRateText;
            rotator.OnLoseGame += UpdateWinRateText;
            
        }
        
    }

    private void OnDestroy()
    {
        Rotator rotator = FindObjectOfType<Rotator>();
        if(rotator != null)
        {
            rotator.OnWinGame -= UpdateWinRateText;
            rotator.OnLoseGame -= UpdateWinRateText;
        }
    }

    private void UpdateWinRateText()
    {
        var data = FindFirstObjectByType<PlayersResultsManager>();
        winRateText.text = data.GetWinningPercentage().ToString();
    }
}
