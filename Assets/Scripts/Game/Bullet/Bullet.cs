using UnityEngine;

public abstract class Bullet : Projectile
{
    protected override Collider2D CollisionCondition => Physics2D.OverlapCircle(transform.position, HitboxSize, CollisionMask);

    protected override void HandleCollisionWithShip<TShip>(Collider2D coll)
    {
        base.HandleCollisionWithShip<TShip>(coll);
        Destroy();
    }

    protected virtual void SpawnDestructionParticles(Vector3 spawnPos, float spawnRotZ)
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

    protected override void Move(Vector3 direction, float speed)
    {
        base.Move(direction, speed);

        //update z-rotation based on moveDirection
        transform.eulerAngles = Mathf.Atan2(-moveDirection.x, moveDirection.y) * Mathf.Rad2Deg * Vector3.forward;
    }
}