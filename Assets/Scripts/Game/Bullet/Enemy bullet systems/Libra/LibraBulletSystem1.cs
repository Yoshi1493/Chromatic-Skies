using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class LibraBulletSystem1 : EnemyShooter<EnemyBullet>
{
    const float RotationSpeed = 4f;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);
        SetSubsystemEnabled(2);

        yield return WaitForSeconds(1f);

        float randDirection = Mathf.Sign(Random.value - 0.5f);

        while (enabled)
        {
            transform.Rotate(0f, 0f, RotationSpeed * randDirection * Time.deltaTime);
            yield return EndOfFrame;
        }
    }
}