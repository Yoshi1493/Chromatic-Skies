using UnityEngine;

public class VirgoLaser1 : Laser
{
    protected override void OnEnable()
    {
        base.OnEnable();

        StartCoroutine(this.LerpSpeed(4f, 0f, 0.4f));
        StartCoroutine(this.LerpSpeed(0f, 4f, 1f, 0.4f));
    }
}