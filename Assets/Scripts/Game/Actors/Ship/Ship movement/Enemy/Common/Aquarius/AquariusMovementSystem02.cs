using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusMovementSystem02 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveRelative(Vector3.down, 2f, 2f);
    }
}