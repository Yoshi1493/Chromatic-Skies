using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class ScorpioBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const int BulletCount = 8;
    const float BulletSpacing = 180f / BulletCount;
    const float BulletRotationSpeed = 90f;
    public const float BulletRotationDuration = 4f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        while (enabled)
        {
            for (int i = 0; i <= BulletCount; i++)
            {
                float d = Mathf.Sign(transform.position.x);
                float z = (i * BulletSpacing) - (90f + (d * 45f));
                Vector3 pos = Vector3.zero;

                var bullet = SpawnProjectile(0, z, pos);
                bullet.StartCoroutine(bullet.RotateBy(-d * BulletRotationSpeed, BulletRotationDuration));
                bullet.Fire();
            }

            yield return WaitForSeconds(5f);

            StartMoveAction?.Invoke();
            yield return WaitForSeconds(5f);
        }
    }
}