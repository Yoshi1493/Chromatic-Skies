using System.Collections;
using UnityEngine;

public abstract class Projectile : Actor
{
    [SerializeField] protected IntObject power;
    [SerializeField] protected float moveSpeed;

    protected IEnumerator movementBehaviour;

    protected virtual void CheckCollisionWith<TShip>(System.Func<Collider2D> Condition) where TShip : Ship
    {
        Collider2D coll = Condition();

        if (coll && coll.TryGetComponent(out TShip ship))
        {
            HandleCollisionWithShip<Ship>(coll);
        }
    }

    protected virtual void HandleCollisionWithShip<TShip>(Collider2D coll) where TShip : Ship
    {
        coll.GetComponent<TShip>().TakeDamage(power.value);
    }

    public abstract void Destroy();

    #region Movement behaviour

    #region Change speed
    public void ChangeSpeed(float endSpeed, float lerpTime = 0f)
    {
        if (lerpTime <= 0f)
        {
            moveSpeed = endSpeed;
            return;
        }

        if (movementBehaviour != null) StopCoroutine(movementBehaviour);

        movementBehaviour = _ChangeSpeed(endSpeed, lerpTime);
        StartCoroutine(movementBehaviour);
    }

    IEnumerator _ChangeSpeed(float endSpeed, float lerpTime)
    {
        float currentLerpTime = 0f;
        float startSpeed = moveSpeed;

        while (currentLerpTime <= lerpTime)
        {
            moveSpeed = Mathf.Lerp(startSpeed, endSpeed, currentLerpTime / lerpTime);

            yield return CoroutineHelper.EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }
    }
    #endregion

    #region Change direction
    public void ChangeDirection(Vector2 endDirection, float lerpTime = 0f)
    {
        if (lerpTime <= 0f)
        {
            moveDirection = endDirection;
            return;
        }

        if (movementBehaviour != null) StopCoroutine(movementBehaviour);

        movementBehaviour = _ChangeDirection(endDirection, lerpTime);
        StartCoroutine(movementBehaviour);
    }

    IEnumerator _ChangeDirection(Vector2 endDirection, float lerpTime)
    {
        float currentLerpTime = 0f;
        Vector2 startDirection = moveDirection;

        while (currentLerpTime <= lerpTime)
        {
            moveDirection = Vector2.Lerp(startDirection, endDirection, currentLerpTime / lerpTime);

            yield return CoroutineHelper.EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }
    }
    #endregion

    #region Rotate by
    public void RotateBy(float rotateAmount, float lerpTime = 0f)
    {
        if (lerpTime <= 0f)
        {
            transform.Rotate(rotateAmount * Vector3.forward);
            return;
        }

        if (movementBehaviour != null) StopCoroutine(movementBehaviour);

        movementBehaviour = _RotateBy(rotateAmount, lerpTime);
        StartCoroutine(movementBehaviour);
    }

    IEnumerator _RotateBy(float rotateAmount, float lerpTime)
    {
        float currentLerpTime = 0f;
        float startRotation = transform.eulerAngles.z;
        float endRotation = startRotation + rotateAmount;

        while (currentLerpTime <= lerpTime)
        {
            float newRotZ = transform.eulerAngles.z;
            newRotZ = Mathf.Lerp(startRotation, endRotation, currentLerpTime / lerpTime);
            transform.eulerAngles = Vector3.forward * newRotZ;

            yield return CoroutineHelper.EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }
    }
    #endregion

    #region Turn towards
    public void TurnTowards(Transform target, float lerpTime = 0f)
    {
        if (target == null) return;

        Vector2 distance = target.position - transform.position;
        float zRotation = Mathf.Atan2(-distance.x, distance.y) * Mathf.Rad2Deg;

        if (lerpTime <= 0f)
        {
            transform.eulerAngles = zRotation * Vector3.forward;
            return;
        }

        if (movementBehaviour != null) StopCoroutine(movementBehaviour);

        float rotateAmount = zRotation - transform.eulerAngles.z;
        movementBehaviour = _RotateBy(rotateAmount, lerpTime);
        StartCoroutine(movementBehaviour);
    }
    #endregion

    #endregion
}