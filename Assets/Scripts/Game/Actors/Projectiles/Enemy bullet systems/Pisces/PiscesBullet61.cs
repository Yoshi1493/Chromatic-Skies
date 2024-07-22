using System.Collections;
using UnityEngine;

public class PiscesBullet61 : ScriptableEnemyBullet<PiscesBulletSystem62, Laser>
{
    [Space]
    [SerializeField] ProjectileObject laserData;

    protected override float MaxLifetime => Mathf.Infinity;

    protected override IEnumerator Move()
    {
        MoveSpeed = 0f;

        float z = 45f * Mathf.Sign(-transform.position.x) + 180f;
        Vector3 pos = transform.position;

        laserData.colour = spriteRenderer.color;
        SpawnBullet(0, z, pos, false).Fire(1f);
        yield return null;
    }
}