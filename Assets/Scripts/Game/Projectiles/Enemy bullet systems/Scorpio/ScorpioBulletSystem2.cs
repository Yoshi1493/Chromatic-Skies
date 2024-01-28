using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class ScorpioBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 40;
    const int BulletMinCount = 6;
    const int BulletMaxCount = 11;
    const float BulletBaseSpeed = 1f;
    const float BulletMinSpeed = 2f;
    const float BulletMaxSpeed = 4f;
    const float BulletSpawnRadius = 0.8f;

    List<EnemyBullet> bullets = new(WaveCount * (BulletMaxCount - 1));

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            bullets.Clear();

            StartMoveAction?.Invoke();

            for (int i = 0; i < WaveCount; i++)
            {
                int bulletCount = Random.Range(BulletMinCount, BulletMaxCount);

                for (int ii = 0; ii < bulletCount; ii++)
                {
                    float z = RandomAngleDeg;
                    float r = Random.value;
                    float s = Mathf.Lerp(BulletMinSpeed, BulletMaxSpeed, r);
                    Vector3 pos = BulletSpawnRadius * transform.up.RotateVectorBy(z);

                    bulletData.colour = bulletData.gradient.Evaluate(r);

                    var bullet = SpawnProjectile(1, z, pos);
                    bullet.StartCoroutine(bullet.LerpSpeed(BulletBaseSpeed, s, 1f));
                    bullet.Fire();
                    bullets.Add(bullet);
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(1f);

            SpawnProjectile(0, 0f, Vector3.zero).Fire();
            yield return WaitForSeconds(1.5f);

            SetSubsystemEnabled(1);

            yield return WaitForSeconds(5f);

            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].isActiveAndEnabled)
                {
                    if (bullets[i].transform.position.y > -screenHalfHeight)
                    {
                        bullets[i].GetComponent<ITimestoppable>().Resume();
                    }
                    else
                    {
                        bullets[i].Destroy();
                    }
                }
            }

            yield return WaitForSeconds(3f);
        }
    }
}