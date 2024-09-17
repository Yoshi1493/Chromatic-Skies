using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Canvas))]
public abstract class Menu : MonoBehaviour
{
    protected Canvas thisMenu;
    protected PointerEventData eventData = new PointerEventData(EventSystem.current);

    protected virtual void Awake()
    {
        thisMenu = GetComponent<Canvas>();
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

    public void LoadScene(int sceneIndex)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();        
#endif
    }
}