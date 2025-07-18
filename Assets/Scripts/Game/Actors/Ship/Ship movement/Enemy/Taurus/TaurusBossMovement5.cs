using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusBossMovement5 : BossMovement
{
    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(3f);

        while (enabled)
        {
            int r = Random.Range(1, 4);

            for (int i = 0; i < r; i++)
            {
                yield return parentShip.MoveToRandomPosition(1f, 0.5f, 1f);
            }

            yield return WaitForSeconds(1f);
        }
    }
}