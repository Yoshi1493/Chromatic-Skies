using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoCommonMovement0 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        Vector3 rotationPoint = new(parentShip.transform.position.x + (-SignX * 5f), parentShip.transform.position.y);
        float d = -SignX;

        yield return parentShip.TranslateAround(rotationPoint, d * 90f, 2.5f);
        yield return WaitForSeconds(1f);
        yield return parentShip.TranslateAround(rotationPoint, d * 90f, 2.5f);

        yield return LeaveScene();
    }
}