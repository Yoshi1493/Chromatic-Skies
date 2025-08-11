using System.Collections;
using UnityEngine;

public class CapricornCommonMovement2 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        Vector3 pos = parentShip.transform.position;
        float d = -Mathf.Sign(pos.x);
        yield return parentShip.TranslateAround(pos + (d * 6f * Vector3.right), d * 135f, 3f);

        yield return LeaveScene();
    }
}