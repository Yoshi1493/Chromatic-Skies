using System.Collections;
using UnityEngine;

public class MoveRelativeLinear : CommonEnemyMovement
{
    [SerializeField] float moveSpeed;
    [SerializeField] Vector2 normalizedDirection;

    protected override IEnumerator Move()
    {
        yield return parentShip.MoveRelativeLinear(normalizedDirection, moveSpeed, duration, delay);
    }
}