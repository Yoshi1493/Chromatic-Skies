using System.Collections;
using UnityEngine;

public class AriesBullet3 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        MoveSpeed = 0;

        float rand = Random.Range(1f, 3f);
        yield return StartCoroutine(this.ChangeSpeed(0, 2, 2, 2));
    }
}