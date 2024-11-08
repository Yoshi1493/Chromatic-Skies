using System;
using System.Collections;
using UnityEngine;
using TMPro;
using static CoroutineHelper;

public class Clock : MonoBehaviour
{
    IEnumerator clock;
    IEnumerator blinkCoroutine;

    [SerializeField] FloatObject currentTime;
    public bool IsPaused { get; private set; }

    TextMeshProUGUI clockText;
    public const string StringFormat = "m':'ss'.'ff";

    Player player;
    Enemy enemy;
    PauseHandler pauseHandler;

    void Awake()
    {
        clockText = GetComponent<TextMeshProUGUI>();

        player = FindObjectOfType<Player>();
        enemy = FindObjectOfType<Enemy>();
        pauseHandler = FindObjectOfType<PauseHandler>();
    }

    void Start()
    {
        player.LoseLifeAction += () => Blink(player.RespawnTime + 2f);
        player.DeathAction += StopClock;

        enemy.LoseLifeAction += () => RestartClock(enemy.RespawnTime + 2f);
        enemy.DeathAction += StopClock;

        pauseHandler.GamePauseAction += SetPaused;

        currentTime.value = 0f;
        RestartClock(enemy.RespawnTime + 2f);
    }

    void Update()
    {
        clockText.text = TimeSpan.FromSeconds(currentTime.value).ToString(StringFormat);
    }

    #region Clock functions

    IEnumerator Run(float delay = 0f)
    {
        if (delay > 0f)
        {
            Blink(delay);
        }

        yield return WaitUntil(() => blinkCoroutine == null);
        currentTime.value = 0f;

        while (true)
        {
            yield return null;

            if (!IsPaused)
            {
                currentTime.value += Time.deltaTime;
            }
        }
    }

    void Blink(float duration = Mathf.Infinity)
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
        }

        blinkCoroutine = _Blink(duration);
        StartCoroutine(blinkCoroutine);
    }

    IEnumerator _Blink(float duration)
    {
        SetPaused(true);

        float currentTime = 0f;

        while (currentTime < duration)
        {
            float a = Mathf.Cos(currentTime * Mathf.PI * 2f) * 0.5f + 0.5f;
            SetTextAlpha(a);

            yield return null;
            currentTime += Time.deltaTime;
        }

        SetTextAlpha(1f);

        SetPaused(false);
        blinkCoroutine = null;
    }

    void StopClock()
    {
        if (clock != null)
        {
            StopCoroutine(clock);
            clock = null;
        }
    }

    void RestartClock(float delay = 0f)
    {
        StopClock();

        clock = Run(delay);
        StartCoroutine(clock);
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