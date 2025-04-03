using System.Collections;
using UnityEngine;

public class MoveToLinear : EnemyMovement
{
    [SerializeField] float delay;
    [SerializeField] float moveDuration;
    [SerializeField] Vector2 endPosition;

    protected override IEnumerator Move()
    {
        yield return this.MoveToLinear(endPosition, moveDuration, delay);
    }
}