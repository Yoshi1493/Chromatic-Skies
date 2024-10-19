using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static CoroutineHelper;

public class GameOverMenu : Menu
{
    IEnumerator popupCoroutine;
    public event Action FinishDisplayAction;

    CanvasGroup canvasGroup;

    [SerializeField] TextMeshProUGUI[] continueButtons;

    Player player;

    protected override void Awake()
    {
        base.Awake();

        player = FindObjectOfType<Player>();
        InitializeCanvasElements();
    }

    void Start()
    {
        player.DeathAction += OnPlayerDie;
    }

    void InitializeCanvasElements()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        thisMenu.enabled = false;
        canvasGroup.alpha = 0f;

        foreach (var item in continueButtons)
        {
            item.enabled = false;
        }

        enabled = false;
    }

    void OnPlayerDie()
    {
        if (popupCoroutine != null)
        {
            StopCoroutine(popupCoroutine);
        }

        popupCoroutine = DisplayGameOver();
        StartCoroutine(popupCoroutine);
    }

    IEnumerator DisplayGameOver()
    {
        yield return WaitForSeconds(4f);

        Open(continueButtons[0].gameObject);

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

        for (int i = 0; i < continueButtons.Length; i++)
        {
            continueButtons[i].enabled = true;
        }
    }
}