using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBulletSystem2 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 40;
    const int BulletMinCount = 8;
    const int BulletMaxCount = 13;
    const float BulletBaseSpeed = 2f;
    const float BulletMinSpeed = 3f;
    const float BulletMaxSpeed = 5f;

    List<ITimestoppable> bullets = new(WaveCount * BulletMaxCount);

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            bullets.Clear();

            for (int i = 0; i < WaveCount; i++)
            {
                int bulletCount = Random.Range(BulletMinCount, BulletMaxCount);

                for (int ii = 0; ii < bulletCount; ii++)
                {
                    float z = MathHelper.RandomAngleDeg;
                    float r = Random.value;
                    float s = Mathf.Lerp(BulletMinSpeed, BulletMaxSpeed, r);
                    Vector3 pos = Vector3.zero;

                    bulletData.colour = bulletData.gradient.Evaluate(r);

                    var bullet = SpawnProjectile(0, z, pos) as ScorpioBullet20;
                    bullet.StartCoroutine(bullet.LerpSpeed(BulletBaseSpeed, s, 1f));
                    bullet.Fire();
                    bullets.Add(bullet);
                }

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(0.5f);
            bullets.ForEach(b => b.Stop());

            yield return WaitForSeconds(3f);
            bullets.ForEach(b => b.Resume());

            yield break;
        }
    }
}