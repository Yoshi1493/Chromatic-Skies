using UnityEngine;
using UnityEngine.EventSystems;

public class UIAutoSelect : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);

        AudioManager.Instance.PlayAudio("menu_hover", AudioType.Sound, true);
    }

    public void OnSelect(BaseEventData eventData)
    {
        AudioManager.Instance.PlayAudio("menu_hover", AudioType.Sound, true);
    }
}