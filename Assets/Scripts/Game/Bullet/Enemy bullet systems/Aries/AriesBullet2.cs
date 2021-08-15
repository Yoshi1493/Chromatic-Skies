using System.Collections;
using UnityEngine;

public class AriesBullet2 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return StartCoroutine(this.ChangeSpeed(3, 2, 3));
    }
}