using System.Collections;
using UnityEngine;

public class AquariusBullet60 : ScriptableEnemyBullet<AquariusBulletSystem6, EnemyBullet>
{
    [SerializeField] ProjectileObject bulletData;

    const int BulletCount = 8;
    const float BulletSpacing = 4f;
    const float BulletBaseSpeed = 3f;
    const float BulletSpeedMultiplier = 0.1f;

    protected override float MaxLifetime => 1.5f;

    protected override IEnumerator Move()
    {
        float startSpeed = MoveSpeed;
        yield return this.LerpSpeed(startSpeed, 0f, 1f);
    }

    public override void Destroy()
    {
        float r = transform.eulerAngles.z + 180f;

        for (int i = 0; i < BulletCount; i++)
        {
            float z = ((i - ((BulletCount - 1) / 2f)) * BulletSpacing) + r;
            float s = BulletBaseSpeed + (i * BulletSpeedMultiplier);
            Vector3 pos = transform.position;

            bulletData.colour = bulletData.gradient.Evaluate(i / (BulletCount - 1f));

            var bullet = SpawnBullet(1, z, pos, false);
            bullet.MoveSpeed = s;
            bullet.Fire();
        }

        base.Destroy();
    }
}