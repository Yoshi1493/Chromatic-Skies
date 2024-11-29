using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SpecialShooterYellow : PlayerSpecialShooter
{
    protected override IEnumerator Shoot()
    {
        yield return null;
    }
}