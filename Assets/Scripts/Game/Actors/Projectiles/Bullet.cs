using UnityEngine;

public abstract class Bullet : Projectile
{
    protected float HitboxSize => 0.8f * Mathf.Min(SpriteRenderer.size.x, SpriteRenderer.size.y) / 2f;
    protected override int NumCollisions => Physics2D.OverlapCircleNonAlloc(transform.position, HitboxSize, collisionResults, CollisionMask);

    public float MoveSpeed { get; set; }

    protected override void Update()
    {
        base.Update();
        Move(moveDirection.normalized, MoveSpeed);
    }

    protected override void HandleCollision(Collider2D coll)
    {
        //get particle spawn position+rotation
        Vector3 pos = coll.ClosestPoint(transform.position);
        float rot = coll.transform.position.GetRotationDifference(transform.position);

        if (coll.TryGetComponent(out Ship ship))
        {
            if (ship.Invincible)
            {
                if (ship is Enemy)
                {
                    ship.DisplayInvincibleShield(pos);
                }
            }
            else
            {
                int damage = DamageCalculator.CalculateDamage(projectileData.Power.value, ship.shipData.Defense.Value, MoveSpeed);
                ship.TakeDamage(damage);
            }

            if (projectileData.destructible)
            {
                Destroy();
            }

            SpawnDestructionParticles(pos, rot);
        }
        else if (coll.TryGetComponent(out PlayerGraze playerGraze))
        {
            if (!hasGrazed)
            {
                playerGraze.GrazePlayer();
                SpawnDestructionParticles(pos, rot);

                hasGrazed = true;
            }
        }
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