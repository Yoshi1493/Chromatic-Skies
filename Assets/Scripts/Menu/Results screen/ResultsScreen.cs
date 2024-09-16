using System;
using System.Collections;
using UnityEngine;
using TMPro;
using static CoroutineHelper;

public class ResultsScreen : MonoBehaviour
{
    IEnumerator popupCoroutine;
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

        InitializeCanvasElements();
    }

    void InitializeCanvasElements()
    {
        canvas.enabled = false;
        canvasGroup.alpha = 0f;

        foreach (var item in resultsTexts)
        {
            item.enabled = false;
        }

        foreach (var item in resultsValues)
        {
            item.enabled = false;
        }

        enabled = false;
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

        yield return WaitForSeconds(1f);

        for (int i = 0; i < resultsTexts.Length; i++)
        {
            resultsTexts[i].enabled = true;
        }

        for (int i = 0; i < resultsValues.Length; i++)
        {
            yield return WaitForSeconds(0.5f);
            resultsValues[i].enabled = true;
        }

        yield return WaitForSeconds(1f);

        ResultsFinishDisplayAction?.Invoke();
    }

    void Update()
    {
        if (Input.GetButtonDown("Shoot"))
        {
            //if results are in the process of being displayed, immediately display all results
            if (resultsValues[0].enabled && !resultsValues[^1].enabled)
            {
                StopAllCoroutines();

                foreach (var item in resultsTexts)
                {
                    item.enabled = true;
                }

                foreach (var item in resultsValues)
                {
                    item.enabled = true;
                }

                ResultsFinishDisplayAction?.Invoke();
            }
        }
    }
}