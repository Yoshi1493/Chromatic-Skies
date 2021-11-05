using System.Collections;
using UnityEngine;

public class DefaultEnemyBullet : EnemyBullet
{
    [Tooltip("x = initial speed\ny = mid speed\nz = end speed")]
    [SerializeField] Vector3 speeds;

    [Tooltip("x = time from moveSpeeds.x -> .y\ny = time from moveSpeeds.y -> .z")]
    [SerializeField] Vector2 durations;

    protected override IEnumerator Move()
    {
        yield return StartCoroutine(this.LerpSpeed(speeds.x, speeds.y, durations.x));
        yield return StartCoroutine(this.LerpSpeed(speeds.y, speeds.z, durations.y));
    }
}