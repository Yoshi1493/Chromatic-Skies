using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static CameraBoundaries;

public class LeoBossShooter42 : BossShooter<BossBullet>
{
    const int WaveCount = 40;
    const int BranchCount = 2;
    const int BulletCount = 3;
    const float MaxBulletSpawnOffset = 4f;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.5f;

    protected override IEnumerator Shoot()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float x = parentShip.transform.position.x + Random.Range(-MaxBulletSpawnOffset, MaxBulletSpawnOffset);

                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = 0f;
                    float s = BulletBaseSpeed + (iii * BulletSpeedModifier);
                    float y = ScreenHalfHeight;
                    Vector3 pos = new(x, y);

                    bulletData.colour = bulletData.gradient.Evaluate(iii / (BulletCount - 1f));

                    var bullet = SpawnProjectile(2, z, pos, false);
                    bullet.StartCoroutine(bullet.LerpSpeed(BulletBaseSpeed, s, 2f));
                    bullet.Fire();
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}