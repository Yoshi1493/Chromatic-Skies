using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class PiscesCommonMovement8 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return LeaveScene();
    }
}