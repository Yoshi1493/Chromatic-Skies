using UnityEngine;

public abstract class Bullet : Projectile
{
    float HitboxSize => spriteRenderer.size.x / 2;
    protected override Collider2D CollisionCondition => Physics2D.OverlapCircle(transform.position, HitboxSize);

    protected override void HandleCollisionWithShip<TShip>(Collider2D coll)
    {
        base.HandleCollisionWithShip<TShip>(coll);
        SpawnDestructionParticles();
        Destroy();
    }

    void SpawnDestructionParticles()
    {
        print("SpawnDestructionParticles() called.");
        GameObject destructionParticles = VisualEffectPool.Instance.Get();

        UnityEngine.VFX.VisualEffect vfx = destructionParticles.GetComponent<UnityEngine.VFX.VisualEffect>();

        vfx.SetVector4("ParticleColour", spriteRenderer.color);
        destructionParticles.SetActive(true);
    }

    protected override void Move(Vector3 direction, float speed)
    {
        base.Move(direction, speed);
        transform.eulerAngles = Mathf.Atan2(-moveDirection.x, moveDirection.y) * Mathf.Rad2Deg * Vector3.forward;
    }

    #region DEBUG
    void OnDrawGizmos()
    {
        if (UnityEditor.EditorApplication.isPlaying) Gizmos.DrawSphere(transform.position, HitboxSize);
    }
    #endregion
}