using UnityEngine;

public abstract class Projectile : Actor
{
    public ProjectileObject projectileData;

    protected virtual float MaxLifetime => 10f;
    protected float currentLifetime;

    [SerializeField] float moveSpeed;

    protected float HitboxSize => Mathf.Min(spriteRenderer.size.x, spriteRenderer.size.y) / 2f;
    protected abstract int CollisionMask { get; }
    protected abstract Collider2D CollisionCondition { get; }

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

        currentLifetime = 0f;
    }

    protected virtual void Update()
    {        
        IncrementLifetime();
    }

    protected virtual void IncrementLifetime()
    {
        currentLifetime += Time.deltaTime;

        if (currentLifetime > MaxLifetime)
            Destroy();
    }

    protected void CheckCollisionWith<TShip>() where TShip : Ship
    {
        Collider2D coll = CollisionCondition;

        if (coll)
        {
            if (coll.TryGetComponent(out TShip _))
            {
                HandleCollisionWithShip<Ship>(coll);
            }
        }
    }

    protected virtual void HandleCollisionWithShip<TShip>(Collider2D coll) where TShip : Ship
    {
        TShip ship = coll.GetComponent<TShip>();

        if (!ship.invincible)
            ship.TakeDamage(projectileData.Power.value);
    }

    public abstract void Destroy();

    #region DEBUG

#if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        if (UnityEditor.EditorApplication.isPlaying)
            Gizmos.DrawSphere(transform.position, HitboxSize);
    }
#endif

    #endregion
}