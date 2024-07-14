using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;
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
    [SerializeField] private float onPlayTimer;

    private bool isPauseGame = false;

    private void Awake()
    {
        Instance = this;
        state = State.OnWaitingCountDown;
    }
    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
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
                    onPlayTimer = onPlayTimerMax;
                    state = State.OnPlay;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.OnPlay:
                onPlayTimer -= Time.deltaTime;
                if(onPlayTimer < 0f)
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

    public bool IsOnGameOver()
    {
        return state == State.OnGameOver;
    }

    public float GetCountDownTimer()
    {
        return onStartingCountDownTimerMax;
    }

    public float GetPlayingTimerNormalized()
    {
        return 1 - (onPlayTimer / onPlayTimerMax);
    }

    public void TogglePauseGame()
    {
        isPauseGame = !isPauseGame;

        if (isPauseGame)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        } else
        {
            Time.timeScale = 1f;
            OnGameUnPaused?.Invoke(this, EventArgs.Empty);
        }
    }
}
