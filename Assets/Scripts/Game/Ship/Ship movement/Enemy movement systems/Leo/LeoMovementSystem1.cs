using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static EnemyMovementBehaviour;

public class LeoMovementSystem1 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return null;
    }
}