using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBullet30 : ScriptableEnemyBullet<ScorpioBulletSystem3, EnemyBullet>
{
    const int WaveCount = 16;
    const int BulletCount = 4;
    const float BulletSpacing = 360f / BulletCount;
    const float ShootingCooldown = 0.4f;

    protected override float MaxLifetime => 7f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 3f;
        StartCoroutine(SpawnBullets());

        yield return this.RotateBy(45f, 0.5f);

        while (enabled)
        {
            yield return this.RotateBy(-90f, 1f);
            yield return this.RotateBy(90f, 1f);
        }
    }

    IEnumerator SpawnBullets()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            yield return WaitForSeconds(ShootingCooldown);

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = (ii * BulletSpacing) + transform.eulerAngles.z;
                Vector3 pos = transform.position;

                SpawnBullet(1, z, pos, false).Fire();
            }
        }
    }
}