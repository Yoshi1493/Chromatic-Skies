using System.Collections;
using UnityEngine;

public class PiscesBullet21 : EnemyBullet
{
    protected override IEnumerator Move()
    {
        bool rotatesClockwise = Random.value > 0.5f;

        while (enabled)
        {
            StartCoroutine(this.RotateBy(60f, 1f, rotatesClockwise));

            yield return this.LerpSpeed(0.5f, 3f, 0.5f);
            yield return this.LerpSpeed(3f, 0.5f, 0.5f);

            moveDirection = Vector3.down;

            rotatesClockwise = !rotatesClockwise;
        }
    }
}