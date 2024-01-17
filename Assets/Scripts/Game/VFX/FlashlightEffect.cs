using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class FlashlightEffect : MonoBehaviour
{
    Camera mainCam;
    Material flashlightMat;

    Player playerShip;
    IEnumerator strengthCoroutine;
    IEnumerator radiusCoroutine;
    IEnumerator hardnessCoroutine;

    const float DefaultStrength = 0.0f;
    const float DefaultRadius = 0.5f;
    const float DefaultHardness = 1.5f;

    void Awake()
    {
        mainCam = Camera.main;
        flashlightMat = GetComponent<SpriteRenderer>().material;

        playerShip = FindObjectOfType<Player>();

        if (playerShip != null)
        {
            playerShip.LoseLifeAction += () => enabled = false;
        }

        ResetProperties();
    }

    void OnEnable()
    {
        SetStengthOverTime(4f, 8f);
    }

    //follow player position
    void Update()
    {
        //convert player position -> viewport space ([0,0], [1,1]) -> shadergraph coordinate space ([-1,-1], [1,1])
        Vector2 position = (2 * mainCam.WorldToViewportPoint(playerShip.transform.position)) - Vector3.one;
        flashlightMat.SetVector("_Position", position);
    }

    void OnDisable()
    {
        SetStengthOverTime(0f, 0.5f);
    }

    #region Coroutines

    public void SetStengthOverTime(float endStrength, float duration)
    {
        if (duration <= 0f)
        {
            if (duration == 0f) flashlightMat.SetFloat("_Strength", endStrength);
            return;
        }

        if (strengthCoroutine != null)
        {
            StopCoroutine(strengthCoroutine);
        }

        strengthCoroutine = FadeStrength(endStrength, duration);
        StartCoroutine(strengthCoroutine);
    }

    IEnumerator FadeStrength(float endStrength, float fadeDuration)
    {
        float currentLerpTime = 0f;
        float startStrength = flashlightMat.GetFloat("_Strength");

        while (currentLerpTime < fadeDuration)
        {
            float lerpProgress = currentLerpTime / fadeDuration;
            float strength = Mathf.Lerp(startStrength, endStrength, lerpProgress);
            flashlightMat.SetFloat("_Strength", strength);

            yield return EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }

        flashlightMat.SetFloat("_Strength", endStrength);
    }

    public void SetRadiusOverTime(float endRadius, float duration)
    {
        if (duration <= 0f)
        {
            if (duration == 0f) flashlightMat.SetFloat("_Radius", endRadius);
            return;
        }

        if (radiusCoroutine != null)
        {
            StopCoroutine(radiusCoroutine);
        }

        radiusCoroutine = FadeRadius(endRadius, duration);
        StartCoroutine(radiusCoroutine);
    }

    IEnumerator FadeRadius(float endRadius, float fadeDuration)
    {
        float currentLerpTime = 0f;
        float startRadius = flashlightMat.GetFloat("_Radius");

        while (currentLerpTime < fadeDuration)
        {
            float lerpProgress = currentLerpTime / fadeDuration;
            float radius = Mathf.Lerp(startRadius, endRadius, lerpProgress);
            flashlightMat.SetFloat("_Radius", radius);

            yield return EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }

        flashlightMat.SetFloat("_Radius", endRadius);
    }

    public void SetHardnessOverTime(float endHardness, float duration)
    {
        if (duration <= 0f)
        {
            if (duration == 0f) flashlightMat.SetFloat("_Hardness", endHardness);
            return;
        }

        if (hardnessCoroutine != null)
        {
            StopCoroutine(hardnessCoroutine);
        }

        hardnessCoroutine = FadeHardness(endHardness, duration);
        StartCoroutine(hardnessCoroutine);
    }

    IEnumerator FadeHardness(float endHardness, float fadeDuration)
    {
        float currentLerpTime = 0f;
        float startHardness = flashlightMat.GetFloat("_Hardness");

        while (currentLerpTime < fadeDuration)
        {
            float lerpProgress = currentLerpTime / fadeDuration;
            float hardness = Mathf.Lerp(startHardness, endHardness, lerpProgress);
            flashlightMat.SetFloat("_Hardness", hardness);

            yield return EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }

        flashlightMat.SetFloat("_Hardness", endHardness);
    }

    #endregion

    void ResetProperties()
    {
        flashlightMat.SetFloat("_Strength", DefaultStrength);
        flashlightMat.SetFloat("_Radius", DefaultRadius);
        flashlightMat.SetFloat("_Hardness", DefaultHardness);
    }

#if UNITY_EDITOR
    void OnApplicationQuit()
    {
        ResetProperties();
    }
#endif
}