using System.Collections;
using UnityEngine;

public class LeoBullet61 : EnemyBullet
{
    [HideInInspector] public Vector3 rotationAxis;
    const float RotationSpeed = 180f;

    protected override IEnumerator Move()
    {
        yield return null;
    }

    protected override void Update()
    {
        base.Update();

        SpriteRenderer.color = projectileData.gradient.Evaluate(currentLifetime / MaxLifetime);
        transform.RotateAround(ownerShip.transform.position, rotationAxis, RotationSpeed * Time.deltaTime);
    }
}