using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SpecialShooterRed : PlayerSpecialShooter
{
    protected override IEnumerator Shoot()
    {
        yield return null;
    }
}