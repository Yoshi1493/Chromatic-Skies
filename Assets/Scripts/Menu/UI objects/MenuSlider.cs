using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Slider))]
public class MenuSlider : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(delegate { OnSliderValueChange(); });
    }

    void OnSliderValueChange()
    {
        AudioManager.Instance.PlaySound("menu_hover", true);
    }

    void OnDestroy()
    {
        slider.onValueChanged.RemoveAllListeners();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (InputHandler.lastSelectedGameObject != gameObject)
        {
            AudioManager.Instance.PlaySound("menu_hover", true);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (InputHandler.lastSelectedGameObject != gameObject)
        {
            AudioManager.Instance.PlaySound("menu_hover", true);
        }
    }
}