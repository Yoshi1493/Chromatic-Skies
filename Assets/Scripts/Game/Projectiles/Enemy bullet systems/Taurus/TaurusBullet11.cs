using System;
using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBullet11 : ScriptableEnemyBullet<TaurusBulletSystem12, Laser>
{
    protected override float MaxLifetime => 8f;

    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(0.5f);
        SpawnLasers();
    }

    void SpawnLasers()
    {
        Vector3 rayOrigin = transform.position;
        float rayDistance = TaurusBulletSystem11.BulletSpacing * 0.7071f;
        int layerMask = 1 << LayerMask.NameToLayer("Enemy bullet");

        var hits = Array.FindAll(Physics2D.OverlapCircleAll(rayOrigin, rayDistance, layerMask), i => i.transform.position != transform.position);

        for (int i = 0; i < hits.Length; i++)
        {
            float z = transform.position.GetRotationDifference(hits[i].transform.position);
            SpawnBullet(0, z, transform.position, false).Fire();
        }
    }

    protected override void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, TaurusBulletSystem11.BulletSpacing * 0.7071f);
    }
}