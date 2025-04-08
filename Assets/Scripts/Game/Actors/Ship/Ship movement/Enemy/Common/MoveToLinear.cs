using System.Collections;
using UnityEngine;

public class MoveToLinear : EnemyMovement
{
    [SerializeField] Vector2 endPosition;

    protected override IEnumerator Move()
    {
        yield return this.MoveToLinear(endPosition, duration, delay);
    }
}