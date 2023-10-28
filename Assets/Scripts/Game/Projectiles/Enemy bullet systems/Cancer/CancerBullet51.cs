using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBullet51 : EnemyBullet
{
    Vector3 rotationPoint;
    Vector3 rotationAxis;
    const float RotationSpeed = 180f;
    const float FireDelay = 1.5f;

    protected override float MaxLifetime => 6f;

    protected override void Awake()
    {
        base.Awake();
        rotationPoint = FindObjectOfType<CancerBullet50>().transform.position;
    }

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        MoveSpeed = 0f;
        yield return WaitForSeconds(FireDelay);

        StartCoroutine(this.LerpSpeed(endSpeed, 4f, 2f));
        this.LookAt(rotationPoint);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        rotationAxis = Vector3.right.RotateVectorBy(transform.eulerAngles.z);
    }

    protected override void Update()
    {
        base.Update();
        if (currentLifetime < FireDelay)
        {
            transform.RotateAround(rotationPoint, rotationAxis, RotationSpeed * Time.deltaTime);
        }
    }
}