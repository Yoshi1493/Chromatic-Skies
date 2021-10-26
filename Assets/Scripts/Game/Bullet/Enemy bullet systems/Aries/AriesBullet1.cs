using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class AriesBullet1 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        MoveSpeed = 5f;

        for (int i = 0; i < 10; i++)
        {
            yield return WaitForSeconds(0.2f);

            var newBullet = GenericObjectPool<EnemyBullet>.Instance.Get(2);

            newBullet.transform.SetPositionAndRotation(transform.position, transform.rotation);

            newBullet.gameObject.SetActive(true);
            newBullet.enabled = true;
            newBullet.Fire();


            yield return WaitForSeconds(0.2f);
        }

        yield return WaitForSeconds(1f);

        Destroy();
    }
}