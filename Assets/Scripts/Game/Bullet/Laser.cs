using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public abstract class Laser : Projectile
{
    protected override Collider2D CollisionCondition => Physics2D.OverlapBox(transform.position + (transform.up * spriteRenderer.size.y / 2), spriteRenderer.size, transform.eulerAngles.z);

    [SerializeField] AnimationCurve widthInterpolation;
    [SerializeField] AnimationCurve heightInterpolation;
    Vector2 originalSize;

    protected override void Awake()
    {
        base.Awake();
        originalSize = spriteRenderer.size;
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

    public void Fire()
    {
        if (movementBehaviour != null)
            StopCoroutine(movementBehaviour);

        movementBehaviour = LerpSize(Vector2.zero, originalSize);
        StartCoroutine(movementBehaviour);
    }

    protected virtual void Update()
    {
        CheckCollisionWith<Player>();
    }

    public override void Destroy()
    {
        EnemyLaserPool.Instance.ReturnToPool(this);
    }

}