using UnityEngine;
using UnityEngine.UI;

public class MainMenuConfirm : Menu
{
    [SerializeField] Button backButton;

    PauseHandler pauseHandler;

    protected override void Awake()
    {
        base.Awake();
        pauseHandler = FindAnyObjectByType<PauseHandler>();
    }

    void Start()
    {
        pauseHandler.GamePauseAction += OnGamePaused;
    }

    void OnGamePaused(bool state)
    {
        if (!state)
        {
            Close();
        }
    }

    void Update()
    {
        if (backInput.WasPressedThisFrame())
        {
            backButton.OnPointerClick(eventData);
        }
    }
}