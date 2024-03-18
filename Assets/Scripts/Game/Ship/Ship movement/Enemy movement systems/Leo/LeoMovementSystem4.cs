using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        print($"starting movement at {Time.timeSinceLevelLoad}");
        yield return this.MoveTo(p1, 1f);
        parentShip.Invincible = true;

        yield return WaitForSeconds(3f);
        parentShip.Invincible = false;

        clonePositions = bulletSystem.bulletSpawnPositions.OrderBy(i => i.x).ToList();

        Vector3 p2 = new(1.1f * -screenHalfWidth, clonePositions[0].y + Random.Range(-0.5f, 0.5f));
        parentShip.transform.position = p2;

        for (int i = 0; i < clonePositions.Count; i++)
        {
            yield return this.MoveTo(clonePositions[i], 0.5f);
            print($"[{i}] {Time.timeSinceLevelLoad}");
        }

        Vector3 p3 = new(1.1f * screenHalfWidth, clonePositions[^1].y + Random.Range(-0.5f, 0.5f));

        yield return this.MoveTo(p3, 0.5f);

        Vector3 p4 = new(0.5f * Random.Range(-screenHalfWidth, screenHalfWidth), 1.1f * screenHalfHeight);
        Vector3 p5 = new(p4.x, p1.y);

        yield return this.MoveFromTo(p4, p5, 1f);
        clonePositions.Clear();
    }
}