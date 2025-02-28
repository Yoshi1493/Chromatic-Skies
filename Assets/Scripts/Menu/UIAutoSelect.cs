using UnityEngine;
using UnityEngine.EventSystems;

public class UIAutoSelect : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, ISelectHandler
{
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);

        AudioManager.Instance.PlayAudio("menu_hover", AudioType.Sound, true);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            AudioManager.Instance.PlayAudio("menu_select", AudioType.Sound, true);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        AudioManager.Instance.PlayAudio("menu_hover", AudioType.Sound, true);
    }
}