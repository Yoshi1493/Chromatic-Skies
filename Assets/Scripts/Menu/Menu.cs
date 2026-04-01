using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Canvas))]
public abstract class Menu : MonoBehaviour
{
    protected Canvas thisMenu;
    protected PointerEventData eventData = new(EventSystem.current);

    [SerializeField] InputActionAsset inputActions;
    protected InputActionMap uiMap;
    protected InputAction submitInput;
    protected InputAction backInput;

    protected virtual void Awake()
    {
        thisMenu = GetComponent<Canvas>();
        uiMap = inputActions.FindActionMap("UI");
        submitInput = uiMap.FindAction("Submit");
        backInput = uiMap.FindAction("Cancel");
    }

    public void Open(GameObject newSelectedGameObject)
    {
        thisMenu.enabled = true;

        if (thisMenu.TryGetComponent(out Menu m))
        {
            m.Enable(newSelectedGameObject);
        }

        uiMap.Enable();
    }

    public void Close()
    {
        thisMenu.enabled = false;
        Disable();

        uiMap.Disable();
    }

    public virtual void Enable(GameObject newSelectedGameObject)
    {
        if (TryGetComponent(out CanvasGroup cg))
        {
            cg.interactable = true;
            cg.blocksRaycasts = true;
        }

        enabled = true;
        EventSystem.current.SetSelectedGameObject(newSelectedGameObject != null ? newSelectedGameObject : null);
    }

    public virtual void Disable()
    {
        if (TryGetComponent(out CanvasGroup cg))
        {
            cg.interactable = false;
            cg.blocksRaycasts = false;
        }

        enabled = false;
    }
}