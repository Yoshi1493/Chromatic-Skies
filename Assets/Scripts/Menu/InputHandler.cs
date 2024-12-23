using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    GameObject lastSelectedGameObject;

    PauseHandler pauseHandler;

    void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == (int)SceneIndexes.Game)
        {
            pauseHandler = FindObjectOfType<PauseHandler>();
            pauseHandler.GamePauseAction += OnGamePaused;
        }
    }

    void Update()
    {
        GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;

        if (currentSelectedGameObject != null)
        {
            lastSelectedGameObject = currentSelectedGameObject;
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(lastSelectedGameObject);
        }
    }

    void OnGamePaused(bool state)
    {
        enabled = state;
    }
}