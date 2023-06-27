using System.Collections;
using UnityEngine;

public class GeminiBullet30 : EnemyBullet
{
    Vector3 rotationPoint;
    Vector3 rotationAxis;
    const float RotationSpeed = 360f;

    protected override float MaxLifetime => 4f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 3f;
        yield return null;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        rotationPoint = Mathf.Sign(transform.position.x) * GeminiBulletSystem3.BranchSpawnOffset * Vector3.right;
        rotationAxis = Mathf.Sign(transform.position.x) * Vector3.up;
    }

    protected override void Update()
    {
        base.Update();
        transform.RotateAround(rotationPoint, rotationAxis, RotationSpeed * Time.deltaTime);
    }
}