using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CoroutineHelper;

public class LeoMovementSystem4 : EnemyMovement
{
    LeoBulletSystem4 bulletSystem;
    List<Vector3> clonePositions;

    protected override void Awake()
    {
        base.Awake();
        bulletSystem = parentShip.GetComponentInChildren<LeoBulletSystem4>();
    }

    protected override IEnumerator Move()
    {
        yield return WaitForSeconds(0.5f);

        Vector3 p1 = 1.2f * new Vector3(screenHalfWidth, screenHalfHeight);
        yield return this.MoveTo(p1, 1f);
        parentShip.Invincible = true;

        yield return WaitForSeconds(1.5f);
        parentShip.Invincible = false;

        clonePositions = bulletSystem.bulletSpawnPositions;

        Vector3 p2 = new(1.1f * -screenHalfWidth, clonePositions[0].y + Random.Range(-0.5f, 0.5f));
        parentShip.transform.position = p2;

        for (int i = 0; i < clonePositions.Count; i++)
        {
            yield return this.MoveTo(clonePositions[i], 0.5f);
        }

        Vector3 p3 = new(1.1f * screenHalfWidth, clonePositions[^1].y + Random.Range(-0.5f, 0.5f));

        yield return this.MoveTo(p3, 0.5f);

        Vector3 p4 = new(PlayerPosition.x + (Random.value * 2 - 1), 1.1f * screenHalfHeight);
        Vector3 p5 = new(p4.x, 2f);

        yield return this.MoveFromTo(p4, p5, 1f);
        clonePositions.Clear();
    }
}