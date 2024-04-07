using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SagittariusBulletSystem6 : EnemyShooter<EnemyBullet>
{
    FlashlightEffect flashlightEffect;

    const int BulletCount = 0;

    protected override void Awake()
    {
        base.Awake();
        flashlightEffect = FindObjectOfType<FlashlightEffect>();
    }
     
    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        flashlightEffect.enabled = true;
        flashlightEffect.SetStengthOverTime(4f, 10f);
        flashlightEffect.SetRadiusOverTime(0.4f, 10f);
        flashlightEffect.SetHardnessOverTime(1.01f, 10f);

        SpawnProjectile(0, 0f, Vector3.zero).Fire();
    }
}