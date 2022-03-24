using System.Collections;
using UnityEngine;

public class LibraBullet21 : EnemyBullet
{
    protected override float MaxLifetime => 8f;

    protected override void OnEnable()
    {
        base.OnEnable();
        print(moveDirection.ToString("F3"));
    }

    protected override IEnumerator Move()
    {
        MoveSpeed = Random.Range(3f, 4f);

        float randRotation = Random.Range(-60f, 60f);
        yield return this.RotateBy(randRotation, MaxLifetime);
    }
}