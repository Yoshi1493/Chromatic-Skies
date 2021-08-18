using System.Collections;
using UnityEngine;

public class AriesBullet2 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return StartCoroutine(this.ChangeSpeed(3, 0, 0.5f));
        yield return StartCoroutine(this.ChangeSpeed(0, 3, 2));
    }
}