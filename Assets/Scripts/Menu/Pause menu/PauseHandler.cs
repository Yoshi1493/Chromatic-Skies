using System;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    [HideInInspector] public bool isPaused;
    public event Action<bool> GamePauseAction;

    void Awake()
    {
        GamePauseAction += OnGamePaused;
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))        
            SetGamePaused(!isPaused);        
    }

    public void SetGamePaused(bool pauseState)
    {
        GamePauseAction?.Invoke(pauseState);
    }

    void OnGamePaused(bool state)
    {
        isPaused = state;
        Time.timeScale = state ? 0 : 1;
    }
}