using System.Collections;
using UnityEngine;

public class AriesBullet3 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return null;

        StartCoroutine(this.ChangeSpeed(1f, 3f, 1f));
        StartCoroutine(this.RotateAround(ownerShip, 2f, 180f, 0.5f));
    }
}