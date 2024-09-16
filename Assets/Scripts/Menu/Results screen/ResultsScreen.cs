using System;
using System.Collections;
using UnityEngine;
using TMPro;
using static CoroutineHelper;

public class ResultsScreen : MonoBehaviour
{
    IEnumerator popupCoroutine;
    public event Action ResultsPopupAction;
    public event Action ResultsFinishDisplayAction;
         
    Canvas canvas;
    CanvasGroup canvasGroup;

    [SerializeField] TextMeshProUGUI[] resultsTexts;
    [SerializeField] TextMeshProUGUI[] resultsValues;

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
        canvasGroup.alpha = 0f;

        if (popupCoroutine != null)
        {
            StopCoroutine(popupCoroutine);
        }

        popupCoroutine = DisplayResults();
        StartCoroutine(popupCoroutine);
    }

    IEnumerator DisplayResults()
    {
        yield return WaitForSeconds(4f);

        float currentLerpTime = 0f;
        float totalLerpTime = 0.25f;

        while (currentLerpTime < totalLerpTime)
        {
            canvasGroup.alpha = currentLerpTime / totalLerpTime;

            yield return EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }

        canvasGroup.alpha = 1f;



        ResultsFinishDisplayAction?.Invoke();
    }
}