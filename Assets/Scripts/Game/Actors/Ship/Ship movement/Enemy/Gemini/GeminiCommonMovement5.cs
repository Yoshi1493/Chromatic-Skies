using System.Collections;
using UnityEngine;
using static CoroutineHelper;
using static CameraBoundaries;

public class GeminiCommonMovement5 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        float d = SignX;
        Vector3 p0 = parentShip.transform.position;
        Vector3 p1 = p0 + new Vector3(-d * 3f, 8f) + 0.5f * (Vector3)Random.insideUnitCircle;

        float r = Random.Range(-1f, 1f);
        Vector3 p2 = new(-d * ScreenHalfWidth * 1.1f, p1.y + r);

        yield return parentShip.SpiralIntoPoint(p1, d * 30f, 1.2f);
        yield return WaitForSeconds(5f);
        yield return parentShip.SpiralIntoPoint(p2, d * r * 20f, 1.5f);

        yield return LeaveScene();
    }
}