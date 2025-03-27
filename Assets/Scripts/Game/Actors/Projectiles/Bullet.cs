using System.Collections;
using UnityEngine;

public abstract class Bullet : Projectile
{
    protected virtual float HitboxSize => 0.8f * Mathf.Min(SpriteRenderer.size.x, SpriteRenderer.size.y) / 2f;
    protected override int NumCollisions => Physics2D.OverlapCircleNonAlloc(transform.position, HitboxSize, collisionResults, CollisionMask);

    protected IEnumerator movementBehaviour;
    protected abstract IEnumerator Move();

    protected Player playerShip;

    protected override void Awake()
    {
        base.Awake();
        playerShip = FindObjectOfType<Player>();
    }

    public void Fire()
    {
        if (movementBehaviour != null)
        {
            StopCoroutine(movementBehaviour);
        }

        movementBehaviour = Move();
        StartCoroutine(movementBehaviour);
    }

    protected override void Update()
    {
        base.Update();

        transform.Translate(Time.deltaTime * MoveSpeed * moveDirection.normalized, Space.World);

        //update z-rotation based on moveDirection
        transform.eulerAngles = Mathf.Atan2(-moveDirection.x, moveDirection.y) * Mathf.Rad2Deg * Vector3.forward;
    }

    protected override void HandleCollision(Collider2D coll)
    {
        Vector3 pos = coll.ClosestPoint(transform.position);

        if (coll.TryGetComponent(out Ship ship))
        {
            if (ship.Invincible)
            {
                if (ship is Boss boss)
                {
                    boss.DisplayInvincibleShield(pos);
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

            SpawnDestructionParticles(pos);
        }
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