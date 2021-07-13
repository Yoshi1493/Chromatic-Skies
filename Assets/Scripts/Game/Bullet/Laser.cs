using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class Laser : Projectile
{
    [SerializeField] AnimationCurve widthInterpolation;

    Vector2 originalSize;

    protected override void Awake()
    {
        base.Awake();

        originalSize = spriteRenderer.size;
        MoveSpeed = 0;
    }

    void OnEnable()
    {
        StartCoroutine(Expand());
    }

    IEnumerator Expand()
    {
        float currentLerpTime = 0f;
        float totalLerpTime = 0.1f;

        Vector2 startSize = Vector2.zero;
        Vector2 endSize = originalSize;

        spriteRenderer.size = startSize;

        while (spriteRenderer.size != endSize)
        {
            float lerpProgress = currentLerpTime / totalLerpTime;

            float width = Mathf.Lerp(startSize.x, endSize.x, widthInterpolation.Evaluate(lerpProgress / 5f));
            float height = Mathf.Lerp(startSize.y, endSize.y, lerpProgress);

            spriteRenderer.size = new Vector2(width, height);

            yield return EndOfFrame;
            currentLerpTime += Time.deltaTime;
        }
    }

    protected virtual void Update()
    {
        Vector2 boxOrigin = transform.position + (transform.up * spriteRenderer.size.y / 2);
        Vector2 boxSize = spriteRenderer.size;

        CheckCollisionWith<Player>(() => Physics2D.OverlapBox(boxOrigin, boxSize, transform.eulerAngles.z));
    }

    public override void Destroy()
    {
        EnemyLaserPool.Instance.ReturnToPool(0, this);
    }

    //void OnDrawGizmos()
    //{
    //    Transform t = GetComponent<Transform>();
    //    SpriteRenderer sr = GetComponent<SpriteRenderer>();
    //    Gizmos.DrawCube(t.position + (Vector3.up * sr.size.y / 2), sr.size);
    //}
}