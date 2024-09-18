using UnityEngine;
using UnityEngine.UI;

public class MainMenuConfirm : Menu
{
    [SerializeField] Button backButton;

    protected override void Awake()
    {
        base.Awake();
        FindObjectOfType<PauseHandler>().GamePauseAction += OnGamePaused;
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
        if (Input.GetButtonDown("Cancel"))
        {
            backButton.OnPointerClick(eventData);
        }
    }
}