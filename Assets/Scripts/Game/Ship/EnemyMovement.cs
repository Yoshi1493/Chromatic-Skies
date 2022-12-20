using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static EnemyMovementBehaviour;

public abstract class EnemyMovement : ShipMovement
{
    IEnumerator moveCoroutine;
    protected abstract IEnumerator _Move();

    protected override void Move()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        moveCoroutine = _Move();
        StartCoroutine(moveCoroutine);
    }

    protected override void OnLoseLife()
    {
        StopAllCoroutines();
    }

    protected override void OnRespawn()
    {
        Move();
    }
}