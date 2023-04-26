using System.Collections;
using UnityEngine;

public class LibraBullet50 : ScriptableEnemyBullet<LibraBulletSystem5, EnemyBullet>
{
    const int BulletCount = 2;
    const float BulletSpacing = 360f / BulletCount;

    [SerializeField] ProjectileObject bulletData;

    protected override float MaxLifetime => 2f;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        yield return this.LerpSpeed(5f, endSpeed, 0.5f);
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
            bullet.MoveSpeed = MoveSpeed;
            bullet.Fire();
        }

        base.Destroy();
    }
}