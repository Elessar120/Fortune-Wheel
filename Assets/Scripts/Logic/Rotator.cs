using System;
using UnityEngine;
using Random = UnityEngine.Random;
[RequireComponent(typeof(IGameStates))]
[RequireComponent(typeof(IReward))]
public class Rotator : MonoBehaviour, IRotate
{
    #region Actions
    public  Action OnSpinEnd{ get; set; }
    public  Action OnLoseOneSpin{ get; set; }// can fill with things like particles
    public  Action OnWinGame{ get; set; }
    public  Action OnExtraChance{ get; set; }// can fill with things like particles
    public  Action OnUpdateUI{ get; set; }
    public  Action OnLoseGame{ get; set; }
    public  Action OnStartGame{ get; set; }
    public  Action<bool> OnSaveGameResult{ get; set; }
    #endregion
    #region Fields

    [SerializeField] private float delayToGivePrize = .5f;
    private float time;
    [Header("Wheel Data")]
    private bool isRotating;
    [SerializeField] float minRotatePower;
    [SerializeField] float maxRotatePower;
    [SerializeField] float StopPower;
    [SerializeField] Rigidbody2D rigidbody2D;
    #endregion
    private IGameStates _gameGameState;
    private IReward reward;

    private void Awake()
    {
        _gameGameState = GetComponent<IGameStates>();
        reward = GetComponent<IReward>();
        if(rigidbody2D == null) rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _gameGameState.ResetSpinCounts();
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
        reward.GetReward(); 
        isRotating = false; 
        time = 0;
        
    }
    private void StopWheel()
    {
        rigidbody2D.angularVelocity -= StopPower * Time.fixedDeltaTime;
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
}