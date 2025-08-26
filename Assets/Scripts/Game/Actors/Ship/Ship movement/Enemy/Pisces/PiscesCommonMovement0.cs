using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesCommonMovement0 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return LeaveScene();
    }
}