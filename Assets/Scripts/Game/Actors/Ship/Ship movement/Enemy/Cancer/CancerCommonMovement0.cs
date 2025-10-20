using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerCommonMovement0 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return LeaveScene();
    }
}