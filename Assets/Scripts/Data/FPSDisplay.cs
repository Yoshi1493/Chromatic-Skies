using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    CanvasGroup canvasGroup;

    const int cacheSize = 30;
    float[] fpsSamples = new float[cacheSize];
    int avgFps;
    float currentAvg;

    [SerializeField] TextMeshProUGUI fpsText;
    bool showFps = true;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        float currentFps = 1f / Time.unscaledDeltaTime;
        fpsSamples[avgFps] = currentFps;

        float avg = 0f;

        foreach (var fps in fpsSamples)
        {
            avg += fps;
        }

        currentAvg = avg / cacheSize;
        avgFps = (avgFps + 1) % cacheSize;

        if (Time.frameCount % 30 == 0)
        {
            fpsText.text = $"{currentAvg:F1} fps";
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            showFps = !showFps;
            canvasGroup.alpha = showFps ? 1 : 0;
        }
    }
}