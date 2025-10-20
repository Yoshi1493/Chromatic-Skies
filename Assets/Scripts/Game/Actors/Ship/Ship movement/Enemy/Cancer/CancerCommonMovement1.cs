using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerCommonMovement1 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return LeaveScene();
    }
}