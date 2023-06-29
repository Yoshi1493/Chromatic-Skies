using System.Collections;
using UnityEngine;

public class GeminiBullet30 : EnemyBullet
{
    [HideInInspector] public Vector3 rotationPoint;
    [HideInInspector] public Vector3 rotationAxis;
    const float RotationSpeed = 240f;

    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 4f;
        yield return null;
    }

    protected override void Update()
    {
        base.Update();
        transform.RotateAround(rotationPoint, rotationAxis, RotationSpeed * Time.deltaTime);
    }
}