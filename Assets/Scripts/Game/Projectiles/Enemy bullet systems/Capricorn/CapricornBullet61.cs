using System.Collections;
using UnityEngine;

public class CapricornBullet61 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        MoveSpeed = 3f;
        yield return null;
    }
}