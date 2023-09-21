using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBullet61 : ScriptableEnemyBullet<PiscesBulletSystem62, Laser>
{
    [Space]
    [SerializeField] ProjectileObject laserData;

    const float LaserSpacing = 10f;
    const float ShootingCooldown = 0.05f;

    protected override float MaxLifetime => 3f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;
        yield return WaitForSeconds(1f);

        float z = transform.position.GetRotationDifference(playerShip.transform.position);
        Vector3 pos = transform.position;

        laserData.colour = spriteRenderer.color;
        SpawnBullet(0, z, pos, false).Fire(1f);
        yield return WaitForSeconds(ShootingCooldown);

    }

    protected override void Update()
    {
        base.Update();
        this.LookAt(playerShip);
    }
}