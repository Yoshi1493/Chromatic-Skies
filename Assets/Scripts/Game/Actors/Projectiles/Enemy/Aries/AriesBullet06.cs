using System.Collections;
using UnityEngine;

public class AriesBullet06 : MinibossBullet
{
    [HideInInspector] public bool rotatesClockwise;

    protected override IEnumerator Move()
    {
        while (enabled)
        {
            StartCoroutine(this.RotateBy(-60f, 1f, rotatesClockwise));
            yield return this.LerpSpeed(4f, 1f, 1f);

            StartCoroutine(this.RotateBy(60f, 1f, rotatesClockwise));
            yield return this.LerpSpeed(4f, 1f, 1f);
        }
    }
}