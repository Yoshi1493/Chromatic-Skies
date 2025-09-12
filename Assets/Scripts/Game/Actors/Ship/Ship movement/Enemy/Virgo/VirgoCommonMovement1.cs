using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class VirgoCommonMovement1 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        float x = 8f, y = 6f;
        Vector3 rotationPoint = new(parentShip.transform.position.x + (-SignX * x), parentShip.transform.position.y + y);
        float d = -SignX;
        float r = Mathf.Atan(x / y) * Mathf.Rad2Deg * 2f;

        yield return parentShip.TranslateAround(rotationPoint, d * 0.5f * r, 2f);
        yield return WaitForSeconds(8f);
        yield return parentShip.TranslateAround(rotationPoint, d * 0.6f * r, 2f);

        yield return LeaveScene();
    }
}