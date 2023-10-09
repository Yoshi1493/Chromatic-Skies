using UnityEngine;

public abstract class Projectile : Actor
{
    [SerializeField] protected ProjectileObject projectileData;

    public int ProjectileID => projectileData.ID;

    protected virtual float MaxLifetime => 10f;
    protected float currentLifetime;

    const int MaxCollisions = 4;
    protected Collider2D[] collisionResults = new Collider2D[MaxCollisions];

    protected abstract int CollisionMask { get; }
    protected abstract int NumCollisions { get; }
    protected bool IsColliding => NumCollisions > 0;

    [HideInInspector] public Vector3 moveDirection;
    [HideInInspector] public float currentSpeed;

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
        {
            Destroy();
        }
    }

    protected void CheckCollisionWith<T>()
    {
        if (NumCollisions > 0)
        {
            for (int i = 0; i < NumCollisions; i++)
            {
                if (collisionResults[i].TryGetComponent(out T _))
                {
                    HandleCollision<T>(collisionResults[i]);
                    break;
                }
            }
        }
    }

    protected abstract void HandleCollision<T>(Collider2D coll);

    public abstract void Destroy();
}