using System.Collections;
using UnityEngine;

public class Clock : MonoBehaviour
{
    IEnumerator clock;
    public float CurrentTime { get; private set; }

    public bool IsPaused { get; private set; }

    Enemy enemy;
    PauseHandler pauseHandler;

    IEnumerator CountUp()
    {
        while (true)
        {
            yield return null;

            if (!IsPaused)
            {
                CurrentTime += Time.deltaTime;
            }
        }
    }

    public void StartClock(float startTime)
    {
        CurrentTime = startTime;

        clock = CountUp();
        StartCoroutine(clock);
    }

    public void StopClock()
    {
        StopCoroutine(clock);
    }

    public void SetPaused(bool state)
    {
        IsPaused = state;
    }

    public void RestartClock()
    {
        StopClock();
        StartClock(0f);
    }

    void Awake()
    {
        pauseHandler = FindObjectOfType<PauseHandler>();
        enemy = FindObjectOfType<Enemy>();
    }

    void Start()
    {
        pauseHandler.GamePauseAction += SetPaused;
        enemy.DeathAction += StopClock;
    }
}