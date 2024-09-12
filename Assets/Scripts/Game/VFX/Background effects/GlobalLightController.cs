using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static CoroutineHelper;

[RequireComponent(typeof(Light2D))]
public class GlobalLightController : MonoBehaviour
{
    Light2D globalLight;

    IEnumerator intensityCoroutine;
    const float LightIntensityAnimationDuration = 3.5f;

    [SerializeField] AnimationCurve lightIntensityInterpolation;

    void Awake()
    {
        globalLight = GetComponent<Light2D>();

        Player player = FindObjectOfType<Player>();
        Enemy enemy = FindObjectOfType<Enemy>();

        if (player != null)
        {
            player.LoseLifeAction += ResetIntensity;
        }
        if (enemy != null)
        {
            enemy.LoseLifeAction += ResetIntensity;
            enemy.DeathAction += OnEnemyDie;
        }
    }

    void OnEnemyDie()
    {
        FadeIntensity(4f, 3.5f, lightIntensityInterpolation);
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

            yield return EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }

        globalLight.intensity = endIntensity;
    }

    void OnApplicationQuit()
    {
        globalLight.intensity = 1f;
    }
}