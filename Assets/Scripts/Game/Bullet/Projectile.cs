using System.Collections;
using UnityEngine;

public abstract class Projectile : Actor
{
    [SerializeField] protected IntObject power;

    [SerializeField] float moveSpeed;
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    protected IEnumerator movementBehaviour;

    protected virtual void CheckCollisionWith<TShip>(System.Func<Collider2D> Condition) where TShip : Ship
    {
        Collider2D coll = Condition();

        if (coll && coll.TryGetComponent(out TShip _))
        {
            HandleCollisionWithShip<Ship>(coll);
        }
    }

    protected virtual void HandleCollisionWithShip<TShip>(Collider2D coll) where TShip : Ship
    {
        coll.GetComponent<TShip>().TakeDamage(power.value);
    }

    public abstract void Destroy();
}