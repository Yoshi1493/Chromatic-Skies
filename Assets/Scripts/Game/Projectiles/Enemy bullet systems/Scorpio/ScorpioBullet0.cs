using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBullet0 : EnemyBullet
{
    new CircleCollider2D collider;

    [SerializeField] AnimationCurve growCurve;
    const float GrowDuration = 1f;
    const float EndRadius = 20f;

    protected override int MaxCollisions => 16;
    protected override int CollisionMask => 1 << LayerMask.NameToLayer("Enemy bullet");
    protected override int NumCollisions => Physics2D.OverlapCircleNonAlloc(transform.position, HitboxSize, collisionResults, CollisionMask);

    protected override float MaxLifetime => GrowDuration;

    protected override void Awake()
    {
        base.Awake();
        collider = GetComponent<CircleCollider2D>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        spriteRenderer.size = Vector2.zero;
    }

    protected override IEnumerator Move()
    {
        float currentLerpTime = 0f;

        while (currentLerpTime < GrowDuration)
        {
            float t = growCurve.Evaluate(currentLerpTime / GrowDuration);

            spriteRenderer.size = Vector2.Lerp(Vector2.zero, 2f * EndRadius * Vector3.one, t);
            collider.radius = Mathf.Lerp(0f, EndRadius, t);

            yield return EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }

        Destroy();
    }

    protected override void Update()
    {
        CheckCollisionWith<ScorpioBullet20>();
    }

    protected override void HandleCollision<T>(Collider2D coll)
    {
        if (coll.TryGetComponent(out ScorpioBullet20 bullet))
        {
            bullet.Stop();
        }
    }

    protected override void OnDrawGizmos()
    {
        return;
    }
}