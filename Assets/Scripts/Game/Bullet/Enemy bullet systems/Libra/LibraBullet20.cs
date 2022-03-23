using System.Collections;
using UnityEngine;

public class LibraBullet20 : EnemyBullet
{
    protected override float MaxLifetime => 5f; 

    protected override IEnumerator Move()
    {
        MoveSpeed = 3f;

        float randRotation = Random.Range(60f, 150f);
        yield return this.RotateBy(randRotation, MaxLifetime, Random.value > 0.5f);
    }
}