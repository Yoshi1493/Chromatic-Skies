using System.Collections;
using UnityEngine;

public class LeoBullet22 : ScriptableEnemyBullet<LeoBulletSystem21, EnemyBullet>
{
    [SerializeField] ProjectileObject bulletData;
    
    const int BulletCount = 8;
    const float BulletSpacing = 12f;
    const float BulletBaseSpeed = 1.5f;
    const float BulletSpeedModifier = 0.15f;

    protected override float MaxLifetime => 3f;

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
            bullet.MoveSpeed = BulletBaseSpeed + (i * BulletSpeedModifier);
            bullet.Fire();
        }

        base.Destroy();
    }
}