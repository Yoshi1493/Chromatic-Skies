using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class ScorpioBullet60 : ScriptableEnemyBullet<ScorpioBulletSystem6, EnemyBullet>
{
    const int BulletCount = 6;
    const float ShootingCooldown = 0.1f;

    List<EnemyBullet> bullets = new(BulletCount);

    protected override float MaxLifetime => Mathf.Infinity;

    protected override IEnumerator Move()
    {
        Vector3 v = transform.position - (3f * transform.up.RotateVectorBy(PositiveOrNegativeOne * Random.Range(60f, 80f)));
        yield return this.MoveTo(v, 1f);

        while (enabled)
        {
            bullets.Clear();

            yield return WaitForSeconds(1f);

            for (int i = 0; i < BulletCount; i++)
            {
                float z = playerShip.transform.position.GetRotationDifference(transform.position);
                Vector3 pos = transform.position;

                var bullet = SpawnBullet(2, z, pos, false);
                bullets.Add(bullet);
                bullet.Fire();

                yield return WaitForSeconds(ShootingCooldown);
            }

            yield return WaitForSeconds(2f);

            SpawnBullet(0, 0f, Vector3.zero).Fire();
            yield return WaitForSeconds(2f);

            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].isActiveAndEnabled)
                {
                    bullets[i].GetComponent<ITimestoppable>().Resume();
                }
            }

            bullets.Clear();
        }
    }
}