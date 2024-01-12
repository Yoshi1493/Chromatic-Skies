using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class SagittariusBulletSystem5 : EnemyShooter<EnemyBullet>
{
    [SerializeField] ProjectileObject smallBulletData;

    const int WaveCount = 18;
    const int BigBulletCount = 12;
    const float BulletBaseSpeed = 0.5f;
    const float BulletSpeedModifier = 0.75f;
    const int SmallBulletCount = 3;
    const float SmallBulletSpacing = 30f;
    const float BulletRotationSpeed = 15f;
    const float BulletRotationDuration = 3f;

    List<EnemyBullet> bullets = new(WaveCount * BigBulletCount);

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

            for (int i = 0; i < WaveCount; i++)
            {
                for (int ii = 0; ii < BigBulletCount; ii++)
                {
                    float z = d.RotateVectorBy(90f).GetRotationDifference(Vector3.zero);
                    float s = BulletBaseSpeed + (ii * BulletSpeedModifier);
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(ii / (BigBulletCount - 1f));

                    var bullet = SpawnProjectile(0, z, pos);
                    bullet.StartCoroutine(bullet.LerpSpeed(s, 0, 2f, delay: 0.5f));
                    bullets.Add(bullet);
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(2f);

            for (int i = 0; i < BigBulletCount; i++)
            {
                for (int ii = 0; ii < WaveCount; ii++)
                {
                    int b = ii * BigBulletCount + i;

                    for (int iii = 0; iii < SmallBulletCount; iii++)
                    {
                        float z = ((i + ii + iii) * SmallBulletSpacing) + bullets[b].transform.eulerAngles.z;
                        Vector3 pos = bullets[b].transform.position;

                        smallBulletData.colour = smallBulletData.gradient.Evaluate(iii / (SmallBulletCount - 1f));

                        var bullet = SpawnProjectile(1, z, pos, false);
                        bullet.StartCoroutine(bullet.RotateBy(iii * BulletRotationSpeed, BulletRotationDuration));
                        bullet.Fire();
                    }

                    bullets[b].Destroy();
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            SetSubsystemEnabled(1);
            yield return WaitForSeconds(10f);
        }

    }
}