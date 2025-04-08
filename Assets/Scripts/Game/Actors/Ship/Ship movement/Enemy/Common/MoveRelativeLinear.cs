using System.Collections;
using UnityEngine;

public class MoveRelativeLinear : EnemyMovement
{
    [SerializeField] float delay;
    [SerializeField] float moveDuration;
    [SerializeField] float moveSpeed;
    [SerializeField] Vector2 normalizedDirection;

    protected override IEnumerator Move()
    {
        yield return this.MoveRelativeLinear(normalizedDirection, moveSpeed, moveDuration, delay);
    }
}