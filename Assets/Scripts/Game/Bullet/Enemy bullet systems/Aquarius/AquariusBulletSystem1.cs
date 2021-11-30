using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBulletSystem1 : EnemyShooter<EnemyBullet>
{
    [SerializeField] ProjectileObject bulletData;

    protected override IEnumerator Shoot()
    {
        yield return base.Shoot();

        SetSubsystemEnabled(1);

        while (enabled)
        {
            float z = Random.Range(120f, 240f);

            int rand = Random.Range(0, 4);
            bulletData.colour = bulletData.gradient.Evaluate(rand / 4f);

            var bullet = SpawnProjectile(Random.value > 0.25f ? 0 : 1, z, Vector3.zero);
            bullet.MoveSpeed = rand + 4f;
            bullet.Fire();

            yield return WaitForSeconds(ShootingCooldown);

            #region DEBUG
            //for (int i = 0; i < 1; i++)
            //{
            //    float z = Random.Range(120f, 240f);

            //    int rand = Random.Range(0, 4);
            //    bulletData.colour = bulletData.gradient.Evaluate(rand / 4f);

            //    var bullet = SpawnProjectile(0, z, Vector3.zero);
            //    bullet.MoveSpeed = rand + 4f;
            //    bullet.Fire();

            //    yield return WaitForSeconds(ShootingCooldown * 100f);
            //} 
            #endregion
        }
    }
}