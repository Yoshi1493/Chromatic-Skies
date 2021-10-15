using System.Collections;
using UnityEngine;

public abstract class Projectile : Actor
{
    public ProjectileObject projectileData;

    [SerializeField] float moveSpeed;
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    protected abstract Collider2D CollisionCondition { get; }

    protected IEnumerator movementBehaviour;

    protected override void Awake()
    {
        base.Awake();
        spriteRenderer.sprite = projectileData.sprite;
    }

    protected virtual void OnEnable()
    {
        spriteRenderer.color = projectileData.useSolidColour ? projectileData.colour : projectileData.gradient.Evaluate(Random.value);

        moveDirection = new Vector2
        (
            Mathf.Sin(transform.localEulerAngles.z * Mathf.Deg2Rad),
            -Mathf.Cos(transform.localEulerAngles.z * Mathf.Deg2Rad)
        );
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