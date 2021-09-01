using System.Collections;
using UnityEngine;

public class AriesBullet1 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return StartCoroutine(this.ChangeSpeed(2f, -1f, 1f));

        yield return StartCoroutine(this.ChangeSpeed(-2f, 0f, 0.5f));

        StartCoroutine(this.ChangeSpeed(0f, 2f, 1f));

        while (enabled)
        {
            yield return this.RotateBy(60f, 1f);
            yield return this.RotateBy(-60f, 1f);
        }
    }
}