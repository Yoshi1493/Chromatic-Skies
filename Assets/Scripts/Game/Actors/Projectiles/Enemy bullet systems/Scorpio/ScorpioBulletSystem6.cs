using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBulletSystem6 : EnemyShooter<EnemyBullet>
{
    const int RingCount = 3;
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const int BulletCount = 15;
    const float BulletSpacing = 5f;
    const float BulletSpawnRadius = 0.6f;
    const float BulletBaseSpeed = 1f;
    const float BulletSpeedModifier = 0.5f;

    EnemyBullet specialBullet;
    List<EnemyBullet> bullets = new(RingCount * BranchCount * BulletCount);

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
            float r = PlayerPosition.GetRotationDifference(specialBullet.transform.position);

            for (int i = 0; i < RingCount; i++)
            {
                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = (ii * BranchSpacing) + r;
                        float s = BulletBaseSpeed + (i * BulletSpeedModifier);
                        float t = ((iii - ((BulletCount - 1) / 2f)) * BulletSpacing);
                        Vector3 pos = specialBullet.transform.position + (BulletSpawnRadius * specialBullet.transform.up.RotateVectorBy(z));

                        bulletData.colour = bulletData.gradient.Evaluate(Mathf.InverseLerp(0f, 360f, z));

                        var bullet = SpawnProjectile(2, z, pos, false);
                        bullet.Fire();
                        bullet.StartCoroutine(bullet.LerpSpeed(0.5f, s, 1f, delay: 1f));
                        bullet.StartCoroutine(bullet.RotateBy(t, 1f, delay: 1f));
                        bullets.Add(bullet);
                    }
                }
            }

            yield return WaitForSeconds(4f);

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
            yield return WaitForSeconds(1f);
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