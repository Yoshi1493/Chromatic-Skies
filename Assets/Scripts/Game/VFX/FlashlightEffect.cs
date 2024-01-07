using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class FlashlightEffect : MonoBehaviour
{
    Camera mainCam;
    Material flashlightMat;

    Player playerShip;
    IEnumerator fadeCoroutine;

    void Awake()
    {
        mainCam = Camera.main;
        flashlightMat = GetComponent<SpriteRenderer>().material;

        playerShip = FindObjectOfType<Player>();

        if (playerShip != null)
        {
            playerShip.LoseLifeAction += () => enabled = false;
        }
    }

    void OnEnable()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = FadeFlashlight(4f, 8f);
        StartCoroutine(fadeCoroutine);
    }

    IEnumerator FadeFlashlight(float endStrength, float fadeDuration)
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

    void Update()
    {
        //follow player position
        Vector2 position = (2 * mainCam.WorldToViewportPoint(playerShip.transform.position)) - Vector3.one;
        flashlightMat.SetVector("_Position", position);
    }

    void OnDisable()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        fadeCoroutine = FadeFlashlight(0f, 0.5f);
        StartCoroutine(fadeCoroutine);
    }

#if UNITY_EDITOR
    void OnApplicationQuit()
    {
        flashlightMat.SetFloat("_Strength", 0f);
    }
#endif
}