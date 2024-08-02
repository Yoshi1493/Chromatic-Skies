using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class LibraBullet41 : ScriptableEnemyBullet<LibraBulletSystem4, EnemyBullet>
{
    const int WaveCount = 30;
    const float WaveSpacing = 0.4f;
    const int BulletCount = 2;
    const float BulletSpacing = 15f;
    const float ShootingCooldown = 0.05f;

    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 0f, 1f);
        yield return this.RotateBy(180f, 0f);
        yield return this.LerpSpeed(0f, 10f, 2f);
        yield return WaitUntil(() => transform.position.y < (-5f - spriteRenderer.size.y));
        MoveSpeed = 0f;

        float r = RandomAngleDeg;

        for (int i = 0; i < WaveCount; i++)
        {
            Vector3 pos = (i * WaveSpacing * -transform.up) + transform.position;

            for (int ii = 0; ii < BulletCount; ii++)
            {
                int d = ii % 2 * 2 - 1;
                float z = (d * (i * BulletSpacing)) + r;

                SpawnBullet(2, z, pos, false).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}