using System.Collections;
using UnityEngine;

public class AriesCommonMovement04 : CommonEnemyMovement
{
    [SerializeField] Vector2 rotationOffset;

    protected override IEnumerator Move()
    {
        yield return parentShip.TranslateAround(parentShip.transform.position + (Vector3)rotationOffset, -540f, 3f);
        yield return parentShip.MoveRelativeLinear(Vector2.down, Mathf.PI, 4f);

        yield return LeaveScene();
    }
}