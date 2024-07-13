using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class TaurusMovementSystem5 : EnemyMovement
{
    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(3f);

        while (enabled)
        {
            int r = Random.Range(1, 4);

            for (int i = 0; i < r; i++)
            {
                yield return this.MoveToRandomPosition(1f, 0.5f, 1f);
            }

            yield return WaitForSeconds(1f);
        }
    }
}