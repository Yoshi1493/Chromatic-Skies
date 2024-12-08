using System.Collections;
using UnityEngine;

public class SpecialYellow : SpecialBullet
{
    protected override int MaxCollisions => 8;

    [SerializeField] AnimationCurve homingInterpolation;

    Vector2 originalSize;

    protected override void Awake()
    {
        base.Awake();
        originalSize = SpriteRenderer.size;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        SpriteRenderer.size = originalSize;
    }

    protected override IEnumerator Move()
    {
        collider.enabled = false;

        float currentLerpTime = 0f;
        float totalLerpTime = Random.Range(0.8f, 1.2f);

        StartCoroutine(this.LerpSize(Vector2.zero, totalLerpTime * 0.8f, delay: totalLerpTime * 0.2f));

        while (currentLerpTime < totalLerpTime)
        {
            float t = homingInterpolation.Evaluate(currentLerpTime / totalLerpTime);
            transform.position = Vector2.Lerp(transform.position, playerShip.transform.position, t * t);

            yield return null;
            currentLerpTime += Time.deltaTime;
        }

        Destroy();
    }
}