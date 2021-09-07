using System.Collections;
using UnityEngine;

public class AriesBullet1 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return StartCoroutine(this.ChangeSpeed(2f, -1f, 1f));

        StartCoroutine(this.ChangeSpeed(-1f, 2f, 1f));

        while (enabled)
        {
            yield return this.RotateBy(60f, 1f);
            yield return this.RotateBy(-60f, 1f);
        }
    }
}