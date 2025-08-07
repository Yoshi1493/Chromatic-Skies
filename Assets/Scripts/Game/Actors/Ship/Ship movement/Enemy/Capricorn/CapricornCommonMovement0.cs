using System.Collections;
using UnityEngine;

public class CapricornCommonMovement0 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        Vector3 pos = parentShip.transform.position;
        float d = -Mathf.Sign(pos.x);
        yield return parentShip.TranslateAround(pos + (d * 6f * Vector3.right), d * 180f, Random.Range(3f, 4f));

        yield return LeaveScene();
    }
}