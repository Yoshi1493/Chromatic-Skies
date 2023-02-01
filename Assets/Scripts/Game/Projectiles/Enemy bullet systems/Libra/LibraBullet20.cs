using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBullet20 : ScriptableEnemyBullet<LibraBulletSystem2, EnemyBullet>
{
    const int BulletCount = 2;
    const float MaxInnerAngle = 120f;
    const float InnerShootingCooldown = 0.4f;
    const float OuterShootingCooldown = 0.2f;

    protected override float MaxLifetime => Mathf.Infinity;

    protected override IEnumerator Move()
    {
        StartCoroutine(this.RotateBy(120f, 1f));
        yield return this.LerpSpeed(5f, 0f, 1f);

        StartCoroutine(ShootInner());
        transform.parent = ownerShip.transform;
        StartCoroutine(this.RotateAround(ownerShip, MaxLifetime, 120f));

        yield return WaitForSeconds(2f);
        StartCoroutine(ShootOuter());
    }

    IEnumerator ShootInner()
    {
        int i = 0;
        float t = MaxInnerAngle / (BulletCount - 1);

        while (enabled)
        {
            float r = transform.eulerAngles.z;

            for (int ii = 0; ii < BulletCount; ii++)
            {
                float z = (MaxInnerAngle * -0.5f) + (ii * t) + r;
                Vector3 pos = transform.position;

                SpawnBullet(1, z, pos, false).Fire();
            }

            yield return WaitForSeconds(InnerShootingCooldown);
            i++;
        }
    }

    IEnumerator ShootOuter()
    {
        int i = 0;

        while (enabled)
        {
            float z = transform.eulerAngles.z;
            Vector3 pos = transform.position;

            SpawnBullet(2, z, pos, false).Fire();

            yield return WaitForSeconds(OuterShootingCooldown);
            i++;
        }
    }
}