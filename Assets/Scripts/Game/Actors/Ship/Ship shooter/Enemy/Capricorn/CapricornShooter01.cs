using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CapricornShooter01 : CommonEnemyShooter<EnemyBullet>
{
    const int RepeatCount = 2;
    const int WaveCount = 36;
    const float WaveSpacing = 10f;
    const int BranchCount = 3;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletCount = 2;
    const float BulletSpacing = 3f;
    const float BulletRotationSpeed = 90f;
    const float BulletBaseSpeed = 2.0f;
    const float BulletSpeedModifier = 0.5f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        float d = Mathf.Sign(parentShip.transform.position.x);

        for (int i = 0; i < RepeatCount; i++)
        {
            for (int ii = 0; ii < WaveCount; ii++)
            {
                for (int iii = 0; iii < BranchCount; iii++)
                {
                    for (int iv = 0; iv < BulletCount; iv++)
                    {
                        float z = (ii * WaveSpacing) + (iii * BranchSpacing) + (iv * BulletSpacing);
                        float s = BulletBaseSpeed + (iv * BulletSpeedModifier);
                        Vector3 pos = Vector3.zero;

                        bulletData.colour = bulletData.gradient.Evaluate(iv);

                        var bullet = SpawnProjectile(1, z, pos);
                        bullet.StartCoroutine(bullet.RotateBy(d * BulletRotationSpeed, 0f, delay: 1.5f));
                        bullet.StartCoroutine(bullet.LerpSpeed(1f, s, 2f, delay: 1.5f));
                        bullet.Fire();
                    }
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(5f);
        }

    }
}