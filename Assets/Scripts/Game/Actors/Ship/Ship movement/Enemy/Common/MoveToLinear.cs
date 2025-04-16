using System.Collections;
using UnityEngine;

public class MoveToLinear : CommonEnemyMovement
{
    [SerializeField] Vector2 endPosition;

    protected override IEnumerator Move()
    {
        yield return parentShip.MoveToLinear(endPosition, duration, delay);
    }
}