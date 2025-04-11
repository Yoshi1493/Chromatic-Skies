using System.Collections;
using UnityEngine;

public class BulletLerpSpeed : EnemyBullet
{
    [SerializeField] float delay;
    [SerializeField] float startSpeed;
    [SerializeField] float endSpeed;
    [SerializeField] float duration;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(startSpeed, endSpeed, duration, delay);
    }
}