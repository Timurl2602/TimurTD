using System;
using System.Collections.Generic;
using Supyrb;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public PlayerInputActions PlayerInputActions;
    
    private void Awake()
    {
        PlayerInputActions = new PlayerInputActions();
        PlayerInputActions.Gameplay.Enable();
    }

    private void OnEnable()
    {
        Signals.Get<BuildModeStartedSignal>().AddListener(OnBuildModeStarted);
        Signals.Get<BuildModeStoppedSignal>().AddListener(OnBuildModeStopped);
    }

    private void OnDisable()
    {
        Signals.Get<BuildModeStartedSignal>().RemoveListener(OnBuildModeStarted);
        Signals.Get<BuildModeStoppedSignal>().RemoveListener(OnBuildModeStopped);
    }

    private void OnBuildModeStarted()
    { 
        PlayerInputActions.Gameplay.Disable(); 
        PlayerInputActions.BuildMode.Enable();
    }
    
    private void OnBuildModeStopped()
    {
        PlayerInputActions.Gameplay.Enable();
        PlayerInputActions.BuildMode.Disable();
    }
}
