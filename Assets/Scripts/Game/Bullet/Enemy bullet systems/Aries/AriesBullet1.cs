using System.Collections;
using UnityEngine;

public class AriesBullet1 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        StartCoroutine(this.ChangeSpeed(3, 1, 2));

        while (true)
        {
            yield return this.RotateBy(120, 1, 0);
            yield return this.RotateBy(-120, 1, 0);
        }
    }
}