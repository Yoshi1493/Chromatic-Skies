using UnityEngine;

public abstract class Projectile : Actor
{
    [SerializeField] protected ProjectileObject projectileData;

    public int ProjectileID => projectileData.ID;

    protected virtual float MaxLifetime => 10f;
    protected float currentLifetime;

    protected virtual int MaxCollisions => 4;
    protected Collider2D[] collisionResults;

    protected abstract int CollisionMask { get; }
    protected abstract int NumCollisions { get; }
    protected bool IsColliding => NumCollisions > 0;

    [HideInInspector] public Vector3 moveDirection;
    [HideInInspector] public float currentSpeed;

    protected override void Awake()
    {
        base.Awake();

        collisionResults = new Collider2D[MaxCollisions];
        SpriteRenderer.sprite = projectileData.sprite;
    }

    protected virtual void OnEnable()
    {
        SpriteRenderer.color = projectileData.useSolidColour ? projectileData.colour : projectileData.gradient.Evaluate(Random.value);

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

    protected void IncrementLifetime()
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
                    HandleCollision(collisionResults[i]);
                }
            }
        }
    }

    protected abstract void HandleCollision(Collider2D coll);

    protected void SpawnDestructionParticles(Vector3 spawnPos, float spawnRotZ)
    {
        //grab particle obj from pool; get VFX component
        GameObject vfx = VFXObjectPool.Instance.Get(VFXType.ProjectileDestruction);
        var particleEffect = vfx.GetComponent<ParticleEffect>();

        //set spawn pos+rot
        vfx.transform.SetPositionAndRotation(spawnPos, Quaternion.Euler(0f, 0f, spawnRotZ));
        vfx.SetActive(true);

        //set colour based on sprite colour
        particleEffect.ParticleSystem.SetVector4("ParticleColour", SpriteRenderer.color);
        particleEffect.enabled = true;
    }

    public abstract void Destroy();
}