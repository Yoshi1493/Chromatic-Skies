using System.Collections;
using UnityEngine;

public class AriesCommonMovement00 : CommonEnemyMovement
{
    [SerializeField] Vector2 rotationPoint;

    protected override IEnumerator Move()
    {
        yield return parentShip.TranslateAround(rotationPoint, 540f, 6f);
        yield return parentShip.MoveRelativeLinear(Vector2.up, Mathf.PI, 1f);

        yield return LeaveScene();
    }
}