using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public abstract class Laser : Projectile
{
    Vector3 HitboxOffset => spriteRenderer.size.y * 0.5f * transform.up;
    protected override int CollisionMask => 1 << LayerMask.NameToLayer("Player");
    protected override Collider2D CollisionCondition => Physics2D.OverlapBox(transform.position + HitboxOffset, spriteRenderer.size, transform.eulerAngles.z, CollisionMask);

    protected bool active;
    protected Vector2 originalSize;

    IEnumerator growAnimation;
    IEnumerator shrinkAnimation;

    [SerializeField] AnimationCurve widthInterpolation;
    [SerializeField] AnimationCurve heightInterpolation;

    protected override void Awake()
    {
        base.Awake();
        originalSize = spriteRenderer.size;
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

    IEnumerator Grow(float warningDuration, float lerpDuration = 0.1f)
    {
        Vector2 startSize = originalSize;
        Vector2 endSize = startSize;
        startSize.x = 0.25f * originalSize.x;

        //display laser warning
        spriteRenderer.size = startSize;
        yield return WaitForSeconds(warningDuration);

        active = true;
        float currentLerpTime = 0f;

        //activate laser
        while (spriteRenderer.size != endSize)
        {
            float lerpProgress = currentLerpTime / lerpDuration;

            float width = Mathf.Lerp(startSize.x, endSize.x, widthInterpolation.Evaluate(lerpProgress / 2f));
            float height = Mathf.Lerp(startSize.y, endSize.y, heightInterpolation.Evaluate(lerpProgress));

            spriteRenderer.size = new Vector2(width, height);

            currentLerpTime += Time.deltaTime;
            yield return EndOfFrame;
        }

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

#if UNITY_EDITOR
    protected override void OnDrawGizmos()
    {
        if (UnityEditor.EditorApplication.isPlaying)
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawCube(0.5f * spriteRenderer.size.y * Vector3.up, spriteRenderer.size);
        }
    }
#endif
}