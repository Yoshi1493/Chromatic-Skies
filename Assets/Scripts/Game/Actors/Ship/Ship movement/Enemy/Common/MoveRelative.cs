using System.Collections;
using UnityEngine;

public class MoveRelative : CommonEnemyMovement
{
    [SerializeField] float moveSpeed;
    [SerializeField] Vector2 normalizedDirection;

    protected override IEnumerator Move()
    {
        yield return parentShip.MoveRelative(normalizedDirection, moveSpeed, duration, delay);
    }
}