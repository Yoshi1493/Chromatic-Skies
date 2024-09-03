using System.Collections;
using UnityEngine;

public class LeoBullet61 : EnemyBullet
{
    [HideInInspector] public Vector3 rotationAxis;
    const float RotationSpeed = 180f;
    public const float FireDelay = 6f;

    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield return null;
    }

    protected override void Update()
    {
        base.Update();

        SpriteRenderer.color = projectileData.gradient.Evaluate(currentLifetime / MaxLifetime);

        if (currentLifetime < FireDelay)
        {
            transform.RotateAround(ownerShip.transform.position, rotationAxis, RotationSpeed * Time.deltaTime);
        }
    }
}