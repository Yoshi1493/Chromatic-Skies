using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBullet10 : ScriptableEnemyBullet<LibraBulletSystem1, EnemyBullet>
{
    [SerializeField] int bulletID;

    const float WaveSpacing = 8f;
    const int BranchCount = 4;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletBaseSpeed = 2f;
    const float BulletSpeedModifier = 0.5f;
    const float ShootingCooldown = 0.2f;

    protected override float MaxLifetime => Mathf.Infinity;

    protected override IEnumerator Move()
    {
        StartCoroutine(this.RotateBy(90f, 1f));
        yield return this.LerpSpeed(3f, 0f, 1f);

        StartCoroutine(Shoot());

        transform.parent = ownerShip.transform;
        StartCoroutine(this.RotateAround(ownerShip, MaxLifetime, 90f));
    }

    IEnumerator Shoot()
    {
        for (int i = 0; enabled; i++)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = -(i * WaveSpacing) - (ii * BranchSpacing);
                float s = BulletBaseSpeed + ((BranchCount - ii) * BulletSpeedModifier);
                Vector3 pos = transform.position;

                var bullet = SpawnBullet(bulletID, z, pos, false);
                bullet.MoveSpeed = s;
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}