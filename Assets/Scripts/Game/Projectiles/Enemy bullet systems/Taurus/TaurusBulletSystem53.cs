using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBulletSystem53 : EnemyShooter<EnemyBullet>
{
    [Space]
    [SerializeField] LayerMask collisionBoundary;

    protected override IEnumerator Shoot()
    {
        yield return WaitForSeconds(2.5f);

        Laser[] lasers = FindObjectsOfType<TaurusLaser51>();

        while (enabled)
        {
            for (int i = 0; i < lasers.Length; i++)
            {
                Vector3 rayOrigin = transform.position;
                Vector3 rayDirection = -transform.up.RotateVectorBy(lasers[i].transform.eulerAngles.z);
                float rayDistance = Mathf.Infinity;

                var hit = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance, collisionBoundary);

                if (hit)
                {
                    Vector3 pos = hit.point;
                    float r = Random.Range(-15f, 15f);
                    float z = PlayerPosition.GetRotationDifference(pos) + r;

                    SpawnProjectile(0, z, pos, false).Fire();
                }

            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}