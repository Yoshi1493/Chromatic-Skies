using System;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    public bool IsPaused { get; private set; }
    public event Action<bool> GamePauseAction;

    ResultsScreen resultsScreen;

    void Awake()
    {
        GamePauseAction += OnGamePaused;

        resultsScreen = FindObjectOfType<ResultsScreen>();
        resultsScreen.ResultsPopupAction += () => enabled = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))        
            SetGamePaused(!IsPaused);        
    }

    public void SetGamePaused(bool pauseState)
    {
        GamePauseAction?.Invoke(pauseState);
    }

    void OnGamePaused(bool state)
    {
        IsPaused = state;
        Time.timeScale = state ? 0 : 1;
    }
}