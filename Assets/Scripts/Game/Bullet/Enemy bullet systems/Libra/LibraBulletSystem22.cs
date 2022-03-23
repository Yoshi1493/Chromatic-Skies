using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem22 : EnemyBulletSubsystem<EnemyBullet>
{
    const int Spacing = 16;
    float screenHalfWidth;
    protected override float ShootingCooldown => 0.2f;

    protected override void Awake()
    {
        base.Awake();

        Camera mainCam = Camera.main;
        screenHalfWidth = mainCam.orthographicSize * mainCam.aspect;
    }

    protected override IEnumerator Shoot()
    {
        while (enabled)
        {
            float zRot = transform.eulerAngles.z;

            for (int i = 0; i < 3; i++)
            {
                float z = zRot + 180f;
                Vector3 pos = Random.Range(-screenHalfWidth, screenHalfWidth) * Vector3.right;

                SpawnProjectile(0, z, pos.RotateVectorBy(zRot)).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}