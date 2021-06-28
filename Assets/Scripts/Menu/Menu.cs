using UnityEngine;

[RequireComponent(typeof(Canvas))]
public abstract class Menu : MonoBehaviour
{
    protected Canvas thisMenu;

    protected virtual void Awake()
    {
        thisMenu = GetComponent<Canvas>();
    }

    public void Open()
    {
        thisMenu.enabled = true;
        if (thisMenu.TryGetComponent(out Menu m)) m.Enable();
    }

    public void Close()
    {
        thisMenu.enabled = false;
        Disable();
    }

    public void Enable()
    {
        if (TryGetComponent(out CanvasGroup cg))
        {
            cg.interactable = true;
            cg.blocksRaycasts = true;
        }

        enabled = true;
    }

    public void Disable()
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
}