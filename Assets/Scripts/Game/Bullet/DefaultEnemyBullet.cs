using System.Collections;
using UnityEngine;

public class DefaultEnemyBullet : EnemyBullet
{
    [SerializeField] float startSpeed = 3f;
    [SerializeField] float midSpeed = 1f;
    [SerializeField] float endSpeed = 2.5f;

    protected override IEnumerator Move()
    {
        yield return StartCoroutine(this.LerpSpeed(startSpeed, midSpeed, 0.5f));
        yield return StartCoroutine(this.LerpSpeed(midSpeed, endSpeed, 1f));
    }
}