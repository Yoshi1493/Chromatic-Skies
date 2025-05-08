using System.Collections;
using UnityEngine;
using static CameraBoundaries;

[RequireComponent(typeof(Enemy))]
public abstract class CommonEnemyMovement : ShipMovement<Enemy>
{
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

    protected IEnumerator LeaveScene()
    {
        Vector3 endPos = new(parentShip.transform.position.x, ScreenHalfHeight + 1f);
        float duration = (endPos - parentShip.transform.position).magnitude;

        yield return parentShip.MoveTo(endPos, duration);
    }
}