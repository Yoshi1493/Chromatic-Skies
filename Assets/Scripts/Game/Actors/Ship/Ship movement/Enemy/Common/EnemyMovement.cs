using System.Collections;
using UnityEngine;

public abstract class EnemyMovement : ShipMovement<Enemy>
{
    [SerializeField] protected float delay;
    [SerializeField] protected float duration;

    protected IEnumerator moveCoroutine;
    protected abstract IEnumerator Move();

    void OnEnable()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        moveCoroutine = Move();
        StartCoroutine(moveCoroutine);
    }

    protected override void OnLoseLife()
    {
        StopAllCoroutines();
    }
}