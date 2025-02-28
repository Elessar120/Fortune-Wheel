using TMPro;
using UnityEngine;

public class WinRateUpdater : MonoBehaviour
{
    private TextMeshProUGUI winRateText;
    IRotate rotate;
    private void Awake()
    {
        if (winRateText == null)
        {
            winRateText = GetComponent<TextMeshProUGUI>();
        }
        GameObject wheel = GameObject.FindWithTag("Wheel");

        if (wheel != null)
        {
            rotate = wheel.GetComponent<IRotate>();
        }
        else
        {
            Debug.LogError("Wheel GameObject not found! Ensure it has the correct tag.");
        }
    
    }

    private void Start()
    {
        if(rotate != null)
        {
            rotate.OnWinGame += UpdateWinRateText;
            rotate.OnLoseGame += UpdateWinRateText;
            
        }

    }

    private void OnDestroy()
    {
        if(rotate != null)
        {
            rotate.OnWinGame -= UpdateWinRateText;
            rotate.OnLoseGame -= UpdateWinRateText;
        }
    }

    private void UpdateWinRateText()
    {
        var data = FindFirstObjectByType<GameResultsManager>();
        winRateText.text = data.GetWinningPercentage().ToString();
    }
}
