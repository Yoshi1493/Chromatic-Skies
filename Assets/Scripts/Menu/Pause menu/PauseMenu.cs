using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenu : Menu
{
    PauseHandler pauseHandler;
    [SerializeField] GameObject resumeButton;

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
            EventSystem.current.SetSelectedGameObject(null);
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