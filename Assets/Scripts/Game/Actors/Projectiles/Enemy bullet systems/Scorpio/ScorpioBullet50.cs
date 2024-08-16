using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBullet50 : ScriptableEnemyBullet<ScorpioBulletSystem5, EnemyBullet>
{
    [SerializeField] ProjectileObject bulletData;

    const int WaveCount = 10;
    const int BulletCount = 4;
    const float BulletSpacing = 360f / BulletCount;
    const float ShootingCooldown = 0.5f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 2f;

        for (int i = 0; i < WaveCount; i++)
        {
            yield return WaitForSeconds(ShootingCooldown);

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = ((ii + 0.5f) * BulletSpacing) + transform.eulerAngles.z;
                Vector3 pos = transform.position;

                bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

                SpawnBullet(1, z, pos, false).Fire();
            }
        }
    }
}