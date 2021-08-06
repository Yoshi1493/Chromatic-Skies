using System.Collections;
using UnityEngine;

public class AriesBullet1 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        StartCoroutine(this.ChangeSpeed(4, 1, 2));

        while (enabled)
        {
            yield return this.RotateBy(60, 1, 0);
            yield return this.RotateBy(-60, 1, 0);
        }
    }
}