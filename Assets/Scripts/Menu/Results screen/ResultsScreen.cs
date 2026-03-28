using System;
using System.Collections;
using UnityEngine;
using TMPro;
using static CoroutineHelper;

public class ResultsScreen : Menu
{
    IEnumerator popupCoroutine;

    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] InputHandler inputHandler;

    [Space]

    [SerializeField] FloatObject elapsedTime;
    float totalTime;
    [SerializeField] IntObject playerGraze;
    [SerializeField] IntObject hitsTaken;

    [Space]

    [SerializeField] TextMeshProUGUI[] resultsTexts;
    [SerializeField] TextMeshProUGUI[] resultsValues;

    [SerializeField] GameObject confirmButton;

    Boss boss;

    protected override void Awake()
    {
        base.Awake();

        InitializeCanvasElements();
        boss = FindAnyObjectByType<Boss>();
    }

    void InitializeCanvasElements()
    {
        canvasGroup.alpha = 0f;

        foreach (var item in resultsTexts)
        {
            item.enabled = false;
        }

        foreach (var item in resultsValues)
        {
            item.enabled = false;
        }
    }

    void Start()
    {
        if (boss != null)
        {
            boss.LoseLifeAction += OnBossLoseLife;
            boss.DeathAction += OnBossDie;
        }

        Close();
    }

    void OnBossLoseLife()
    {
        totalTime += elapsedTime.value;
    }

    void OnBossDie()
    {
        Open(confirmButton);
        inputHandler.enabled = true;

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
        InitializeResults();

        yield return WaitForSeconds(4f);

        float currentLerpTime = 0f;
        float totalLerpTime = 0.25f;

        while (currentLerpTime < totalLerpTime)
        {
            canvasGroup.alpha = currentLerpTime / totalLerpTime;

            yield return null;
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

        confirmButton.SetActive(true);
    }

    void InitializeResults()
    {
        resultsValues[0].text = TimeSpan.FromSeconds(totalTime).ToString(Clock.StringFormat);
        resultsValues[2].text = playerGraze.value.ToString();
        resultsValues[3].text = hitsTaken.value.ToString();
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

                confirmButton.SetActive(true);
            }
        }
    }

}