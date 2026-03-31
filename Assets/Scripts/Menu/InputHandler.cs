using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    public static GameObject lastSelectedGameObject;

    PauseHandler pauseHandler;

    [SerializeField] InputActionAsset inputActions;
    InputActionMap playerMap;
    InputActionMap pauseMap;
    InputActionMap uiMap;
    

    void Awake()
    {
        playerMap = inputActions.FindActionMap("Player");
        pauseMap = inputActions.FindActionMap("Pause");
        uiMap = inputActions.FindActionMap("UI");

        if (SceneManager.GetActiveScene().buildIndex == (int)SceneIndexes.Menu)
        {
            uiMap.Enable();
            playerMap.Disable();
            pauseMap.Disable();
        }
        else
        {
            playerMap.Enable();
            pauseMap.Enable();
            uiMap.Disable();
            
            pauseHandler = FindAnyObjectByType<PauseHandler>();
            pauseHandler.GamePauseAction += OnGamePaused;
        }        
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    [RuntimeInitializeOnLoadMethod]
    static void OnSceneLoad()
    {
        lastSelectedGameObject = EventSystem.current.firstSelectedGameObject;
    }

    void Update()
    {
        GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;

        if (currentSelectedGameObject != null)
        {
            if (lastSelectedGameObject != currentSelectedGameObject)
            {
                lastSelectedGameObject = currentSelectedGameObject;
            }
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(lastSelectedGameObject);
        }

#if UNITY_EDITOR || UNITY_STANDALONE

        // DetectKeyInput();
        // DetectCursorMovement();

#endif
    }

    void DetectKeyInput()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetMouseButtonDown(0) ||
                Input.GetMouseButtonDown(1) ||
                Input.GetMouseButtonDown(2) ||
                Input.GetMouseButtonDown(3) ||
                Input.GetMouseButtonDown(4) ||
                Input.GetMouseButtonDown(5) ||
                Input.GetMouseButtonDown(6))
            {
                return;
            }

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void DetectCursorMovement()
    {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void OnGamePaused(bool state)
    {
        enabled = state;
    }
}