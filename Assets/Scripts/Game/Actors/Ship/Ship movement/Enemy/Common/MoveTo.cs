using System.Collections;
using UnityEngine;

public class MoveTo : EnemyMovement
{
    [SerializeField] float delay;
    [SerializeField] float moveDuration;
    [SerializeField] Vector2 endPosition;

    protected override IEnumerator Move()
    {
        yield return this.MoveTo(endPosition, moveDuration, delay);
    }
}