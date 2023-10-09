using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public abstract class Laser : Projectile
{
    Vector3 HitboxOffset => originalSize.y * 0.5f * transform.up;
    protected override int CollisionMask => 1 << LayerMask.NameToLayer("Player");
    protected override int NumCollisions => Physics2D.OverlapBoxNonAlloc(transform.position + HitboxOffset, originalSize, transform.eulerAngles.z, collisionResults, CollisionMask);

    protected bool active;
    protected Vector2 originalSize;
    protected Vector2 activeSize;
    const float WarningSpriteWidth = 0.04f;

    IEnumerator growAnimation;
    IEnumerator shrinkAnimation;

    [SerializeField] AnimationCurve widthInterpolation;
    [SerializeField] AnimationCurve heightInterpolation;

    protected override void Awake()
    {
        base.Awake();

        originalSize = spriteRenderer.size;
        activeSize = originalSize;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        active = false;
    }

    public void Fire(float delay = 0.5f)
    {
        if (growAnimation != null)
        {
            StopCoroutine(growAnimation);
        }

        growAnimation = Grow(delay);
        StartCoroutine(growAnimation);
    }

    protected override void Update()
    {
        base.Update();

        if (active)
        {
            CheckCollisionWith<Player>();
        }
    }

    protected override void HandleCollision<T>(Collider2D coll)
    {
        if (coll.TryGetComponent(out Ship ship))
        {
            if (!ship.invincible)
            {
                ship.TakeDamage(projectileData.Power.value);
            }
        }
    }

    IEnumerator Grow(float warningDuration, float lerpDuration = 0.1f)
    {
        //continuously update sprite renderer initial size in case laser path changes length during warning delay (usually due to collisions)
        for (float _ = 0; _ < warningDuration; _ += Time.deltaTime)
        {
            spriteRenderer.size = new(WarningSpriteWidth, IsColliding ? activeSize.y : originalSize.y);
            yield return EndOfFrame;
        }

        //determine animation start and end points after delay (in case it collides with something during warning delay)
        Vector2 currentSize = spriteRenderer.size;
        Vector2 endSize = activeSize;
        float currentLerpTime = 0f;

        //animate laser from warning size to active size
        while (spriteRenderer.size != endSize)
        {
            float lerpProgress = currentLerpTime / lerpDuration;

            float width = Mathf.Lerp(currentSize.x, endSize.x, widthInterpolation.Evaluate(lerpProgress / 2f));
            float height = Mathf.Lerp(currentSize.y, endSize.y, heightInterpolation.Evaluate(lerpProgress));

            spriteRenderer.size = new Vector2(width, height);

            currentLerpTime += Time.deltaTime;
            yield return EndOfFrame;
        }

        active = true;
        growAnimation = null;
    }

    IEnumerator Destroy(float lerpDuration = 0.1f)
    {
        float currentLerpTime = 0f;
        Vector2 startSize = spriteRenderer.size;

        while (spriteRenderer.size.x != 0f)
        {
            float lerpProgress = currentLerpTime / lerpDuration;

            float width = Mathf.Lerp(startSize.x, 0f, heightInterpolation.Evaluate(lerpProgress));
            spriteRenderer.size = new Vector2(width, spriteRenderer.size.y);

            currentLerpTime += Time.deltaTime;
            yield return EndOfFrame;
        }

        EnemyLaserPool.Instance.ReturnToPool(this);
        shrinkAnimation = null;
    }

    public override void Destroy()
    {
        if (growAnimation != null)
        {
            StopCoroutine(growAnimation);
        }

        if (shrinkAnimation == null)
        {
            shrinkAnimation = Destroy();
            StartCoroutine(shrinkAnimation);
        }
    }

    void OnDisable()
    {
        active = false;
    }

#if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawCube(0.5f * spriteRenderer.size.y * Vector3.up, spriteRenderer.size);
        }
    }
#endif
}