using UnityEngine;
using UnityEngine.EventSystems;

public class UIAutoSelect : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IDeselectHandler
{
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (InputHandler.lastSelectedGameObject != gameObject)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
            AudioManager.Instance.PlayAudio("menu_hover", AudioType.Sound, true);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (InputHandler.lastSelectedGameObject != gameObject)
        {
            AudioManager.Instance.PlayAudio("menu_hover", AudioType.Sound, true);
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {

    }
}