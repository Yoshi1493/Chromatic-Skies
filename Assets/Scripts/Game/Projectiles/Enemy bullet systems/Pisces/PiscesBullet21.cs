using System.Collections;
using UnityEngine;

public class PiscesBullet21 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float startSpeed = Random.Range(3f, 4f);

        yield return this.LerpSpeed(startSpeed, startSpeed - 2, 2f);
    }
}