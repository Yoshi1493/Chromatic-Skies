using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class GeminiBulletSystem21 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 50;
    const float WaveSpacing = 0.25f;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 2;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.02f;

    List<(Vector2 pos, float z)> bulletSpawnData = new(WaveCount * BranchCount * BulletCount);

    protected override float ShootingCooldown => 1f / 60;

    protected override IEnumerator Shoot()
    {
        bulletSpawnData.Clear();
        float r = Random.Range(45f, 75f);

        for (int i = 1; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    int d = iii % 2 * 2 - 1;

                    float z = d * (r - 90f);
                    Vector3 v1 = i * WaveSpacing * transform.up.RotateVectorBy(d * r);
                    Vector3 pos = v1.RotateVectorBy(ii * BranchSpacing);

                    SpawnProjectile(2, z, pos);
                    bulletSpawnData.Add((pos, z));
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        yield return WaitForSeconds(2f);

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

                    var bullet = SpawnProjectile(3, z, pos);
                    bullet.MoveSpeed = s;
                    bullet.Fire();
                }

                bulletSpawnData.RemoveAt(0);
            }

            yield return WaitForSeconds(ShootingCooldown * 3f);
        }

        enabled = false;
    }
}