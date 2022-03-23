using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem12 : EnemyBulletSubsystem<EnemyBullet>
{
    const int Spacing = 20;
    float screenHalfWidth;
    protected override float ShootingCooldown => 0.5f;

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

            for (int i = 0; i <= Spacing; i++)
            {
                float z = 5 * (i - Spacing * 0.5f) + zRot;
                Vector3 pos = Mathf.Lerp(-screenHalfWidth, screenHalfWidth, i / (float)Spacing) * Vector3.right;

                SpawnProjectile(1, z, pos.RotateVectorBy(zRot)).Fire();
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}