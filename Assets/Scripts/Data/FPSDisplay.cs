using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI fpsText;

    bool showFps = true;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (Time.frameCount % 30 == 0)
        {
            fpsText.text = $"{1 / Time.unscaledDeltaTime:F1} fps";
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            showFps = !showFps;
            canvasGroup.alpha = showFps ? 1 : 0;
        }
    }
}