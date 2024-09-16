using System;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    public bool IsPaused { get; private set; }
    public event Action<bool> GamePauseAction;

    Enemy enemy;

    void Awake()
    {
        GamePauseAction += OnGamePaused;

        enemy = FindObjectOfType<Enemy>();
        enemy.DeathAction += () => enabled = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            SetGamePaused(!IsPaused);
        }
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