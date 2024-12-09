using UnityEngine;

public static class DamageCalculator
{
    public static int CalculateDamage(int atk, int def, float speed)
    {
        float trueSpd = Mathf.Max(Mathf.Abs(speed), 1f);
        float trueAtk = atk * trueSpd;
        float trueDef = 100f / (100f + def);

        float dmg = trueAtk * trueDef;
        //Debug.Log($"({atk} * {trueSpd}) * (100 / (100 + {def}))");
        int trueDmg = Mathf.Max(Mathf.CeilToInt(dmg), 1);

        return trueDmg;
    }

    public static int CalculateHealing(int maxHealth, int redundancy)
    {
        float redundancyFactor = (Mathf.Min(redundancy, 100) - 100) / 10f;
        float sqrRedundancyFactor = redundancyFactor * redundancyFactor;
        float healing = maxHealth * sqrRedundancyFactor * 0.0001f;              //1% max hp * redundancy factor as percent

        //Debug.Log($"{maxHealth} * 0.01 * {redundancyFactor:f2} * {redundancyFactor:f2} = {healing}");
        int trueHealing = Mathf.Max(Mathf.CeilToInt(healing), 1);

        return -trueHealing;
    }
}