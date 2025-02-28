using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    [SerializeField] Button exitButton;

    private void Start()
    {
        if (exitButton == null)
        {
            exitButton = GetComponent<Button>();
        }
        exitButton.onClick.AddListener(Exit);
    }

    private void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
