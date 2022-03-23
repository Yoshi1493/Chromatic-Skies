using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem21 : EnemyBulletSubsystem<Laser>
{
    float screenHalfWidth;
    protected override float ShootingCooldown => 0.4f;

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
                Vector3 pos = Random.Range(-screenHalfWidth, screenHalfWidth) * Vector3.right;
                SpawnProjectile(0, zRot, pos.RotateVectorBy(zRot));
            }

            yield return WaitForSeconds(ShootingCooldown);
        }
    }
}