using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static MathHelper;

public class TaurusBulletSystem53 : EnemyShooter<EnemyBullet>
{
    const int WaveCount = 55;

    [Space]
    [SerializeField] LayerMask collisionBoundary;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2.5f);

        Laser[] lasers = FindObjectsOfType<TaurusLaser51>();

        for (int i = 0; i < WaveCount; i++)
        {
            for (int ii = 0; ii < lasers.Length; ii++)
            {
                Vector3 rayOrigin = transform.position;
                Vector3 rayDirection = -transform.up.RotateVectorBy(lasers[ii].transform.eulerAngles.z);
                float rayDistance = Mathf.Infinity;

                var hit = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance, collisionBoundary);

                if (hit)
                {
                    Vector3 pos = hit.point;
                    float r = Random.Range(2f, 15f);
                    float z = PlayerPosition.GetRotationDifference(pos) + (r * PositiveOrNegativeOne);

                    SpawnProjectile(0, z, pos, false).Fire();
                }

            }

            yield return WaitForSeconds(ShootingCooldown);
        }

        enabled = false;
    }
}