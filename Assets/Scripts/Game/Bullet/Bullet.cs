using UnityEngine;

public abstract class Bullet : Actor
{
    [SerializeField] protected ShipObject ownerShip;
    [SerializeField] protected float moveSpeed;

    protected virtual float MaxLifetime => 3f;
    float currentLifetime;

    int bulletPower;

    protected override void Awake()
    {
        base.Awake();

        moveDirection = transform.up;
    }

    void OnEnable()
    {
        currentLifetime = 0;
        bulletPower = ownerShip.Power.CurrentValue;
    }

    protected virtual void Update()
    {
        Move(moveSpeed);

        currentLifetime += Time.deltaTime;
        if (currentLifetime > MaxLifetime) Destroy();
    }

    protected void CheckCollisionWith<T>() where T : Ship
    {
        Collider2D coll = Physics2D.OverlapCircle(transform.position, 0.16f);

        if (coll && coll.TryGetComponent(out T ship))
        {
            int shipDefense = ship.shipData.Defense.CurrentValue;

            coll.GetComponent<T>().TakeDamage(bulletPower, shipDefense);
            Destroy();
        }
    }

    protected abstract void Destroy();
}