using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBullet10 : ScriptableBossBullet<ScorpioBulletSystem1, BossBullet>
{
    [SerializeField] ProjectileObject bulletData;

    const int WaveCount = 10;
    const int BulletCount = 4;
    const float BulletSpacing = 360f / BulletCount;
    const float ShootingCooldown = ScorpioBulletSystem1.BulletRotationDuration / WaveCount;

    protected override float MaxLifetime => ScorpioBulletSystem1.BulletRotationDuration + 0.5f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 3f;

        for (int i = 0; i < WaveCount; i++)
        {
            yield return WaitForSeconds(ShootingCooldown);

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = (ii * BulletSpacing) + transform.eulerAngles.z;
                Vector3 pos = transform.position;

                bulletData.colour = SpriteRenderer.color;

                SpawnBullet(1, z, pos, false).Fire();
            }
        }
    }

    protected override void Update()
    {
        base.Update();
        SpriteRenderer.color = projectileData.gradient.Evaluate(currentLifetime / MaxLifetime);
    }
}