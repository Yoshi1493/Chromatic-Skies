using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SpecialShooterGreen : PlayerSpecialShooter
{
    protected override IEnumerator Shoot()
    {
        yield return null;
    }
}