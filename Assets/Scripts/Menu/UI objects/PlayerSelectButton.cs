using UnityEngine.EventSystems;

public class PlayerSelectButton : MenuButton, ISelectHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        if (InputHandler.lastSelectedGameObject != gameObject)
        {
            AudioManager.Instance.PlayAudio("menu_hover", AudioType.Sound);
        }
    }
}