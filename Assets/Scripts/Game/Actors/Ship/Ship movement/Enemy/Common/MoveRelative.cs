using System.Collections;
using UnityEngine;

public class MoveRelative : CommonEnemyMovement
{
    [SerializeField] float moveDuration;
    [SerializeField] float moveSpeed;
    [SerializeField] Vector2 normalizedDirection;

    protected override IEnumerator Move()
    {
        yield return this.MoveRelative(normalizedDirection, moveSpeed, moveDuration, delay);
    }
}