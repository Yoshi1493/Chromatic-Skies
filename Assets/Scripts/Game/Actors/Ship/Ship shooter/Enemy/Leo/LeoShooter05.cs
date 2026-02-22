using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LeoShooter05 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 3;
    const int BranchCount = 20;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletRotationSpeed = 60f;
    const float BulletRotationDuration = 3f;

    protected override float ShootingCooldown => 1f;

    public event System.Func<IEnumerator> ShootFunc;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        int i = 1;

        while (enabled)
        {
            ShootFunc?.Invoke();

            yield return WaitForSeconds(13f - (WaveCount * ShootingCooldown));

            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    float z = iii * BranchSpacing;
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(i / 2f + 0.5f);

                    var bullet = SpawnProjectile(5, z, pos);
                    bullet.StartCoroutine(bullet.RotateBy(i * BulletRotationSpeed, BulletRotationDuration));
                    bullet.Fire();
                }

                yield return WaitForSeconds(ShootingCooldown);
                i *= -1;
            }            
        }
    }
}