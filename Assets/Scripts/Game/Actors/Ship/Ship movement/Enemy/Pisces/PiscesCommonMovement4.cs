using System.Collections;
using UnityEngine;
using static CameraBoundaries;

public class PiscesCommonMovement4 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        Vector3 v = new(SignX * ScreenHalfWidth * 1.1f, 0f);
        float r = (SignX * v.GetRotationDifference(transform.position)) - 90f;

        yield return parentShip.TranslateAroundLinear(v, SignX * -r * 2f, 3f);
        yield return LeaveScene();
    }
}