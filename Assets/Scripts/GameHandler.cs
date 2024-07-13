using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public event EventHandler OnStateChanged;
    public static GameHandler Instance { get; private set; }
    public enum State
    {
        OnWaitingCountDown,
        OnStartingCountDown,
        OnPlay,
        OnGameOver
    }

    private State state;
    [SerializeField] private float onWaitingCountDownTimerMax = 1f;
    [SerializeField] private float onStartingCountDownTimerMax = 3f;
    [SerializeField] private float onPlayTimerMax = 10f;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        state = State.OnWaitingCountDown;
    }

    private void Update()
    {
        switch (state)
        {
            case State.OnWaitingCountDown:
                onWaitingCountDownTimerMax -= Time.deltaTime;
                if(onWaitingCountDownTimerMax < 0f)
                {
                    state = State.OnStartingCountDown;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.OnStartingCountDown:
                onStartingCountDownTimerMax -= Time.deltaTime;  
                if(onStartingCountDownTimerMax < 0f)
                {
                    state = State.OnPlay;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.OnPlay:
                onPlayTimerMax -= Time.deltaTime;
                if(onPlayTimerMax < 0f)
                {
                    state = State.OnGameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.OnGameOver:
                break;
        }
        Debug.Log(state);
    }

    public bool IsOnPlay()
    {
        return state == State.OnPlay;
    }

    public bool IsOnStartingCountDown()
    {
        return state == State.OnStartingCountDown;
    }

    public float GetCountDownTimer()
    {
        return onStartingCountDownTimerMax;
    }
}
