using System.Collections;
using UnityEngine;

public class CapricornCommonMovement0 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.TranslateAround(parentShip.transform.position + (6f * Vector3.right), 180f, Random.Range(3f, 4f));

        yield return LeaveScene();
    }
}