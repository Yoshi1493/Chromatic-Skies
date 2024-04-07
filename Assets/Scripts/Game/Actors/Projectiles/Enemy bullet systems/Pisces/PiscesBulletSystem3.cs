using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBulletSystem3 : EnemyShooter<EnemyBullet>
{
    const float WaveSpacing = 10f;
    const int BranchCount = 3;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 5;
    const float BulletSpacing = 15f;
    const float BulletBaseSpeed = 3f;

    protected override float ShootingCooldown => 0.2f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        StartMoveAction?.Invoke();
        SetSubsystemEnabled(1);

        for (int i = 0; enabled; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                for (int iii = 0; iii < BulletCount; iii++)
                {
                    float z = (i * WaveSpacing) + (ii * BranchSpacing);
                    float r = (iii - ((BulletCount - 1) / 2f)) * BulletSpacing;
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(iii / (BulletCount - 1f));

                    var bullet = SpawnProjectile(0, z, pos);
                    bullet.StartCoroutine(bullet.LerpSpeed(BulletBaseSpeed * 2, 1f, 0.5f));
                    bullet.StartCoroutine(bullet.RotateBy(r, 2f, delay: 1f));
                    bullet.StartCoroutine(bullet.LerpSpeed(1f, BulletBaseSpeed, 1f, delay: 1f));
                }
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}