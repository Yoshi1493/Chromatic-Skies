using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class MenuButton : MonoBehaviour, ISelectHandler
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
        AudioManager.Instance.PlaySound(onSelectSoundEffect, true);
    }

    void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (InputHandler.lastSelectedGameObject != gameObject)
        {
            AudioManager.Instance.PlaySound("menu_hover", true);
        }
    }
}