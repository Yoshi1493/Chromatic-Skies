using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem6 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 36;
    const float WaveSpacing = 360f / WaveCount;
    const float MaxWaveAxialTilt = 15f;
    const int BranchCount = 9;
    const float BulletSpawnRadius = 1.5f;
    const float BulletSpawnRadiusModifier = 1.5f;
    const int BulletCount = 2;
    const float BulletSpacing = 30f;

    List<EnemyBullet> bullets = new(WaveCount * BranchCount);

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        while (enabled)
        {
            bullets.Clear();

            for (int i = 0; i < WaveCount; i++)
            {
                float r = BulletSpawnRadius;
                float t = Mathf.Lerp(MaxWaveAxialTilt, -MaxWaveAxialTilt, i / (WaveCount - 1f));
                Vector3 v = r * Vector3.up.RotateVectorBy(t);

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = t + 90f;

                    float y = Mathf.Lerp(-r, r, ii / (BranchCount - 1f));
                    float x = Mathf.Sqrt((r * r) - (y * y));
                    Vector3 pos = r * new Vector3(x, y).RotateVectorBy(t);

                    var bullet = SpawnProjectile(0, z, pos) as LeoBullet60;
                    bullet.MoveSpeed = r;
                    bullet.rotationAxis = v;
                    bullets.Add(bullet);
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);

            for (int i = 0; i < BranchCount; i++)
            {
                for (int ii = 0; ii < WaveCount; ii++)
                {
                    int b = (ii * BranchCount) + i;

                    if (bullets[b].isActiveAndEnabled)
                    {
                        for (int iii = 0; iii < BulletCount; iii++)
                        {
                            Vector3 pos = bullets[b].transform.position;
                            float z = pos.GetRotationDifference(ownerShip.transform.position) + ((iii % 2 * 2 - 1) * BulletSpacing);

                            bulletData.colour = bullets[b].SpriteRenderer.color;

                            SpawnProjectile(1, z, pos, false).Fire();
                            bullets[b].Destroy();
                        }
                    }
                }

                yield return WaitForSeconds(ShootingCooldown * 5f);
            }

            yield return WaitForSeconds(8f);
        }
    }
}