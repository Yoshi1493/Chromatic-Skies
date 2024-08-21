using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoBullet61 : ScriptableEnemyBullet<VirgoBulletSystem61, EnemyBullet>
{
    [SerializeField] ProjectileObject bulletData;

    const int WaveCount = 12;
    const int BranchCount = 6;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletBaseSpeed = 3f;
    const float BulletSpeedModifier = 0.4f;
    const float ShootingCooldown = 0.1f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(2f, 0f, 1f);

        StartCoroutine(Shoot());
        yield return this.HomeInOn(playerShip, 2.5f, 1f);

        yield return WaitForSeconds(1f);

        StartCoroutine(this.LerpSpeed(0f, 10f, 5f));
        yield return this.GraduallyLookAt(playerShip.transform.position, 1f);
    }

    IEnumerator Shoot()
    {
        yield return WaitForSeconds(1f);

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = (transform.eulerAngles.z + 180f) + (ii * BranchSpacing);
                float s = BulletBaseSpeed + (i * BulletSpeedModifier);
                Vector3 pos = transform.position;

                bulletData.colour = SpriteRenderer.color;
                var bullet = SpawnBullet(4, z, pos, false);
                bullet.MoveSpeed = s;
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}