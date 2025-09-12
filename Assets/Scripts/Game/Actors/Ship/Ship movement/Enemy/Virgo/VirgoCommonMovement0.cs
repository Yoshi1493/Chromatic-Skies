using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoCommonMovement0 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        Vector3 rotationPoint = 6f * Vector3.up;
        float d = -SignX;

        yield return parentShip.TranslateAround(rotationPoint, d * 90f, 2f);
        yield return WaitForSeconds(1f);
        yield return parentShip.TranslateAround(rotationPoint, d * 90f, 2f);

        yield return LeaveScene();
    }
}