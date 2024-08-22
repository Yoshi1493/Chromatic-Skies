using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBulletSystem6 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 12;
    const float WaveSpacing = 15f;
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletSpawnRadius = 0.6f;

    EnemyBullet specialBullet;
    List<EnemyBullet> bullets = new(WaveCount * BranchCount);

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
            for (int i = 0; i < WaveCount; i++)
            {
                float r = PlayerPosition.GetRotationDifference(specialBullet.transform.position);

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    float z = (i * WaveSpacing) + (ii * BranchSpacing) + r;
                    Vector3 pos = specialBullet.transform.position + (BulletSpawnRadius * specialBullet.transform.up.RotateVectorBy(z));

                    bulletData.colour = bulletData.gradient.Evaluate((i * BranchCount + ii) / (WaveCount * BranchCount - 1f));

                    var bullet = SpawnProjectile(2, z, pos, false);
                    bullets.Add(bullet);
                    bullet.Fire();
                }

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

            yield return WaitForSeconds(8f);
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