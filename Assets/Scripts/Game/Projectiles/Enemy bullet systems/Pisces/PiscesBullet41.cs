using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesBullet41 : ScriptableEnemyBullet<PiscesBulletSystem41, Laser>
{
    const int LaserCount = 6;
    const float LaserSpacing = 360f / LaserCount;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(2.8f, 0f, 1f);

        var siblingBullets = FindObjectsOfType<PiscesBullet41>();
        var homingDuration = 2f + (System.Array.IndexOf(siblingBullets, this) * 0.5f);

        yield return this.HomeInOn(playerShip, homingDuration);

        for (int i = 0; i < LaserCount; i++)
        {
            float z = (i * LaserSpacing) + transform.eulerAngles.z;
            Vector3 pos = transform.position;

            SpawnBullet(0, z, pos, false).Fire();
        }

        yield return WaitForSeconds(1.5f);
        Destroy();
    }
}