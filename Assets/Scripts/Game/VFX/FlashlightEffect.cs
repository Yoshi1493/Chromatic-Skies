using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static CoroutineHelper;

public class FlashlightEffect : MonoBehaviour
{
    Camera mainCam;

    [SerializeField] VolumeProfile volumeProfile;
    Vignette vignette;
    Vector2 vignetteCentre;

    IEnumerator fadeCoroutine;

    Player playerShip;

    void Awake()
    {
        mainCam = Camera.main;

        volumeProfile.TryGet(out vignette);
        vignette.intensity.value = 0f;

        playerShip = FindObjectOfType<Player>();
    }

    void OnEnable()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = FadeVignette(0f, 1f, 5f);
    }

    IEnumerator FadeVignette(float startIntensity, float endIntensity, float fadeDuration)
    {
        volumeProfile.TryGet(out vignette);

        if (vignette != null)
        {
            float currentLerpTime = 0f;

            vignette.intensity.value = startIntensity;

            while (vignette.intensity.value != endIntensity)
            {
                float lerpProgress = currentLerpTime / fadeDuration;
                vignette.intensity.value = Mathf.Lerp(startIntensity, endIntensity, lerpProgress);

                yield return EndOfFrame;
                currentLerpTime += Time.deltaTime;
            }

            vignette.intensity.value = endIntensity;
        }

        yield return EndOfFrame;
    }

    void Update()
    {
        //follow player position
        vignette.center.value = mainCam.WorldToViewportPoint(playerShip.transform.position);
    }

    void ResetteVignette()
    {
        vignette.intensity.value = 0;
        vignette.center.value = 0.5f * Vector2.one;
    }

    void OnDisable()
    {
        ResetteVignette();
    }

#if UNITY_EDITOR
    void OnApplicationQuit()
    {
        ResetteVignette();
    }
#endif
}