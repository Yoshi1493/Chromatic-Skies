using System;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{
    public static bool IsPaused { get; private set; }
    public event Action<bool> GamePauseAction;

    Ship[] ships;

    void Awake()
    {
        GamePauseAction += OnGamePaused;

        ships = FindObjectsOfType<Ship>();
        foreach (var ship in ships)
        {
            ship.DeathAction += OnShipDie;
        }

        IsPaused = false;
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
        AudioManager.Instance.PlaySound("game_pause");
    }

    void OnGamePaused(bool state)
    {
        IsPaused = state;
        Time.timeScale = state ? 0 : 1;
    }

    void OnShipDie() => enabled = false;

    void OnDestroy()
    {
        foreach (var ship in ships)
        {
            ship.DeathAction -= OnShipDie;
        }

        IsPaused = false;
    }
}