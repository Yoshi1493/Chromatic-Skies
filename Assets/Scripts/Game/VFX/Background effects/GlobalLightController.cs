using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static CoroutineHelper;

[RequireComponent(typeof(Light2D))]
public class GlobalLightController : MonoBehaviour
{
    Light2D globalLight;

    IEnumerator intensityCoroutine;
    const float LightIntensityAnimationDuration = 2f;

    [SerializeField] AnimationCurve lightIntensityInterpolation;

    Player player;
    Boss boss;

    void Awake()
    {
        globalLight = GetComponent<Light2D>();

        player = FindObjectOfType<Player>();
        boss = FindObjectOfType<Boss>();
    }

    void Start()
    {
        player.LoseLifeAction += ResetIntensity;

        boss.LoseLifeAction += ResetIntensity;
        boss.DeathAction += OnBossDie;
    }

    void OnBossDie()
    {
        FadeIntensity(2f, LightIntensityAnimationDuration, lightIntensityInterpolation);
    }

    void ResetIntensity()
    {
        FadeIntensity(1f, 1f, AnimationCurve.EaseInOut(0f, 0f, 1f, 1f));
    }

    public void FadeIntensity(float endIntensity, float lerpDuration, AnimationCurve interpolationCurve)
    {
        if (intensityCoroutine != null)
        {
            StopCoroutine(intensityCoroutine);
        }

        intensityCoroutine = _FadeIntensity(endIntensity, lerpDuration, interpolationCurve);
        StartCoroutine(intensityCoroutine);
    }

    IEnumerator _FadeIntensity(float endIntensity, float lerpDuration, AnimationCurve interpolationCurve)
    {
        float currentLerpTime = 0f;
        float startIntensity = globalLight.intensity;

        while (currentLerpTime < lerpDuration)
        {
            float t = interpolationCurve.Evaluate(currentLerpTime / lerpDuration);

            globalLight.intensity = Mathf.Lerp(startIntensity, endIntensity, t);

            yield return null;
            currentLerpTime += Time.deltaTime;
        }
    }

    void OnApplicationQuit()
    {
        globalLight.intensity = 1f;
    }
}