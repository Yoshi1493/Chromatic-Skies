using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesCommonMovement5 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return LeaveScene();
    }
}