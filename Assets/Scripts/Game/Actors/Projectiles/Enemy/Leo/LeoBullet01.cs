using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoBullet01 : EnemyBullet
{
    [HideInInspector] public Vector3 rotationPoint;

    Vector3 rotationAxis;
    const float RotationSpeed = 180f;
    const float FireDelay = 2f;

    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;
        yield return WaitForSeconds(FireDelay);

        this.LookAt(rotationPoint);
        StartCoroutine(this.LerpSpeed(4f, 2f, 2f));
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        rotationAxis = Vector3.right.RotateVectorBy(transform.eulerAngles.z);
        rotationAxis.z = 0.1f;
    }

    protected override void Update()
    {
        base.Update();

        if (currentLifetime < FireDelay)
        {
            transform.RotateAround(rotationPoint, rotationAxis, RotationSpeed * Time.deltaTime);
        }

        if (currentLifetime < 0.5f)
        {
            Color c = SpriteRenderer.color;
            c.a = currentLifetime * 2f;
            SpriteRenderer.color = c;
        }
    }
}