using System.Collections;
using UnityEngine;

public class CancerCommonMovement2 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        float offsetX = 5f, offsetY = -1f;
        Vector3 rotationPoint = new(parentShip.transform.position.x + (SignX * offsetX), parentShip.transform.position.y + offsetY);
        yield return parentShip.TranslateAround(rotationPoint, SignX * 120f, 4f);

        yield return LeaveScene();
    }
}