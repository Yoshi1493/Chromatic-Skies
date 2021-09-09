using UnityEngine;

public class MainMenuConfirm : Menu
{
    protected override void Awake()
    {
        base.Awake();
        FindObjectOfType<PauseHandler>().GamePauseAction += OnGamePaused;
    }

    void OnGamePaused(bool state)
    {
        if (!state)
            Close();
    }
}