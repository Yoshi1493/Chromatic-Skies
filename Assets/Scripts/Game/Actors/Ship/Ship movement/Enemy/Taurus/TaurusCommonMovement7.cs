using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusCommonMovement7 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return LeaveScene();
    }
}