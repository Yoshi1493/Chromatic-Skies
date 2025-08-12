using System.Collections;
using UnityEngine;

public class CapricornCommonMovement2 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        float d = -SignX;
        yield return parentShip.TranslateAround(parentShip.transform.position + (d * 6f * Vector3.right), d * 135f, 3f);

        yield return LeaveScene();
    }
}