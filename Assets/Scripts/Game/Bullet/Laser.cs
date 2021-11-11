using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public abstract class Laser : Projectile
{
    [SerializeField] protected bool stationary;

    protected override Collider2D CollisionCondition => Physics2D.OverlapCircle(transform.position, spriteRenderer.size.x);

    IEnumerator sizeAnimation;

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

        if (sizeAnimation != null)
            StopCoroutine(sizeAnimation);

        sizeAnimation = LerpSize(Vector2.zero, originalSize);
        StartCoroutine(sizeAnimation);
    }

    IEnumerator LerpSize(Vector2 startSize, Vector2 endSize, float lerpDuration = 0.1f)
    {
        float currentLerpTime = 0f;

        spriteRenderer.size = startSize;

        while (spriteRenderer.size != endSize)
        {
            float lerpProgress = currentLerpTime / lerpDuration;

            float width = Mathf.Lerp(startSize.x, endSize.x, widthInterpolation.Evaluate(lerpProgress / 5f));
            float height = Mathf.Lerp(startSize.y, endSize.y, heightInterpolation.Evaluate(lerpProgress));

            spriteRenderer.size = new Vector2(width, height);

            currentLerpTime += Time.deltaTime;
            yield return EndOfFrame;
        }
    }

    protected override void Update()
    {
        base.Update();
        CheckCollisionWith<Player>();
    }

    protected override void Move(float moveSpeed)
    {
        if (stationary) return;
        base.Move(moveSpeed);
    }

    public override void Destroy()
    {
        base.Destroy();
        EnemyLaserPool.Instance.ReturnToPool(this);
    }

    #region DEBUG
    public void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, spriteRenderer.size.x);
    }
    #endregion
}