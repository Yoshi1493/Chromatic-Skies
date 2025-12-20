using System.Collections;
using UnityEngine;

public class GeminiBullet02 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        float d = Mathf.Sign(MoveSpeed);
        MoveSpeed = Mathf.PI;
        float r = GeminiShooter02.BranchSpacing * 2f;
        float s = MoveSpeed / 4f;

        while (enabled)
        {
            yield return this.RotateBy(d * r, s);
            yield return this.RotateBy(d * -r, s);
        }
    }
}