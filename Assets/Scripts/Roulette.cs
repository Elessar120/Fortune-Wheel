using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Roulette : MonoBehaviour
{
    [SerializeField] float minRotatePower;
    [SerializeField] float maxRotatePower;
    [SerializeField] float StopPower;
    [SerializeField] Rigidbody2D rigidbody2D;
    public static Action OnSpinEnd;
    public static Action OnLose;
    public static Action OnWin;
    public static Action OnExtraChance;
    int inRotate;
    private int rewardNumber;

    private void Start()
    {
        rewardNumber = 0;
        if(rigidbody2D == null) rigidbody2D = GetComponent<Rigidbody2D>();
        FortuneWheelSpinClickHandler.OnSpinStart += Rotete;
    }

    float t;
    private void FixedUpdate()
    {           
        
        if (rigidbody2D.angularVelocity > 0)
        {
            rigidbody2D.angularVelocity -= StopPower*Time.deltaTime;

            rigidbody2D.angularVelocity =  Mathf.Clamp(rigidbody2D.angularVelocity, 0 , maxRotatePower);
        }

        if(rigidbody2D.angularVelocity == 0 && inRotate == 1) 
        {
            t +=1*Time.deltaTime;// use coroutine
            if(t >= 0.5f)
            {
                GetReward();

                inRotate = 0;
                t = 0;
            }
        }
    }


    private void Rotete() 
    {
        if(inRotate == 0)
        {
            rigidbody2D.AddTorque(Random.Range(minRotatePower, maxRotatePower));
            inRotate = 1;
        }
    }


    private void GetReward()
    {
        float rotation = transform.eulerAngles.z;

        switch (rotation)
        {
            case > 0 and <= 30:
                Lose();
                break;
            case > 30 and <= 60:
                Lose();
                break;
            case > 60 and <= 90:
                Lose();
                break;
            case > 90 and <= 120:
                ExtraChance();
                break;
            case > 120 and <= 150:
                Lose();
                break;
            case > 150 and <= 180:
                Lose();
                break;
            
            case > 180 and <= 210:
                ExtraChance();
                break;
            case > 210 and <= 240:
                Lose();
                break;
            case > 240 and <= 270:
                Lose();
                break;
            case > 270 and <= 300:  
                ExtraChance();
                break;
            case > 300 and <= 330:
                Lose();
                break;
            case > 330 and <= 360:
                Win(800);
                break;
        }
    }

    private void ExtraChance()
    {
        OnSpinEnd?.Invoke();
        OnExtraChance?.Invoke();
        FortuneWheelSpinClickHandler.OnUpdateUI?.Invoke(1);

    }

    private void Lose()
    {
        OnSpinEnd?.Invoke();
        FortuneWheelSpinClickHandler.OnUpdateUI?.Invoke(-1);
        OnLose?.Invoke();
    }
    public void Win(int Score)
    {
        print(Score);
        OnSpinEnd?.Invoke();
        OnWin?.Invoke();
    }


}