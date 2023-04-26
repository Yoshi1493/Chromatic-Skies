using System.Collections;
using UnityEngine;

public class LibraBullet51 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float startSpeed = MoveSpeed;
        StartCoroutine(this.LerpSpeed(startSpeed, startSpeed * 2f, 2f));
        yield return this.RotateBy(60f, 5f, Random.value > 0.5f);
    }
}