using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuButton : MonoBehaviour
{
    Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        AudioManager.Instance.PlayAudio("menu_select", AudioType.Sound, true);
    }

    void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}