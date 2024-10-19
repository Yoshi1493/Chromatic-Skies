using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class SettingsMenu : Menu, ISavable
{
    [SerializeField] Button backButton;

    [Header("Settings elements")]

    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundSlider;
    [SerializeField] Slider backgroundDimSlider;

    PauseHandler pauseHandler;

    protected override void Awake()
    {
        base.Awake();

        if (SceneManager.GetActiveScene().buildIndex == (int)SceneIndexes.Game)
        {
            pauseHandler = FindObjectOfType<PauseHandler>();
        }
    }

    void Start ()
    {
        if (pauseHandler != null)
        {
            pauseHandler.GamePauseAction += OnGamePaused;
        }
    }

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

    public void OnChangeBackgroundDim(TextMeshProUGUI tmp)
    {
        tmp.text = $"{backgroundDimSlider.value}%";
    }

    #region Interface impl.

    void ISavable.LoadData(UserData data)
    {
        musicSlider.value = data.MusicVolume * 10f;
        soundSlider.value = data.SoundVolume * 10f;
        backgroundDimSlider.value = data.BackgroundDim * 100f;
    }

    void ISavable.SaveData(UserData data)
    {
        data.MusicVolume = musicSlider.value * 0.1f;
        data.SoundVolume = soundSlider.value * 0.1f;
        data.BackgroundDim = backgroundDimSlider.value * 0.01f;
    }

    #endregion

    void OnGamePaused(bool state)
    {
        if (!state)
        {
            Close();
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}