using System.Collections;
using UnityEngine;

public class CancerBullet30 : EnemyBullet
{
    float magnitude = CancerBulletSystem3.SpawnRadius;

    Vector3 faceDirection;
    Vector3 smoothDampVel = Vector3.zero;
    float z;

    protected override float MaxLifetime => Mathf.Infinity;

    protected override IEnumerator Move()
    {
        yield return this.LerpSpeed(magnitude * 2f, 0f, 1f);

        faceDirection = playerShip.transform.position - ownerShip.transform.position;
        z = transform.eulerAngles.z + 180f;

        while (enabled)
        {
            UpdatePosition();
            yield return null;
        }
    }

    void UpdatePosition()
    {
        Vector3 ownerShipPos = ownerShip.transform.position;
        Vector3 targetFaceDirection = playerShip.transform.position - ownerShipPos;

        faceDirection = Vector3.SmoothDamp(faceDirection, targetFaceDirection, ref smoothDampVel, 1.0f).normalized;

        transform.position = ownerShipPos + (magnitude * faceDirection.RotateVectorBy(z));
    }
}