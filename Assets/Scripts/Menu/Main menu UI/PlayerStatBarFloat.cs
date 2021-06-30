using System.Linq;
using UnityEngine;

public class PlayerStatBarFloat : PlayerStatBar
{
    [SerializeField] FloatObject[] stats;

    public override void SetFillAmounts()
    {
        fillAmounts = new float[stats.Length];

        maxStatValue = stats.Select(i => i.value).Max();

        for (int i = 0; i < fillAmounts.Length; i++)
        {
            fillAmounts[i] = stats[i].value / maxStatValue;
            fillAmounts[i] = (fillAmounts[i] - 0.5f) * 2;
        }

        base.SetFillAmounts();
    }
}