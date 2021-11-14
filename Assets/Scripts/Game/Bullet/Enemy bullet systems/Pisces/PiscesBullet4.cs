using System.Collections;
using UnityEngine;

public class PiscesBullet4 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;
        yield return null;
    }
}