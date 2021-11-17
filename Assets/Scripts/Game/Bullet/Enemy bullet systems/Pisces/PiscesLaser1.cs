using UnityEngine;

public class PiscesLaser1 : Laser
{
    protected override void OnEnable()
    {
        base.OnEnable();

        StartCoroutine(this.LerpSpeed(3f, 0f, 0.3f));
        StartCoroutine(this.LerpSpeed(0f, 3f, 1f, 0.3f));
    }
}