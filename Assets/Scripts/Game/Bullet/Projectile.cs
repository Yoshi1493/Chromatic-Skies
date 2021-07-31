using System.Collections;
using UnityEngine;

public abstract class Projectile : Actor
{
    public ProjectileObject projectileData;

    protected abstract Collider2D CollisionCondition { get; }

    protected IEnumerator movementBehaviour;

    protected virtual void OnEnable()
    {
        spriteRenderer.sprite = projectileData.sprite;

        projectileData.MoveSpeed.CurrentValue = projectileData.MoveSpeed.OriginalValue;
    }

    protected void CheckCollisionWith<TShip>() where TShip : Ship
    {
        Collider2D coll = CollisionCondition;

        if (coll && coll.TryGetComponent(out TShip _))
        {
            HandleCollisionWithShip<Ship>(coll);
        }
    }

    protected virtual void HandleCollisionWithShip<TShip>(Collider2D coll) where TShip : Ship
    {
        coll.GetComponent<TShip>().TakeDamage(projectileData.Power.value);
    }

    public abstract void Destroy();
}