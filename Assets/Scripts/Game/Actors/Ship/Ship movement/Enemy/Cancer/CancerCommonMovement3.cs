using System.Collections;
using UnityEngine;

public class CancerCommonMovement3 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        float r = Random.Range(-5f, 5f);
        float moveSpeed = 6f;
        float rotationAmount = 1.5f * Mathf.PI;

        yield return parentShip.MoveRelativeLinear((SignX * Vector3.left).RotateVectorBy(r), moveSpeed, 1.5f);
        yield return parentShip.TranslateAroundLinear(transform.position + Vector3.down.RotateVectorBy(r), SignX * rotationAmount * Mathf.Rad2Deg, rotationAmount / moveSpeed);
        yield return parentShip.MoveRelativeLinear(Vector3.up.RotateVectorBy(r), moveSpeed, 1f);

        yield return LeaveScene();
    }
}