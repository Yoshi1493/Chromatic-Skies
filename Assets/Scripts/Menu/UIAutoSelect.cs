using UnityEngine;
using UnityEngine.EventSystems;

public class UIAutoSelect : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void PlayAudio(AudioClip clip)
    {
        AudioManager.Instance.PlayAudio(clip, AudioType.Sound, true);
    }
}