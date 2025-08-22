using System.Collections;
using UnityEngine;
using static CameraBoundaries;

[RequireComponent(typeof(CommonEnemy))]
public abstract class CommonEnemyMovement : ShipMovement<CommonEnemy>
{
    protected IEnumerator moveCoroutine;
    protected abstract IEnumerator Move();

    protected float SignX => Mathf.Sign(parentShip.transform.position.x);

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
        if (parentShip.IsWithinCameraBounds())
        {
            Vector3 endPos = new(parentShip.transform.position.x, ScreenHalfHeight + 1f);
            float duration = (endPos - parentShip.transform.position).magnitude;

            yield return parentShip.MoveTo(endPos, duration);
        }

        parentShip.Destroy();
    }
}