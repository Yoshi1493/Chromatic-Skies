using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBullet41 : ScriptableEnemyBullet<LibraBulletSystem4, EnemyBullet>
{
    const int WaveCount = 40;
    const float WaveSpacing = 0.3f;
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

        for (int i = 0; i < WaveCount; i++)
        {
            Vector3 pos = (i * WaveSpacing * -transform.up) + transform.position;
            //print($"{i} * {WaveSpacing} * {-(Vector2)transform.up} + {(Vector2)transform.position:F1} = {(Vector2)pos:F3}");
            for (int ii = 0; ii < BulletCount; ii++)
            {
                int d = ii % 2 * 2 - 1;
                float z = (d * (i * BulletSpacing)) + transform.eulerAngles.z;

                SpawnBullet(2, z, pos, false).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}