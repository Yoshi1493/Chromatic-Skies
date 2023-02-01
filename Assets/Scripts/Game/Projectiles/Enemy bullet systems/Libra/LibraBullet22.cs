using System.Collections;
using UnityEngine;

public class LibraBullet22 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        MoveSpeed = 3f;
        yield return this.RotateBy(Random.Range(-30f, 30f), 2f, Random.value > 0.5f);
    }
}