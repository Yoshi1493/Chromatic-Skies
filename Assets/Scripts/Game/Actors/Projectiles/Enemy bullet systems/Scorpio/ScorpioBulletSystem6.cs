using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBulletSystem6 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 6;
    const float BulletSpawnRadius = 0.5f;

    EnemyBullet specialBullet;
    List<EnemyBullet> bullets = new(BulletCount);

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        bullets.Clear();
        SetSubsystemEnabled(1);
        yield return WaitForSeconds(3f);

        specialBullet = SpawnProjectile(1, 0f, Vector3.zero);
        specialBullet.Fire();

        yield return WaitForSeconds(1f);

        while (enabled)
        {
            for (int i = 0; i < BulletCount; i++)
            {
                float z = PlayerPosition.GetRotationDifference(specialBullet.transform.position);
                Vector3 pos = specialBullet.transform.position + (BulletSpawnRadius * specialBullet.transform.up.RotateVectorBy(z));

                var bullet = SpawnProjectile(2, z, pos, false);
                bullets.Add(bullet);
                bullet.Fire();

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(2f);

            SpawnProjectile(0, 0f, Vector3.zero).Fire();
            yield return WaitForSeconds(1f);

            specialBullet.StartCoroutine(specialBullet.MoveTo(GetRandomPosition(), 1f));
            yield return WaitForSeconds(1f);

            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].isActiveAndEnabled)
                {
                    bullets[i].GetComponent<ITimestoppable>().Resume();
                    bullets[i].moveDirection = -bullets[i].LookAt(specialBullet).normalized;
                }
            }

            bullets.Clear();

            yield return WaitForSeconds(5f);
        }
    }

    Vector3 GetRandomPosition()
    {
        float x, y;

        do
        {
            x = 0.6f * Random.Range(-screenHalfWidth, screenHalfWidth);
            y = Random.Range(0f, screenHalfHeight);
        }
        while (Mathf.Abs(specialBullet.transform.position.x - x) > 1f && Mathf.Abs(specialBullet.transform.position.y - y) > 1);

        return new(x, y);
    }
}