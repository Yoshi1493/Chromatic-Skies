using System.Collections;
using UnityEngine;

public class SpecialYellow : SpecialBullet
{
    protected override int MaxCollisions => 8;
    protected override int NumCollisions => Physics2D.OverlapBoxNonAlloc(transform.position, SpriteRenderer.size, transform.eulerAngles.z, collisionResults, CollisionMask);

    [SerializeField] AnimationCurve homingInterpolation;

    protected override void OnEnable()
    {
        base.OnEnable();
        collider.enabled = true;
    }

    protected override IEnumerator Move()
    {
        collider.enabled = false;

        float currentLerpTime = 0f;
        float totalLerpTime = Random.Range(0.8f, 1.2f);

        StartCoroutine(this.LerpSize(Vector2.zero, totalLerpTime * 0.75f));

        while (currentLerpTime < totalLerpTime)
        {
            float t = homingInterpolation.Evaluate(currentLerpTime / totalLerpTime);
            transform.position = Vector2.Lerp(transform.position, playerShip.transform.position, t);

            yield return null;
            currentLerpTime += Time.deltaTime;
        }

        Destroy();
    }
}