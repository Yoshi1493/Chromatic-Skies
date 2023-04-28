using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBullet50 : ScriptableEnemyBullet<LibraBulletSystem5, EnemyBullet>
{
    const int BulletCount = 2;
    const float BulletSpacing = 360f / BulletCount;
    float bulletBaseSpeed;

    [SerializeField] ProjectileObject bulletData;

    protected override float MaxLifetime => 2f;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(5f, endSpeed, 0.5f);
        yield return WaitForSeconds(1f);

        bulletBaseSpeed = MoveSpeed;
        yield return this.LerpSpeed(MoveSpeed, 0f, 0.5f);
    }

    public override void Destroy()
    {
        float r = Random.Range(0f, BulletSpacing);

        for (int i = 0; i < BulletCount; i++)
        {
            float z = (i * BulletSpacing) + r;
            Vector3 pos = transform.position;

            bulletData.colour = spriteRenderer.color;

            var bullet = SpawnBullet(1, z, pos, false);
            bullet.MoveSpeed = bulletBaseSpeed;
            bullet.Fire();
        }

        base.Destroy();
    }
}