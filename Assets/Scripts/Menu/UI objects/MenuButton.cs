using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuButton : MonoBehaviour
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
}