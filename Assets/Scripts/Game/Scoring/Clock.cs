using System;
using System.Collections;
using UnityEngine;
using TMPro;
using static CoroutineHelper;

public class Clock : MonoBehaviour
{
    IEnumerator clock;
    IEnumerator blinkCoroutine;

    public float CurrentTime { get; private set; }
    public bool IsPaused { get; private set; }

    TextMeshProUGUI clockText;
    const string StringFormat = "m':'ss'.'ff";

    Enemy enemy;
    PauseHandler pauseHandler;

    void Awake()
    {
        clockText = GetComponent<TextMeshProUGUI>();

        pauseHandler = FindObjectOfType<PauseHandler>();
        enemy = FindObjectOfType<Enemy>();
    }

    void Start()
    {
        pauseHandler.GamePauseAction += SetPaused;

        enemy.StartAttackAction += RestartClock;
        enemy.DeathAction += StopClock;
    }

    void Update()
    {
        clockText.text = TimeSpan.FromSeconds(CurrentTime).ToString(StringFormat);
    }

    #region Clock functions

    IEnumerator Run(float delay = 0f)
    {
        if (delay > 0f)
        {
            IsPaused = true;
            yield return Blink(delay);
        }

        CurrentTime = 0f;
        IsPaused = false;

        while (true)
        {
            yield return null;

            if (!IsPaused)
            {
                CurrentTime += Time.deltaTime;
            }
        }
    }

    IEnumerator Blink(float blinkDuration)
    {
        float currentTime = 0f;

        while (currentTime < blinkDuration)
        {
            float a = Mathf.Cos(currentTime * Mathf.PI * 2f) * 0.5f + 0.5f;
            SetTextAlpha(a);

            yield return null;
            currentTime += Time.deltaTime;
        }

        SetTextAlpha(1f);
    }

    void StartClock()
    {
        clock = Run(4f);
        StartCoroutine(clock);
    }

    void StopClock()
    {
        if (clock != null)
        {
            StopCoroutine(clock);
        }
    }

    void RestartClock(int _)
    {
        StopClock();
        StartClock();
    }

    void SetPaused(bool state)
    {
        IsPaused = state;
    }

    #endregion
 
    void SetTextAlpha(float alpha)
    {
        Color c = clockText.color;
        c.a = alpha;
        clockText.color = c;
    }
}