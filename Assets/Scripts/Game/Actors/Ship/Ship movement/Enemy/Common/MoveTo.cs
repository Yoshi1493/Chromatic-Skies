using System.Collections;
using UnityEngine;

public class MoveTo : CommonEnemyMovement
{
    [SerializeField] Vector2 endPosition;

    protected override IEnumerator Move()
    {
        yield return this.MoveTo(endPosition, duration, delay);
    }
}