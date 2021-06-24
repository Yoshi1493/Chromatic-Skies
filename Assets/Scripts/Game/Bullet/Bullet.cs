using System.Collections;
using UnityEngine;

public abstract class Bullet : Actor
{
    [SerializeField] IntObject bulletPower;
    [SerializeField] protected float moveSpeed;

    const float MaxLifetime = 3f;
    float currentLifetime;

    protected int bulletIndex;

    protected override void Awake()
    {
        base.Awake();

        moveDirection = transform.up;
    }

    void OnEnable()
    {
        currentLifetime = 0;
    }

    protected virtual void Update()
    {
        Move(moveSpeed);

        currentLifetime += Time.deltaTime;
        if (currentLifetime > MaxLifetime) Destroy();
    }

    protected void CheckCollisionWith<TShip>() where TShip : Ship
    {
        Collider2D coll = Physics2D.OverlapCircle(transform.position, 0.16f);

        if (coll && coll.TryGetComponent(out TShip ship))
        {
            int shipDefense = ship.shipData.Defense.CurrentValue;

            coll.GetComponent<TShip>().TakeDamage(bulletPower.value, shipDefense);
            Destroy();
        }
    }

    protected abstract void Destroy();

    #region Movement methods

    #region Change speed
    public void ChangeSpeed(float endSpeed, float lerpTime = 0f)
    {
        if (lerpTime <= 0f)
        {
            moveSpeed = endSpeed;
            return;
        }

        Run.Coroutine(_ChangeSpeed(endSpeed, lerpTime));
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

        Run.Coroutine(_ChangeDirection(endDirection, lerpTime));
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

    #region Change rotation
    public void RotateBy(float rotateAmount, float lerpTime = 0f)
    {
        if (lerpTime <= 0)
        {
            transform.Rotate(rotateAmount * Vector3.forward);
            return;
        }

        Run.Coroutine(_Rotate(rotateAmount, lerpTime));
    }

    IEnumerator _Rotate(float rotateAmount, float lerpTime)
    {
        float currentLerpTime = 0f;
        float startRotation = transform.eulerAngles.z;
        float endRotation = startRotation + rotateAmount;

        while (currentLerpTime <= lerpTime)
        {
            float newRotZ = transform.eulerAngles.z;
            newRotZ = Mathf.Lerp(startRotation, endRotation, currentLerpTime / lerpTime);

            yield return CoroutineHelper.EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }
    }
    #endregion

    #region FaceTowards
    public void FaceTowards(Transform target)
    {
        if (target == null) return;

        Vector2 distance = target.position - transform.position;
        float zRotation = Mathf.Atan2(-distance.x, distance.y) * Mathf.Rad2Deg;

        transform.eulerAngles = zRotation * Vector3.forward;
    }
    #endregion

    #endregion

}