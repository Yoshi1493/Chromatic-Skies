using System.Collections;
using UnityEngine;

public class VirgoMovementSystem6 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return this.MoveToRandomPosition(1f);
    }
}