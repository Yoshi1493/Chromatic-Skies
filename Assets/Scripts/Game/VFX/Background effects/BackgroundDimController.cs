using UnityEngine;
using UnityEngine.UI;

public class BackgroundDimController : MonoBehaviour
{
    SpriteRenderer backgroundSprite;

    [SerializeField] Slider backgroundDimSlider;

    void Awake()
    {
        backgroundSprite = GetComponent<SpriteRenderer>();
        OnChangeBackgroundDim();
    }

    //sets background rgb based on slider value in Settings menu
    //called from Awake as well as changing slider value in Settings menu
    public void OnChangeBackgroundDim()
    {
        float sliderValue = 1f - (backgroundDimSlider.value * 0.01f);
        backgroundSprite.color = new Color(sliderValue, sliderValue, sliderValue);
    }
}