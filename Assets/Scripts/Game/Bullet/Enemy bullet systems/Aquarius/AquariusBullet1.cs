using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AquariusBullet1 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        //float delay = 0.1f * (5f - MoveSpeed);
        yield return WaitForSeconds(0.25f);

        float dX = ownerShip.transform.position.x - transform.position.x;
        float x = transform.position.x - dX;
        Vector3 endPos = x * Vector3.right;

        StartCoroutine(this.GraduallyLookAt(endPos, 1f));
    }
}