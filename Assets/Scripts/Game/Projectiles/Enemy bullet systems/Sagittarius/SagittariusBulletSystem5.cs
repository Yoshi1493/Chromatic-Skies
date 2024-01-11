using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class SagittariusBulletSystem5 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 18;
    const int BulletCount = 12;
    const float BulletBaseSpeed = 0.5f;
    const float BulletSpeedModifier = 0.75f;

    List<EnemyBullet> bullets = new(WaveCount * BulletCount);

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            bullets.Clear();

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(ShootingCooldown);

            Vector3 d = ownerShip.GetComponent<Enemy>().GetCurrentMovementSystem().moveDirection;
            d *= -Mathf.Sign(d.x);

            float z = d.RotateVectorBy(90f).GetRotationDifference(Vector3.zero);
            Vector3 pos = Vector3.zero;

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BulletCount; ii++)
                {
                    float s = BulletBaseSpeed + (ii * BulletSpeedModifier);

                    bulletData.colour = bulletData.gradient.Evaluate(ii / (BulletCount - 1f));

                    var bullet = SpawnProjectile(0, z, pos);
                    bullet.StartCoroutine(bullet.LerpSpeed(s, 0, 2f, delay: 0.5f));
                    bullets.Add(bullet);
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(2f);

            for (int i = 0; i < BulletCount; i++)
            {
                for (int ii = 0; ii < WaveCount; ii++)
                {
                    int b = ii * BulletCount + i;

                    bullets[b].Destroy();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(10f);
        }

    }
}