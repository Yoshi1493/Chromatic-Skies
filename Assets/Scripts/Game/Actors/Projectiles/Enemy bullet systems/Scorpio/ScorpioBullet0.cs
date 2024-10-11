using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBullet0 : EnemyBullet
{
    new CircleCollider2D collider;

    [SerializeField] AnimationCurve growCurve;
    const float GrowDuration = 1f;
    const float EndRadius = 27f;

    protected override int MaxCollisions => 256;
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
        SpriteRenderer.size = Vector2.zero;
    }

    protected override IEnumerator Move()
    {
        float currentLerpTime = 0f;

        while (currentLerpTime < GrowDuration)
        {
            float t = growCurve.Evaluate(currentLerpTime / GrowDuration);

            SpriteRenderer.size = Vector2.Lerp(Vector2.zero, 2f * EndRadius * Vector3.one, t);
            collider.radius = Mathf.Lerp(0f, EndRadius, t);

            yield return EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }

        yield return WaitForSeconds(0.5f);
        Destroy();
    }

    protected override void Update()
    {
        CheckCollisionWith<ITimestoppable>();
    }

    protected override void HandleCollision(Collider2D coll)
    {
        if (coll.TryGetComponent(out ITimestoppable bullet))
        {
            bullet.Stop();
        }
    }

#if UNITY_EDITOR
    protected override void OnDrawGizmos()
    {
        return;
    }
#endif
}