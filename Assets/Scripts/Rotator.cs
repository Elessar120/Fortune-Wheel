using System;
using DefaultNamespace;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rotator : MonoBehaviour
{
    [SerializeField] float minRotatePower;
    [SerializeField] float maxRotatePower;
    [SerializeField] float StopPower;
    //negative for lose chance and positive for extra chance
    [SerializeField] private int eachSpinCost = -1;
    [SerializeField] Rigidbody2D rigidbody2D;
    public static Action OnSpinEnd;
    public static Action OnLoseOneSpin;
    public static Action OnWin;
    public static Action OnExtraChance;
    public static Action OnUpdateUI;
    public static Action OnLoseGame;
    public static Action OnStartGame;
    public static Action<bool> OnEndGame;
    private bool isRotating;
    private int rewardNumber;
    [SerializeField] PrizeData[] prizes;
    [SerializeField] private int maxSpins;
    public static int currentSpin;

    private void Start()
    {
        rewardNumber = 0;
        currentSpin = maxSpins;
        if(rigidbody2D == null) rigidbody2D = GetComponent<Rigidbody2D>();
        FortuneWheelSpinClickHandler.OnSpinStart += Rotate;
        OnUpdateUI?.Invoke();
    }

    float t;
    private void FixedUpdate()
    {           
        
        if (rigidbody2D.angularVelocity > 0)
        {
            rigidbody2D.angularVelocity -= StopPower*Time.deltaTime;

            rigidbody2D.angularVelocity =  Mathf.Clamp(rigidbody2D.angularVelocity, 0 , maxRotatePower);
        }

        if(rigidbody2D.angularVelocity == 0 && isRotating == true) 
        {
            t +=1*Time.deltaTime;// use coroutine
            if(t >= 0.5f)
            {
                GetReward();

                isRotating = false;
                t = 0;
            }
        }
    }


    private void Rotate() 
    {
        if(isRotating == false)
        {
            UpdateSpinChances(eachSpinCost);
            rigidbody2D.AddTorque(Random.Range(minRotatePower, maxRotatePower));
            isRotating = true;
        }
    }


    private void GetReward()
    {
        float rotation = transform.eulerAngles.z;
        int prizeArea = (int)(rotation / 30) + 1; // +1 because PrizeData IDs start at 1
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
            OnEndGame?.Invoke(false);
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

    }

    private void Win(PrizeData prize)
    {
        OnEndGame?.Invoke(true);
        OnWin?.Invoke();
    }

    public void ResetGame()
    {
        currentSpin = maxSpins;
        OnStartGame?.Invoke();
        OnUpdateUI?.Invoke();
        gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
        // todo: Save Game
    }

   

}