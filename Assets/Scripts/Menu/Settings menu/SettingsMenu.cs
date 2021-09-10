using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : Menu
{
    [SerializeField] UserSettings userSettings;

    [SerializeField] Button backButton;

    [Header("Settings elements")]

    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundSlider;

    protected override void Awake()
    {
        base.Awake();
        InitSettings();
    }

    void InitSettings()
    {
        musicSlider.value = userSettings.MusicVolume;
        soundSlider.value = userSettings.SoundVolume;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            backButton.OnPointerClick(eventData);
    }

    public void OnChangeMusicVolume(TextMeshProUGUI tmp)
    {
        userSettings.MusicVolume = musicSlider.value;
        tmp.text = userSettings.MusicVolume.ToString();
    }

    public void OnChangeSoundVolume(TextMeshProUGUI tmp)
    {
        userSettings.SoundVolume = soundSlider.value;
        tmp.text = userSettings.SoundVolume.ToString();
    }
}