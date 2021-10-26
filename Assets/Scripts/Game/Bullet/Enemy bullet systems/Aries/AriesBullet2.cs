using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBullet2 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(1f);
        
        this.LookAt(playerShip);

        float z = Mathf.Atan2(-moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;

        for (int i = 0; i <= 5; i++)
        {
            var newBullet = GenericObjectPool<EnemyBullet>.Instance.Get(0);

            newBullet.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0f, 0f, i * 72f + z + 180f));

            newBullet.gameObject.SetActive(true);
            newBullet.enabled = true;
            newBullet.Fire();
        }

        Destroy();
    }
}