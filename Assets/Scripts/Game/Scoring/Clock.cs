using System;
using System.Collections;
using UnityEngine;
using TMPro;
using static CoroutineHelper;

public class Clock : MonoBehaviour
{
    IEnumerator clock;
    IEnumerator pauseCoroutine;

    [SerializeField] FloatObject currentTime;
    public bool IsPaused { get; private set; }

    TextMeshProUGUI clockText;
    public const string StringFormat = "m':'ss'.'ff";

    Player player;
    Boss boss;

    [SerializeField] PauseHandler pauseHandler;

    [SerializeField] IntObject selectedBossIndex;
    EnemySpawner enemySpawner;

    void Awake()
    {
        clockText = GetComponent<TextMeshProUGUI>();

        player = FindAnyObjectByType<Player>();
        boss = FindAnyObjectByType<Boss>();

        enemySpawner = FindAnyObjectByType<EnemySpawner>();
    }

    void OnEnable()
    {
        clockText.enabled = true;

        currentTime.value = 0f;
        RestartClock(boss.RespawnTime + 2f);
    }

    void OnDisable()
    {
        clockText.enabled = false;
    }

    void Start()
    {
        player.LoseLifeAction += OnPlayerLoseLife;
        player.DeathAction += StopClock;

        boss.LoseLifeAction += OnBossLoseLife;
        boss.DeathAction += StopClock;

        pauseHandler.GamePauseAction += SetPaused;

        enemySpawner.BossSpawnAction += OnBossSpawn;

        StopAllCoroutines();
        enabled = false;
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
            PauseClock(true, delay);
        }

        yield return WaitUntil(() => pauseCoroutine == null);
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

    void PauseClock(bool blink, float duration = Mathf.Infinity)
    {
        SetPaused(true);

        if (pauseCoroutine != null)
        {
            StopCoroutine(pauseCoroutine);
        }

        pauseCoroutine = _Pause(blink, duration);
        StartCoroutine(pauseCoroutine);
    }

    IEnumerator _Pause(bool blink, float duration)
    {
        if (blink)
        {
            float currentTime = 0f;

            while (currentTime < duration)
            {
                float a = Mathf.Cos(currentTime * Mathf.PI * 2f) * 0.5f + 0.5f;
                SetTextAlpha(a);

                yield return null;
                currentTime += Time.deltaTime;
            }

            SetTextAlpha(1f);
        }
        else
        {
            yield return WaitForSeconds(duration);
        }

        SetPaused(false);
        pauseCoroutine = null;
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

    void OnPlayerLoseLife()
    {
        PauseClock(false, player.RespawnTime + 2f);
    }

    void OnBossLoseLife()
    {
        RestartClock(boss.RespawnTime + 2f);
    }

    void OnCharacterShipDie()
    {
        StopClock();
    }

    void OnBossSpawn()
    {
        enabled = true;
    }

    void OnDestroy()
    {
        player.LoseLifeAction -= OnPlayerLoseLife;
        player.DeathAction -= OnCharacterShipDie;

        boss.LoseLifeAction -= OnBossLoseLife;
        boss.DeathAction -= OnCharacterShipDie;

        pauseHandler.GamePauseAction -= SetPaused;

        enemySpawner.BossSpawnAction -= OnBossSpawn;
    }
}