using System.Collections;
using UnityEngine;

public class CancerBullet10 : ScriptableEnemyBullet<CancerBulletSystem1, EnemyBullet>
{
    const int WaveCount = 3;
    const int BulletCount = 3;
    const float BulletSpacing = 360f / BulletCount;
    const float BulletBaseSpeed = 3f;
    const float BulletSpeedMultiplier = 0.3f;

    [Space]
    [SerializeField] ProjectileObject bulletData;

    protected override IEnumerator Move()
    {
        yield return null;
        Destroy();
    }

    public override void Destroy()
    {
        float r = ownerShip.transform.position.GetRotationDifference(transform.position);

        for (int i = 0; i < WaveCount; i++)
        {
            bulletData.colour = bulletData.gradient.Evaluate(i / (WaveCount - 1f));

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = ii * BulletSpacing + r;
                float s = BulletBaseSpeed + (i * BulletSpeedMultiplier);
                Vector3 pos = transform.position;

                var bullet = SpawnBullet(1, z, pos, false);
                bullet.MoveSpeed = s;
                bullet.Fire();
            }
        }

        base.Destroy();
    }
}