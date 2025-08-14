using System.Collections;
using UnityEngine;

public class CapricornCommonMovement2 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.TranslateAround(parentShip.transform.position + (-SignX * 6f * Vector3.right), -SignX * 150f, 3f);

        yield return LeaveScene();
    }
}