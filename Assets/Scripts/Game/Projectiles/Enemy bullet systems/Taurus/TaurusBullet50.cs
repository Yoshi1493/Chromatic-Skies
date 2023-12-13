using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBullet50 : ScriptableEnemyBullet<TaurusBulletSystem52, Laser>
{
    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(1f);

        float z = transform.eulerAngles.z;
        Vector3 pos = transform.position;

        SpawnBullet(0, z, pos, false).Fire();
    }
}