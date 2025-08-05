using System.Collections;
using UnityEngine;

public class AriesCommonMovement03 : CommonEnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return parentShip.MoveRelative(Vector3.down, 4f, 1f);
    }
}