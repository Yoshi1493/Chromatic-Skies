using System.Collections;
using UnityEngine;

public class PiscesBullet41 : ScriptableEnemyBullet<PiscesBulletSystem41, Laser>
{
    const int LaserCount = 6;
    const float LaserSpacing = 360f / LaserCount;

    protected override float MaxLifetime => 6f;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(2.8f, 0f, 1f);
        yield return this.HomeInOn(playerShip, 4f);

        for (int i = 0; i < LaserCount; i++)
        {
            float z = (i * LaserSpacing) + transform.eulerAngles.z;
            Vector3 pos = transform.position;

            SpawnBullet(0, z, pos, false).Fire();
        }
    }
}