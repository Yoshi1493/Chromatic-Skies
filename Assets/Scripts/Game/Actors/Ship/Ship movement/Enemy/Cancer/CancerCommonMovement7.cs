using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerCommonMovement7 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return LeaveScene();
    }
}