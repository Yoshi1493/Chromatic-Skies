using System.Collections;
using UnityEngine;
using static CameraBoundaries;

public class PiscesCommonMovement5 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        float t = Mathf.Acos(0.6f) * Mathf.Rad2Deg;

        while (transform.position.y > -ScreenHalfHeight * 1.1f)
        {
            yield return parentShip.TranslateAround(3f * Vector3.right + transform.position, t, 0.8f);
            yield return parentShip.TranslateAround(3f * Vector3.left + transform.position, -t, 0.8f);
        }

        yield return LeaveScene();
    }
}