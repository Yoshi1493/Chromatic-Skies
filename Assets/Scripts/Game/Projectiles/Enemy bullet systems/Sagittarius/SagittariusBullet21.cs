using System.Collections;
using UnityEngine;

public class SagittariusBullet21 : ScriptableEnemyBullet<SagittariusBulletSystem2, EnemyBullet>
{
    const int BulletCount = 4;
    const float BulletSpacing = 360f / BulletCount;
    float bulletBaseSpeed;

    [SerializeField] ProjectileObject bulletData;

    protected override float MaxLifetime => 1.5f;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        bulletBaseSpeed = endSpeed;
        yield return this.LerpSpeed(MoveSpeed, 0f, 1f);
    }

    public override void Destroy()
    {
        float z = transform.eulerAngles.z + (BulletSpacing * 0.5f);

        for (int i = 0; i < BulletCount; i++)
        {
            z += i * BulletSpacing;
            float s = bulletBaseSpeed;
            Vector3 pos = transform.position;

            var bullet = SpawnBullet(2, z, pos, false);
            bullet.MoveSpeed = s;
        }

        base.Destroy();
    }
}