using UnityEngine;

public abstract class Bullet : Projectile
{
    const float MaxLifetime = 10f;
    float currentLifetime;

    protected int bulletIndex;

    protected override void Awake()
    {
        base.Awake();

        moveDirection = transform.up;
    }

    protected virtual void OnEnable()
    {
        currentLifetime = 0;
    }

    protected virtual void Update()
    {
        Move(MoveSpeed);

        currentLifetime += Time.deltaTime;
        if (currentLifetime > MaxLifetime) Destroy();
    }

    protected override void HandleCollisionWithShip<TShip>(Collider2D coll)
    {
        base.HandleCollisionWithShip<TShip>(coll);
        Destroy();
    }

    public abstract override void Destroy();
}