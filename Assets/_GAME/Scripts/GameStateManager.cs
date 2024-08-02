using System;
using System.Collections;
using System.Collections.Generic;
using Supyrb;
using UnityEngine;

public enum GameState
{
    None = 0,
    MainMenu = 1,
    Combat = 2,
    WaveOver = 3,
}
public class GameStateManager : MonoBehaviour
{
    public GameState GameState;

    private void Awake()
    {
        GameState = GameState.WaveOver;
        Debug.Log($"GameState: " + GameState);
    }

    private void OnEnable()
    {
        Signals.Get<WaveStartedSignal>().AddListener(OnWaveStarted);
        Signals.Get<WaveEndedSignal>().AddListener(OnWaveEnded);
    }

    private void OnDisable()
    {
        Signals.Get<WaveStartedSignal>().RemoveListener(OnWaveStarted);
        Signals.Get<WaveEndedSignal>().RemoveListener(OnWaveEnded);
    }

    private void OnWaveStarted()
    {
        GameState = GameState.Combat;
        Debug.Log($"GameState: " + GameState);
    }
    
    private void OnWaveEnded()
    {
        GameState = GameState.WaveOver;
        Debug.Log($"GameState: " + GameState);
    }
}
