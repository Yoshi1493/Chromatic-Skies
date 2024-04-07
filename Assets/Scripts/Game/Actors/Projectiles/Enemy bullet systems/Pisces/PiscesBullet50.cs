using System.Collections;
using UnityEngine;

public class PiscesBullet50 : ScriptableEnemyBullet<PiscesBulletSystem51, EnemyBullet>
{
    [Space]
    [SerializeField] ProjectileObject bulletData;

    const int BulletCount = 5;
    const float BulletSpacing = 360f / BulletCount;

    protected override float MaxLifetime => 1f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 0f, MaxLifetime);
    }

    public override void Destroy()
    {
        float t = transform.eulerAngles.z + ownerShip.transform.eulerAngles.z;

        for (int i = 0; i < BulletCount; i++)
        {
            float r = (i - ((BulletCount - 1) / 2f)) * BulletSpacing;
            float z = r + t;
            Vector3 pos = transform.position;

            bulletData.colour = bulletData.gradient.Evaluate(i / (BulletCount - 1f));
            var bullet = SpawnBullet(1, z, pos, false);
            bullet.Fire();
        }

        base.Destroy();
    }
}