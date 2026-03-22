using System;
using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBullet11 : ScriptableBossBullet<TaurusBossShooter12, BossLaser>
{
    [SerializeField] LayerMask bossBulletLayer;
    protected override float MaxLifetime => 9f;

    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(0.5f);
        SpawnLasers();
        yield return WaitForSeconds(5f);
        SpawnLasers();
    }

    void SpawnLasers()
    {
        Vector3 rayOrigin = transform.position;
        float rayDistance = TaurusBossShooter11.BulletDensity;
        int layerMask = bossBulletLayer.value;

        var hits = Array.FindAll(Physics2D.OverlapCircleAll(rayOrigin, rayDistance, layerMask), i => (
            i.transform.position.x == transform.position.x ||
            i.transform.position.y == transform.position.y) &&
            i.transform.position != transform.position);

        for (int i = 0; i < hits.Length; i++)
        {
            float z = transform.position.GetRotationDifference(hits[i].transform.position);
            Vector3 pos = transform.position;

            SpawnBullet(0, z, pos, false).Fire();
        }
    }
}