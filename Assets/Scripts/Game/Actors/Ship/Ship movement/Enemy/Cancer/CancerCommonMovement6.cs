using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class CancerCommonMovement6 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return LeaveScene();
    }
}