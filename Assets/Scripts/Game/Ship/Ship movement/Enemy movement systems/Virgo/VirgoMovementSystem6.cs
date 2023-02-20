using System.Collections;
using UnityEngine;

public class VirgoMovementSystem6 : EnemyMovement
{
    protected override void OnEnable()
    {
        StartCoroutine(this.MoveTo(new Vector3(0f, 2f, 0f), 1f));
    }

    protected override IEnumerator Move()
    {
        yield break;
    }
}