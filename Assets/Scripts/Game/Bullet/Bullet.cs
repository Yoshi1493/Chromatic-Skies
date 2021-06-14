using UnityEngine;

public abstract class Bullet : Actor
{
    [SerializeField] protected float moveSpeed;

    protected virtual float MaxLifetime => 3f;
    float currentLifetime;

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

    protected abstract void CheckCollisionWith<T>() where T : Ship;

    protected abstract void Destroy();
}