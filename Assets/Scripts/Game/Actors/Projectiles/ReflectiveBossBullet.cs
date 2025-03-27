using UnityEngine;

public abstract class ReflectiveBossBullet : BossBullet
{
    protected virtual int MaxReflectCount => 1;
    protected int currentReflectCount;

    const float ReflectCollisionThreshold = 0.01f;

    protected override int CollisionMask => base.CollisionMask | 1 << LayerMask.NameToLayer("Bullet bounds");

    protected override void OnEnable()
    {
        base.OnEnable();

        currentReflectCount = MaxReflectCount;
        SpriteRenderer.color = projectileData.gradient.Evaluate(1f);
    }

    protected override void Update()
    {
        base.Update();
        CheckCollisionWith<EdgeCollider2D>();
    }

    protected override void HandleCollision(Collider2D coll)
    {
        base.HandleCollision(coll);

        if (coll.TryGetComponent(out EdgeCollider2D _) && currentReflectCount > 0)
        {
            currentReflectCount--;
            HandleReflection(coll);
        }
    }

    protected virtual void HandleReflection(Collider2D coll)
    {
        Vector3 p = coll.ClosestPoint(transform.position);
        Vector3 d = moveDirection;

        if (Mathf.Abs(p.x - transform.position.x) < ReflectCollisionThreshold)
        {
            d.y *= -1;
        }
        if (Mathf.Abs(p.y - transform.position.y) < ReflectCollisionThreshold)
        {
            SpawnReflectionParticles(p);
            d.x *= -1;
        }

        moveDirection = d;
    }

    void SpawnReflectionParticles(Vector3 spawnPos)
    {
        var particleEffect = VFXObjectPool.Instance.Get((int)VFXType.BulletReflection);

        particleEffect.transform.position = spawnPos;
        particleEffect.gameObject.SetActive(true);

        particleEffect.ParticleSystem.SetVector4("ParticleColour", SpriteRenderer.color);
        particleEffect.ParticleSystem.SetFloat("ParticleRotation", Mathf.Sign(moveDirection.x) * 90f);

        particleEffect.enabled = true;
    }
}