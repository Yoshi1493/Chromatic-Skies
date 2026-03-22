using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusShooter010 : CommonEnemyShooter<EnemyBullet>
{
    const int BulletCount = 50;
    const float BulletSpawnRadius = 2f;
    const float BulletMinSpeed = 2f;
    const float BulletMaxSpeed = 4f;

    protected override float ShootingCooldown => 0.05f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        for (int i = 0; i < BulletCount; i++)
        {
            float z = 0f;
            float t = Random.value;
            float s = Mathf.Lerp(BulletMinSpeed, BulletMaxSpeed, t);
            Vector3 pos = BulletSpawnRadius * Random.insideUnitCircle;

            bulletData.colour = bulletData.gradient.Evaluate(t);

            var bullet = SpawnProjectile(7, z, pos);
            bullet.StartCoroutine(bullet.LerpSpeed(0f, s, s * 0.5f));

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}