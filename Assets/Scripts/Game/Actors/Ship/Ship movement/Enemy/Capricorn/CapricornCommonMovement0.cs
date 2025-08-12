using System.Collections;
using UnityEngine;

public class CapricornCommonMovement0 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        float d = -SignX;
        yield return parentShip.TranslateAround(parentShip.transform.position + (d * 6f * Vector3.right), d * 180f, Random.Range(3f, 4f));

        yield return LeaveScene();
    }
}