using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseHandler : MonoBehaviour
{
    public static bool IsPaused { get; private set; }
    public event Action<bool> GamePauseAction;

    [SerializeField] InputActionAsset inputActions;
    InputAction pauseAction;
    
    Ship[] ships;

    void Awake()
    {
        GamePauseAction += OnGamePaused;

        ships = FindObjectsByType<Ship>(FindObjectsSortMode.None);
        foreach (var ship in ships)
        {
            ship.DeathAction += OnShipDie;
        }

        IsPaused = false;

        pauseAction = inputActions.FindActionMap("Pause").FindAction("Pause");
    }

    void Update()
    {
        if (pauseAction.WasPressedThisFrame())
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