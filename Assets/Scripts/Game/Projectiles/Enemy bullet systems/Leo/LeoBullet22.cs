using System.Collections;
using UnityEngine;

public class LeoBullet22 : ScriptableEnemyBullet<LeoBulletSystem21, EnemyBullet>
{
    [SerializeField] ProjectileObject bulletData;
    
    const int BulletCount = 8;
    const float BulletSpacing = 12f;

    protected override float MaxLifetime => 2.5f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 0f, 1f);
    }

    public override void Destroy()
    {
        float r = transform.eulerAngles.z;

        for (int i = 0; i < BulletCount; i++)
        {
            float z = (i * BulletSpacing) + r;
            Vector3 pos = transform.position;
            bulletData.colour = bulletData.gradient.Evaluate(i / (BulletCount - 1f));

            var bullet = SpawnBullet(3, z, pos, false);
            bullet.MoveSpeed = i * 0.25f + 1.5f;
            bullet.Fire();
        }

        base.Destroy();
    }
}