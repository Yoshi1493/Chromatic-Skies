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
        musicSlider.value = userSettings.MusicVolume * 10f;
        soundSlider.value = userSettings.SoundVolume * 10f;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            backButton.OnPointerClick(eventData);
    }

    public void OnChangeMusicVolume(TextMeshProUGUI tmp)
    {
        userSettings.MusicVolume = musicSlider.value * 0.1f;
        tmp.text = musicSlider.value.ToString();
    }

    public void OnChangeSoundVolume(TextMeshProUGUI tmp)
    {
        userSettings.SoundVolume = soundSlider.value * 0.1f;
        tmp.text = soundSlider.value.ToString();
    }
}