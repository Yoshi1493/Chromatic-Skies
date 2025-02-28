using UnityEngine.EventSystems;

public class PlayerSelectButton : MenuButton, ISelectHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        AudioManager.Instance.PlayAudio("menu_hover", AudioType.Sound);
    }
}