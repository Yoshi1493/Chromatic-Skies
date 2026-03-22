using System.Collections;
using UnityEngine;

public class LeoCommonMovement2 : CommonEnemyMovement
{
    [SerializeField] bool invertMovement;

    protected override IEnumerator Move()
    {
        Vector3 p0 = parentShip.transform.position;
        float d = SignX;

        yield return parentShip.SpiralIntoPointLinear(Vector3.zero, (invertMovement ? d : -d) * 30f, 2f);
        yield return parentShip.SpiralIntoPointLinear(-p0, (invertMovement ? -d : d) * 30f, 2f);

        yield return LeaveScene();
    }
}