using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : Menu
{
    [SerializeField] UserSettings userSettings;

    [SerializeField] Button backButton;

    PauseHandler pauseHandler;

    protected override void Awake()
    {
        base.Awake();
        pauseHandler = GetComponent<PauseHandler>();
        pauseHandler.GamePauseAction += OnGamePaused;
    }

    void OnGamePaused(bool state)
    {
        if (state)
            Open(backButton.gameObject);

        else
            Close();
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            pauseHandler.OnGamePaused(false);
    }
}