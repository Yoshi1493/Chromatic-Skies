using System;
using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class ResultsScreen : MonoBehaviour
{
    IEnumerator popupCoroutine;
    public event Action ResultsPopupAction;

    Canvas canvas;
    CanvasGroup canvasGroup;

    void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();

        Enemy enemy = FindObjectOfType<Enemy>();
        enemy.DeathAction += OnEnemyDie;
    }

    void OnEnable()
    {
        ResultsPopupAction?.Invoke();
    }

    void OnEnemyDie()
    {
        enabled = true;
        canvas.enabled = true;

        if (popupCoroutine != null)
        {
            StopCoroutine(popupCoroutine);
        }

        popupCoroutine = DisplayResults();
        StartCoroutine(popupCoroutine);
    }

    IEnumerator DisplayResults()
    {
        yield return WaitForSeconds(5f);

        float currentLerpTime = 0f;
        float totalLerpTime = 0.25f;

        while (currentLerpTime < totalLerpTime)
        {
            canvasGroup.alpha = currentLerpTime / totalLerpTime;

            yield return EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }

        canvasGroup.alpha = 1f;
    }
}