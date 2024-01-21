using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class GeminiBulletSystem21 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 43;
    const float WaveSpacing = 0.3f;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletBaseSpeed = 3.5f;
    const float BulletSpeedModifier = -0.03f;

    List<(Vector2 pos, float z)> bulletSpawnData = new(WaveCount * BranchCount);

    protected override float ShootingCooldown => 1f / 60;

    protected override IEnumerator Shoot()
    {
        int d = PositiveOrNegativeOne;
        bulletSpawnData.Clear();
        float r = Random.Range(45f, 75f) * d;

        for (int i = 1; i < WaveCount; i++)
        {
            Vector3 v1 = i * WaveSpacing * transform.up.RotateVectorBy(r);

            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = r - 90f * Mathf.Sign(r);
                Vector3 pos = v1.RotateVectorBy(ii * BranchSpacing) + transform.position;

                SpawnProjectile(2, z, pos, false);

                pos.x *= -1;
                z *= -1;
                bulletSpawnData.Add((pos, z));
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        yield return WaitForSeconds(1f);

        bulletSpawnData.Randomize();

        for (int i = 1; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    var data = bulletSpawnData[0];
                    float z = data.z + (iii * BulletSpacing);
                    float s = BulletBaseSpeed + (i * BulletSpeedModifier);
                    Vector3 pos = new(data.pos.x, data.pos.y);

                    bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                    var bullet = SpawnProjectile(3, z, pos, false);
                    bullet.MoveSpeed = s;
                    bullet.Fire();
                }

                bulletSpawnData.RemoveAt(0);
            }

            yield return WaitForSeconds(ShootingCooldown * 2f);
        }

        enabled = false;
    }
}