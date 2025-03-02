using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class MenuButton : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    Button button;

    [SerializeField] AudioClip onSelectSoundEffect;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        AudioManager.Instance.PlayAudio(onSelectSoundEffect, AudioType.Sound, true);
    }

    void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (InputHandler.lastSelectedGameObject != gameObject)
        {
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
}