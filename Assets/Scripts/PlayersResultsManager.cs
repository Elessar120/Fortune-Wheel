using System;
using UnityEngine;
    public class PlayersResultsManager : MonoBehaviour
    {
        private const string GameResultsKey = "GameResults";

        private void Start()
        {
            Rotator.OnEndGame += SaveGameResult;
        }

        private void SaveGameResult(bool playerWon)
        {
            // Load existing data or create new if not found
            GameResultsData data = LoadGameResults();
            data.totalPlayers++;
            if (playerWon)
            {
                data.winners++;
            }
            string json = JsonUtility.ToJson(data, true);
            PlayerPrefs.SetString(GameResultsKey, json);
            PlayerPrefs.Save();
        }

        private GameResultsData LoadGameResults()
        {
            if (PlayerPrefs.HasKey(GameResultsKey))
            {
                string json = PlayerPrefs.GetString(GameResultsKey);
                return JsonUtility.FromJson<GameResultsData>(json);
            }
            // If not found, return a new instance with default values (0)
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
