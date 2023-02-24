using UnityEngine;

public abstract class Bullet : Projectile
{
    protected override Collider2D CollisionCondition => Physics2D.OverlapCircle(transform.position, HitboxSize, CollisionMask);
    public float MoveSpeed { get; set; }

    protected override void Update()
    {
        base.Update();
        Move(moveDirection.normalized, MoveSpeed);
    }

    protected override void HandleCollisionWithShip<TShip>(Collider2D coll)
    {
        TShip ship = coll.GetComponent<TShip>();

        if (!ship.invincible)
        {
            ship.TakeDamage(projectileData.Power.value);

            //get particle spawn position+rotation
            Vector3 pos = coll.ClosestPoint(transform.position);
            float rot = coll.transform.position.GetRotationDifference(transform.position);
            SpawnDestructionParticles(pos, rot);
        }

        if (projectileData.destructible)
        {
            Destroy();
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

    public override void Fire() { }

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
}