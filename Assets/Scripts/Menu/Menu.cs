using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Canvas))]
public abstract class Menu : MonoBehaviour
{
    protected Canvas thisMenu;
    protected PointerEventData eventData = new(EventSystem.current);

    [SerializeField] InputActionAsset inputActions;
    protected InputAction submitInput;
    protected InputAction backInput;

    protected virtual void Awake()
    {
        thisMenu = GetComponent<Canvas>();
        submitInput = inputActions.FindActionMap("UI").FindAction("Submit");
        backInput = inputActions.FindActionMap("UI").FindAction("Cancel");
    }

    public void Open(GameObject newSelectedGameObject)
    {
        thisMenu.enabled = true;

        if (thisMenu.TryGetComponent(out Menu m))
            m.Enable(newSelectedGameObject);
    }

    public void Close()
    {
        thisMenu.enabled = false;
        Disable();
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