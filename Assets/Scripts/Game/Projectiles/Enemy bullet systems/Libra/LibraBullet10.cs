using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBullet10 : ScriptableEnemyBullet<LibraBulletSystem1, EnemyBullet>
{
    [SerializeField] int bulletID;
    [SerializeField] float rotationSpeed;

    const float WaveSpacing = 8f;
    const int BranchCount = 4;
    const float BranchSpacing = 360f / BranchCount;
    const float BulletSpeedMultiplier = 0.5f;
    const float ShootingCooldown = 0.2f;

    protected override float MaxLifetime => Mathf.Infinity;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(3f, 0f, 1f);
        yield return this.RotateBy(90f, 0f);

        StartCoroutine(Shoot());
        transform.parent = ownerShip.transform;
        yield return this.RotateAround(ownerShip, MaxLifetime, rotationSpeed);
    }

    IEnumerator Shoot()
    {
        int i = 0;

        while (enabled)
        {
            for (int ii = 0; ii < BranchCount; ii++)
            {
                float z = (i * WaveSpacing) + (ii * BranchSpacing);
                float s = (BranchCount - ii) * BulletSpeedMultiplier + 2f;
                Vector3 pos = transform.position;

                var bullet = SpawnBullet(bulletID, z, pos, false);
                bullet.MoveSpeed = s;
                bullet.Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
            i++;
        }
    }
}