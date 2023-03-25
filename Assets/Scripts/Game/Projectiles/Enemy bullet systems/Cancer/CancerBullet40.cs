using System.Collections;
using UnityEngine;

public class CancerBullet40 : EnemyBullet
{
    Vector3 rotationAxis;
    const float RotationSpeed = 0.5f;

    protected override IEnumerator Move()
    {
        MoveSpeed = 5f;
        yield return null;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        rotationAxis = Vector3.right.RotateVectorBy(transform.eulerAngles.z);
    }

    protected override void Update()
    {
        base.Update();

        if (movementBehaviour == null)
        {
            transform.RotateAround(ownerShip.transform.position, rotationAxis, RotationSpeed);
        }
    }
}