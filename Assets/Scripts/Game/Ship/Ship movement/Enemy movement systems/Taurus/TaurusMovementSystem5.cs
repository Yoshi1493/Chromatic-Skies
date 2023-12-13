using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class TaurusMovementSystem5 : EnemyMovement
{
    List<Vector3> positions = new(5);
    const float MovementRadius = 1.5f;

    protected override IEnumerator Move()
    {
        Vector3 p0 = transform.position;

        positions.Clear();

        for (int i = 0; i < positions.Capacity; i++)
        {
            Vector3 p = p0 + (MovementRadius * Vector3.up.RotateVectorBy((i + 1) * TaurusBulletSystem5.WaveSpacing));
            positions.Add(p);
        }

        for (int i = 0; i < positions.Count; i++)
        {
            yield return this.MoveTo(positions[i], 0.5f);
        }

        //yield return this.MoveTo(p0, 0.5f);

        yield return WaitForSeconds(3f);
    }
}