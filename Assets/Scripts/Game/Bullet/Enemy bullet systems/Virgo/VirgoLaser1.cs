using UnityEngine;

public class VirgoLaser1 : Laser
{
    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(this.LerpSpeed(5f, 0f, 0.5f));
        StartCoroutine(this.LerpSpeed(0f, 5f, 1f, 0.5f));
    }
}