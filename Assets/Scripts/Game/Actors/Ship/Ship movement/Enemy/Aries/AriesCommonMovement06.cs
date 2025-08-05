using System.Collections;
using UnityEngine;

public class AriesCommonMovement06 : CommonEnemyMovement
{
    [SerializeField] Vector2 moveDirection;

    protected override IEnumerator Move()
    {
        moveDirection = ((Vector3)moveDirection.normalized).RotateVectorBy(Random.Range(-5f, 5f));

        yield return parentShip.MoveRelative(moveDirection, Random.Range(3.5f, 4f), 6f);
        yield return LeaveScene();
    }
}