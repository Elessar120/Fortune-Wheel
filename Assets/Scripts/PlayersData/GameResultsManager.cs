using UnityEngine;
    public class GameResultsManager : MonoBehaviour
    {
        private const string GameResultsKey = "GameResults";
        IRotate rotate;
        private void Awake()
        {
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
            rotate.OnSaveGameResult += SaveGameResult;
        }

        private void SaveGameResult(bool playerWon)
        {
            // Load existing data or create new if not found
            GameResultsData data = LoadGameResults();
            data = AddPlayerToResults(data, playerWon);
            string json = JsonUtility.ToJson(data, true);
            PlayerPrefs.SetString(GameResultsKey, json);
            PlayerPrefs.Save();
        }

        private GameResultsData AddPlayerToResults(GameResultsData data, bool playerWon)
        {
            data.totalPlayers++;
            if (playerWon)
            {
                data.winners++;
            }
            return data;
        }
        private GameResultsData LoadGameResults()
        {
            if (PlayerPrefs.HasKey(GameResultsKey))
            {
                string json = PlayerPrefs.GetString(GameResultsKey);
                return JsonUtility.FromJson<GameResultsData>(json);
            }
            return new GameResultsData();
        }

     
        public float GetWinningPercentage()
        {
            GameResultsData data = LoadGameResults();
            if (data.totalPlayers == 0)
                return 0f;
            return (float)data.winners / data.totalPlayers * 100f;
        }
    }
