using System.Collections;
using UnityEngine;

public class AquariusBullet42 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        MoveSpeed = 3f;
        yield return null;
    }
}