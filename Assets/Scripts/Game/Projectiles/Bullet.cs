using UnityEngine;

public abstract class Bullet : Projectile
{
    protected float HitboxSize => 0.8f * Mathf.Min(spriteRenderer.size.x, spriteRenderer.size.y) / 2f;
    protected override int NumCollisions => Physics2D.OverlapCircleNonAlloc(transform.position, HitboxSize, collisionResults, CollisionMask);

    public float MoveSpeed { get; set; }

    protected override void Update()
    {
        base.Update();
        Move(moveDirection.normalized, MoveSpeed);
    }

    protected override void HandleCollision<T>(Collider2D coll)
    {
        if (coll.TryGetComponent(out Ship ship))
        {
            if (!ship.invincible)
            {
                ship.TakeDamage(projectileData.Power.value);

                //get particle spawn position+rotation
                Vector3 pos = coll.ClosestPoint(transform.position);
                float rot = coll.transform.position.GetRotationDifference(transform.position);
                SpawnDestructionParticles(pos, rot);

                if (projectileData.destructible)
                {
                    Destroy();
                }
            }
        }
    }

    void SpawnDestructionParticles(Vector3 spawnPos, float spawnRotZ)
    {
        //grab particle obj from pool; get VFX component
        GameObject destructionParticles = VisualEffectPool.Instance.Get();
        var particleEffect = destructionParticles.GetComponent<ParticleEffect>();

        //set spawn pos+rot
        destructionParticles.transform.SetPositionAndRotation(spawnPos, Quaternion.Euler(0f, 0f, spawnRotZ));
        destructionParticles.SetActive(true);

        //set colour based on sprite colour
        particleEffect.ParticleSystem.SetVector4("ParticleColour", spriteRenderer.color);
        particleEffect.enabled = true;
    }

    protected void Move(Vector3 direction, float speed)
    {
        transform.Translate(Time.deltaTime * speed * direction, Space.World);

        //update z-rotation based on moveDirection
        transform.eulerAngles = Mathf.Atan2(-moveDirection.x, moveDirection.y) * Mathf.Rad2Deg * Vector3.forward;
    }

    public override void Destroy()
    {
        MoveSpeed = 0f;
    }

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