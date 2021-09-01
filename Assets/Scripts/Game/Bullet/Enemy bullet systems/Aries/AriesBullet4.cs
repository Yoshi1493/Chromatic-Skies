using System.Collections;
using UnityEngine;

public class AriesBullet4 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return StartCoroutine(this.ChangeSpeed(3f, 0f, 1));

        StartCoroutine(this.LookAt(playerShip, 0.5f));
        StartCoroutine(this.ChangeSpeed(0f, 5f, 0f, 0.5f));
    }
}