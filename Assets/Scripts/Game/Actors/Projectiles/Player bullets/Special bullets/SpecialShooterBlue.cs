using System.Collections;
using UnityEngine;
using static CoroutineHelper;

public class SpecialShooterBlue : PlayerSpecialShooter
{
    protected override IEnumerator Shoot()
    {
        yield return null;
    }
}