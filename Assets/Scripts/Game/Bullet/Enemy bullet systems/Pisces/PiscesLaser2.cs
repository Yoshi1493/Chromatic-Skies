using UnityEngine;

public class PiscesLaser2 : Laser
{
    protected override void OnEnable()
    {
        base.OnEnable();

        StartCoroutine(this.LerpSpeed(6f, 2f, 1f));
    }
}