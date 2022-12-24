using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static EnemyMovementBehaviour;

public class CapricornMovementSystem1 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return null;
    }
}