using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static CoroutineHelper;

[RequireComponent(typeof(Light2D))]
public class ShipDeathLightController : MonoBehaviour
{
    Light2D globalLight;
    IEnumerator intensityCoroutine;
    const float LightIntensityAnimationDuration = 3.5f;

    [SerializeField] AnimationCurve lightIntensityInterpolation;

    void Awake()
    {
        globalLight = GetComponent<Light2D>();

        Enemy enemy = FindObjectOfType<Enemy>();

        if (enemy != null)
        {
            enemy.DeathAction += OnEnemyDie;
        }
        else
        {
            //Debug.LogError("No Enemy object found.");
        }
    }

    void OnEnemyDie()
    {
        if (intensityCoroutine != null)
        {
            StopCoroutine(intensityCoroutine);
        }

        intensityCoroutine = _OnEnemyDie();
        StartCoroutine(intensityCoroutine);
    }

    IEnumerator _OnEnemyDie()
    {
        float currentLerpTime = 0f;

        while (currentLerpTime < LightIntensityAnimationDuration)
        {
            float t = lightIntensityInterpolation.Evaluate(currentLerpTime / LightIntensityAnimationDuration);

            globalLight.intensity = Mathf.LerpUnclamped(0f, 1f, t);

            yield return EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }
    }
}