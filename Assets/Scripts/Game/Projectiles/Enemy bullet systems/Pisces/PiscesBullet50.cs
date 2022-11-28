using System.Collections;
using UnityEngine;

public class PiscesBullet50 : ScriptableEnemyBullet<PiscesBulletSystem5, EnemyBullet>
{
    [Space]
    [SerializeField] ProjectileObject bulletData;

    const int BulletCount = 5;
    const float BulletSpacing = 15f;

    protected override float MaxLifetime => 1f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(4f, 1f, 0.5f);
    }

    public override void Destroy()
    {
        float t = transform.eulerAngles.z + ownerShip.transform.eulerAngles.z;

        for (int i = 0; i < BulletCount; i++)
        {
            float z = (i - ((BulletCount - 1) * 0.5f)) * BulletSpacing + t;
            Vector3 pos = transform.position;

            bulletData.colour = bulletData.gradient.Evaluate((float)i / BulletCount);
            SpawnBullet(1, z, pos, false).Fire();
        }

        base.Destroy();
    }
}