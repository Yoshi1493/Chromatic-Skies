using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : Menu, ISavable
{
    [SerializeField] Button backButton;

    [Header("Settings elements")]

    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundSlider;

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            backButton.OnPointerClick(eventData);
        }
    }

    public void OnChangeMusicVolume(TextMeshProUGUI tmp)
    {
        tmp.text = musicSlider.value.ToString();
    }

    public void OnChangeSoundVolume(TextMeshProUGUI tmp)
    {
        tmp.text = soundSlider.value.ToString();
    }

    #region Interface impl.

    void ISavable.LoadData(UserData data)
    {
        musicSlider.value = data.MusicVolume * 10f;
        soundSlider.value = data.SoundVolume * 10f;
    }

    void ISavable.SaveData(UserData data)
    {
        data.MusicVolume = musicSlider.value * 0.1f;
        data.SoundVolume = soundSlider.value * 0.1f;
    }

    #endregion
}