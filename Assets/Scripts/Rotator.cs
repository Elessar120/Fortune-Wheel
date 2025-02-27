using System;
using System.Collections;
using DefaultNamespace;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rotator : MonoBehaviour
{
    #region Actions
    public  Action OnSpinEnd;
    public  Action OnLoseOneSpin;// can fill with things like particles
    public  Action OnWinGame;
    public  Action OnExtraChance;// can fill with things like particles
    public  Action OnUpdateUI;
    public  Action OnLoseGame;
    public  Action OnStartGame;
    public  Action<bool> OnSaveGameResult;
    #endregion
    #region Fields

    [SerializeField] private float delayToGivePrize = .5f;
    private float time;
    [Header("Wheel Data")]
    private bool isRotating;
    public static int currentSpin;
    [SerializeField] float minRotatePower;
    [SerializeField] float maxRotatePower;
    [SerializeField] float StopPower;
    [SerializeField] Rigidbody2D rigidbody2D;
    [SerializeField] PrizeData[] prizes;
    [SerializeField] private int maxSpins;
    #endregion
    private void Start()
    {
        ResetSpinCounts();
        if(rigidbody2D == null) rigidbody2D = GetComponent<Rigidbody2D>();
        OnUpdateUI?.Invoke();
    }

    private void FixedUpdate()
    {           
        CheckSpinStatus();
    }

    void CheckSpinStatus()
    {
        if (rigidbody2D.angularVelocity > 0)
        {
            StopWheel();
        }

        if (rigidbody2D.angularVelocity == 0 && isRotating && (time += Time.deltaTime) >= delayToGivePrize)
        {
           GivePrize();
        }
    }

    void GivePrize()
    {
        GetReward(); 
        isRotating = false; 
        time = 0;
        
    }
    private void StopWheel()
    {
        rigidbody2D.angularVelocity -= StopPower*Time.deltaTime;

        rigidbody2D.angularVelocity =  Mathf.Clamp(rigidbody2D.angularVelocity, 0 , maxRotatePower);
    }


    public void Rotate() 
    {
        if(isRotating == false)
        {
            rigidbody2D.AddTorque(Random.Range(minRotatePower, maxRotatePower));
            isRotating = true;
        }
    }


    private void GetReward()
    {
        float rotation = transform.eulerAngles.z;
        int prizeArea = (int)(rotation / 30) + 1; // +1 because PrizeData IDs start at 1
        Debug.Log($"Rotation: {rotation}, PrizeArea: {prizeArea}");
        bool isWinner = false;
        
        foreach (var prize in prizes)
        {
            if (prize.ID == prizeArea)
            {
                switch (prize.Type)
                {
                    case PrizeType.Lose:
                        Lose(prize);
                        break;
                    case PrizeType.ExtraChance:
                        ExtraChance(prize);
                        break;
                    case PrizeType.Win:
                        Win(prize);
                        isWinner = true;
                        break;
                }
            }
        }

        // Check if the game is over.
        if (IsGameOver())
        {
            OnSaveGameResult?.Invoke(false);
            OnLoseGame?.Invoke();
        }
        // If no win occurred, call OnSpinEnd to re-enable the spin button.
        else if (!isWinner)
        {
            OnSpinEnd?.Invoke();
            OnUpdateUI?.Invoke();
        }
        // If a win occurred, update the UI (if needed) but don't re-enable the spin button.
        else
        {
            OnSaveGameResult?.Invoke(false);
            OnUpdateUI?.Invoke();
        }
    }


    private bool IsGameOver()
    {
        if (currentSpin <= 0 )
        {
            return true;
        }
        return false;
    }

    private void ExtraChance(PrizeData prize)
    {
        OnExtraChance?.Invoke();
        UpdateSpinChances(prize.Amount);
    }

    private void UpdateSpinChances(int spinChanceCount)
    {
        currentSpin += spinChanceCount;
        OnUpdateUI?.Invoke();
    }
    private void Lose(PrizeData prize)
    {
        OnLoseOneSpin?.Invoke();
        UpdateSpinChances(prize.Amount);
    }

    private void Win(PrizeData prize)
    {
        OnWinGame?.Invoke();
    }

    public void ResetGame()
    {
        ResetSpinCounts();
        OnStartGame?.Invoke();
        OnUpdateUI?.Invoke();
        gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        // todo: Save Game
    }

    private void ResetSpinCounts()
    {
        currentSpin = maxSpins;
    }
   

}