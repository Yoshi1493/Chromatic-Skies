using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;
public class GeminiBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 40;
    const float WaveSpacing = 0.3f;
    const int BranchCount = 2;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletBaseSpeed = 4f;
    const float BulletSpeedModifier = 0.03f;
    const float BulletSpawnOffset = 0.5f;

    protected override float ShootingCooldown => 1 / 60f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        int i = 1;
        List<(Vector2 pos, float z)> bulletSpawnData = new(WaveCount * BranchCount);

        while (enabled)
        {
            float r = Random.Range(45f, 75f) * i;

            for (int ii = 1; ii < WaveCount; ii++)
            {
                float z = r - 90f * Mathf.Sign(r);

                Vector3 v1 = ii * WaveSpacing * transform.up.RotateVectorBy(r);

                for (int iii = 0; iii < BranchCount; iii++)
                {
                    Vector3 pos = v1.RotateVectorBy(iii * BranchSpacing);

                    bulletSpawnData.Add((pos, z));
                    SpawnProjectile(0, z, pos).Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(0.5f);

            SetSubsystemEnabled(1);
            yield return WaitForSeconds(3f);

            bulletSpawnData.Randomize();

            for (int ii = 1; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    var data = bulletSpawnData[0];
                    float z = data.z;
                    float s = BulletBaseSpeed - (ii * BulletSpeedModifier);
                    Vector3 pos = new(data.pos.x, data.pos.y);

                    bulletData.colour = bulletData.gradient.Evaluate(ii / (WaveCount - 1f));
                    var bullet = SpawnProjectile(1, z, pos);
                    bullet.MoveSpeed = s;
                    bullet.Fire();

                    bulletSpawnData.RemoveAt(0);
                }

                yield return WaitForSeconds(ShootingCooldown * 2f);
            }

            bulletSpawnData.Clear();

            yield return WaitForSeconds(1f);

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(1f);

            i *= -1;
        }
    }
}