using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBullet50 : ScriptableBossBullet<GeminiBulletSystem5, BossBullet>
{
    [Space]
    [SerializeField] ProjectileObject bulletData;

    const float SpriteAlpha = 0.5f;

    const float WaveSpacing = -30f;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletSpacing = 5f;
    const float BulletRotationSpeed = -90f;
    const float ShootingCooldown = 0.4f;

    protected override int NumCollisions => 0;
    protected override float MaxLifetime => Mathf.Infinity;

    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(2f);

        for (int i = 0; enabled; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = (i * WaveSpacing) + (ii * BranchSpacing) + ((iii - ((BulletCount - 1) / 2f)) * BulletSpacing);
                    Vector3 pos = transform.position;

                    bulletData.colour = bulletData.gradient.Evaluate(iii);

                    var bullet = SpawnBullet(1, z, pos, false);
                    bullet.StartCoroutine(bullet.RotateBy((iii % 2 * 2 - 1) * BulletRotationSpeed, 0f, delay: 1f));
                    bullet.Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }

    protected override void Update()
    {
        base.Update();
        UpdatePosition();

        if (currentLifetime < SpriteAlpha)
        {
            Color c = SpriteRenderer.color;
            c.a = currentLifetime;
            SpriteRenderer.color = c;
        }
    }

    void UpdatePosition()
    {
        Vector3 pos = parentShip.transform.position;
        pos.x *= -1;
        transform.position = pos;
    }
}