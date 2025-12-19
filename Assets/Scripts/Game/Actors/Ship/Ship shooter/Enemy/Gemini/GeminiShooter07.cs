using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class GeminiShooter07 : CommonEnemyShooter<EnemyBullet>
{
    const int WaveCount = 7;
    const int BranchCount = 2;
    const float BranchSpacing = 120f;

    List<EnemyBullet> bullets = new((int)Mathf.Pow(BranchCount, WaveCount - 1));

    [SerializeField] GeminiShooter06 parentShooter;

    protected override float ShootingCooldown => 0.5f;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(1.5f);

        while (enabled)
        {
            bullets.Clear();
            yield return WaitForSeconds(4f);

            float z = 0f;
            Vector3 pos = transform.position;

            var bullet = SpawnProjectile(7, z, pos, false);
            bullet.StartCoroutine(bullet.LerpSpeed(5f, 0f, 0.5f));
            bullets.Add(bullet);

            for (int i = 0; i < WaveCount; i++)
            {
                yield return WaitForSeconds(ShootingCooldown);

                int bulletCount = bullets.Count;

                for (int ii = 0; ii < bulletCount; ii++)
                {
                    pos = bullets[ii].transform.position;
                    bullets[ii].StartCoroutine(bullets[ii].RotateBy(-0.5f * BranchSpacing, 0f));
                    bullets[ii].StartCoroutine(bullets[ii].LerpSpeed(5f, 0f, ShootingCooldown));

                    for (int iii = 0; iii < BranchCount - 1; iii++)
                    {
                        z = bullets[ii].transform.eulerAngles.z + (iii - 0.5f * BranchSpacing) + 180f;

                        bullet = SpawnProjectile(7, z, pos, false);
                        if (i < WaveCount - 1)
                        {
                            bullet.StartCoroutine(bullet.LerpSpeed(5f, 0f, ShootingCooldown));
                        }
                        bullets.Add(bullet);
                    }
                }
            }

            bullets.ForEach(b => b.Fire());

            yield return WaitForSeconds(6f);

            bullets.ForEach(b => b.ReturnToObjectPool());
            yield return null;
        }
    }
}