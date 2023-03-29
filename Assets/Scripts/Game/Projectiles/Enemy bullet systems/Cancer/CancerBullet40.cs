using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerBullet40 : ScriptableEnemyBullet<CancerBulletSystem4, EnemyBullet>
{
    Vector3 rotationAxis;
    const float RotationSpeed = 180f;
    const float FireDelay = 1.5f;
    const float MutationChance = 0.1f;

    protected override float MaxLifetime => 5f;

    protected override IEnumerator Move()
    {
        float endSpeed = MoveSpeed;
        MoveSpeed = 0f;
        yield return WaitForSeconds(FireDelay);

        MoveSpeed = endSpeed;
        this.LookAt(ownerShip);
        yield return WaitForSeconds(FireDelay);

        CheckMutation();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        rotationAxis = Vector3.right.RotateVectorBy(transform.eulerAngles.z);
    }

    protected override void Update()
    {
        base.Update();

        if (currentLifetime < FireDelay)
        {
            transform.RotateAround(ownerShip.transform.position, rotationAxis, RotationSpeed * Time.deltaTime);
        }
    }

    void CheckMutation()
    {
        float y = transform.position.y;

        if (y < ownerShip.transform.position.y && y > playerShip.transform.position.y)
        {
            if (Random.value < MutationChance)
            {
                float z = transform.eulerAngles.z;
                Vector3 pos = transform.position;

                SpawnBullet(1, z, pos, false).Fire();
                Destroy();
            }
        }
    }
}