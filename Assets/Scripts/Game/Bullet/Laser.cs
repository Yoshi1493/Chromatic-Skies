using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public abstract class Laser : Projectile
{
    Vector3 HitboxOffset => spriteRenderer.size.y * 0.5f * Vector3.up;
    protected override Collider2D CollisionCondition => Physics2D.OverlapBox(transform.position + HitboxOffset, spriteRenderer.size, 0f);

    IEnumerator growAnimation;
    IEnumerator shrinkAnimation;

    [SerializeField] AnimationCurve widthInterpolation;
    [SerializeField] AnimationCurve heightInterpolation;
    Vector2 originalSize;

    protected override void Awake()
    {
        base.Awake();
        originalSize = spriteRenderer.size;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        if (growAnimation != null)
        {
            StopCoroutine(growAnimation);
        }

        growAnimation = Grow(originalSize);
        StartCoroutine(growAnimation);
    }

    protected override void Update()
    {
        CheckCollisionWith<Player>();
        IncrementLifetime();
    }

    public override void Destroy()
    {
        if (shrinkAnimation == null)
        {
            shrinkAnimation = ShrinkAndDestroy();
            StartCoroutine(shrinkAnimation);
        }
    }

    IEnumerator Grow(Vector2 endSize, float lerpDuration = 0.1f)
    {
        float currentLerpTime = 0f;

        Vector2 startSize = Vector2.zero;
        spriteRenderer.size = startSize;

        while (spriteRenderer.size != endSize)
        {
            float lerpProgress = currentLerpTime / lerpDuration;

            float width = Mathf.Lerp(startSize.x, endSize.x, widthInterpolation.Evaluate(lerpProgress / 2f));
            float height = Mathf.Lerp(startSize.y, endSize.y, heightInterpolation.Evaluate(lerpProgress));

            spriteRenderer.size = new Vector2(width, height);

            currentLerpTime += Time.deltaTime;
            yield return EndOfFrame;
        }
    }

    IEnumerator ShrinkAndDestroy(float lerpDuration = 0.1f)
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

    protected override void OnDrawGizmos()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawCube(HitboxOffset, spriteRenderer.size);
        }
    }
}