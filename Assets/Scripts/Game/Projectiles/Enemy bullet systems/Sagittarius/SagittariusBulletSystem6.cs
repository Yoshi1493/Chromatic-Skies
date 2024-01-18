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

        //flashlightEffect.enabled = true;
        //flashlightEffect.SetStengthOverTime(4f, 0f);
        //flashlightEffect.SetRadiusOverTime(0f, 0f);
        //flashlightEffect.SetHardnessOverTime(1f, 0f);

        //flashlightEffect.SetRadiusOverTime(0f, 6f);
        //flashlightEffect.SetHardnessOverTime(0.5f, 6f);

        SpawnProjectile(0, 0f, Vector3.zero).Fire();
    }
}