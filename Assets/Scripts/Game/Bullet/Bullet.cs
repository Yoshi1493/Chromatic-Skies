using UnityEngine;

public abstract class Bullet : Projectile
{
    protected override Collider2D CollisionCondition => Physics2D.OverlapCircle(transform.position, HitboxSize, CollisionMask);

    protected override void HandleCollisionWithShip<TShip>(Collider2D coll)
    {
        base.HandleCollisionWithShip<TShip>(coll);

        Vector3 pos = transform.position;
        float zRot = coll.transform.position.GetRotationDifference(transform.position);
        SpawnDestructionParticles(pos, zRot);

        Destroy();
    }

    void SpawnDestructionParticles(Vector3 pos, float zRot)
    {
        GameObject destructionParticles = VisualEffectPool.Instance.Get();
        var particleEffect = destructionParticles.GetComponent<ParticleEffect>();

        destructionParticles.transform.SetPositionAndRotation(pos, Quaternion.Euler(0f, 0f, zRot));
        destructionParticles.SetActive(true);

        particleEffect.ParticleSystem.SetVector4("ParticleColour", spriteRenderer.color);
        particleEffect.enabled = true;
    }

    protected override void Move(Vector3 direction, float speed)
    {
        base.Move(direction, speed);
        transform.eulerAngles = Mathf.Atan2(-moveDirection.x, moveDirection.y) * Mathf.Rad2Deg * Vector3.forward;
    }
}