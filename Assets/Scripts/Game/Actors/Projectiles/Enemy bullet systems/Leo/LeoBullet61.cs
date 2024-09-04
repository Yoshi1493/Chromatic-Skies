using System.Collections;
using UnityEngine;

public class LeoBullet61 : EnemyBullet
{
    [HideInInspector] public Vector3 rotationAxis;
    const float RotationSpeed = 180f;
    bool preFire = false;

    protected override float MaxLifetime => 12f;

    protected override void OnEnable()
    {
        base.OnEnable();
        preFire = false;
    }

    protected override IEnumerator Move()
    {
        preFire = true;
        float startSpeed = MoveSpeed;
        yield return this.LerpSpeed(startSpeed, 2.5f, 1f);
    }

    protected override void Update()
    {
        base.Update();

        SpriteRenderer.color = projectileData.gradient.Evaluate(currentLifetime / MaxLifetime);

        if (!preFire)
        {
            transform.RotateAround(ownerShip.transform.position, rotationAxis, RotationSpeed * Time.deltaTime);
        }
    }

    public override void Destroy()
    {
        preFire = false;
        base.Destroy();
    }
}