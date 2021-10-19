using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : Menu
{
    [SerializeField] UserSettings userSettings;

    [SerializeField] GameObject resumeButton;

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
            Open(resumeButton);

        else
        {
            Close();
            EventSystem.current.SetSelectedGameObject(resumeButton);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            pauseHandler.SetGamePaused(false);
            Close();
        }
    }
}