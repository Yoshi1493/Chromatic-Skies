using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LeoBulletSystem4 : EnemyShooter<EnemyBullet>
{
    const int BranchCount = 7;
    const float BranchSpacing = 18f;
    const int BulletCount = 6;
    const float BulletBaseSpeed = 1.3f;
    const float BulletSpeedModifier = 0.2f;

    List<EnemyBullet> bullets = new(BulletCount);
    [HideInInspector] public List<Vector3> bulletSpawnPositions = new(BulletCount);

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            bullets.Clear();
            bulletSpawnPositions.Clear();

            StartMoveAction?.Invoke();

            //get random positions
            for (int i = 0; i < BulletCount; i++)
            {
                float x = Mathf.Lerp(-screenHalfWidth, screenHalfWidth, i / (BulletCount - 1f)) + Random.Range(-1f, 1f);
                float y = Random.Range(1f, screenHalfHeight);

                Vector3 v = 0.75f * new Vector3(x, y);
                bulletSpawnPositions.Add(v);
            }

            bulletSpawnPositions.Randomize();

            //spawn clones
            for (int i = 0; i < BulletCount; i++)
            {
                float z = 0f;
                Vector3 pos = Vector3.zero;

                var bullet = SpawnProjectile(0, z, pos);
                bullet.StartCoroutine(bullet.MoveTo(bulletSpawnPositions[i], 1f));
                bullet.Fire();

                yield return WaitForSeconds(ShootingCooldown);
            }

            bulletSpawnPositions.Sort((a, b) => a.x.CompareTo(b.x));

            yield return WaitForSeconds(4.5f);

            //fire bullets from clones
            for (int i = 0; i < BulletCount; i++)
            {
                Vector3 pos = bulletSpawnPositions[i];
                float r = PlayerPosition.GetRotationDifference(pos);

                for (int ii = 0; ii < BranchCount; ii++)
                {
                    for (int iii = 0; iii < BulletCount; iii++)
                    {
                        float z = ((ii - ((BranchCount - 1) / 2f)) * BranchSpacing) + r;
                        float s = BulletBaseSpeed + (iii * BulletSpeedModifier);

                        bulletData.colour = bulletData.gradient.Evaluate(iii / (BulletCount - 1f));

                        var bullet = SpawnProjectile(2, z, pos, false);
                        bullet.MoveSpeed = s;
                        bullet.Fire();
                    }
                }

                yield return WaitForSeconds(0.5f);
            }

            yield return WaitForSeconds(5f);
        }
    }
}