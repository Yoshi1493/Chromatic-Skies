using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class MenuSlider : MonoBehaviour
{
    Slider slider;

    void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(delegate { OnSliderValueChange(); });
    }

    void OnSliderValueChange()
    {
        AudioManager.Instance.PlayAudio("menu_hover", AudioType.Sound, true);
    }

    void OnDestroy()
    {
        slider.onValueChanged.RemoveAllListeners();
    }
}