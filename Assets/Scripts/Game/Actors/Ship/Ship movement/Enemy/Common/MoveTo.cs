using System.Collections;
using UnityEngine;

public class MoveTo : EnemyMovement
{
    [SerializeField] Vector2 endPosition;

    protected override IEnumerator Move()
    {
        yield return this.MoveTo(endPosition, duration, delay);
    }
}