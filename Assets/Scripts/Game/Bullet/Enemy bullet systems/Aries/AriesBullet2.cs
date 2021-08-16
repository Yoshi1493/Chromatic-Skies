using System.Collections;
using UnityEngine;

public class AriesBullet2 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        MoveSpeed = 0;
        yield return StartCoroutine(this.ChangeSpeed(0, 2, 2, 0.5f));
    }
}