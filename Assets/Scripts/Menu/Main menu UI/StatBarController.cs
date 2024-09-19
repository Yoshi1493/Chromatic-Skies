using System.Linq;
using UnityEngine;

public class StatBarController : MonoBehaviour
{
    [SerializeField] ShipObject[] players;
    [SerializeField] PlayerStatBar[] statBars;
    float[,] fillAmounts;

    [SerializeField] IntObject selectedPlayerIndex;

    void Awake()
    {
        InitializeStatBars();
    }

    void Start()
    {
        for (int i = 0; i < statBars.Length; i++)
        {
            statBars[i].SetStatBar(fillAmounts[i, 0], players[0].UIColour.value);
        }
    }

    //initialize values in statBarFillAmounts
    void InitializeStatBars()
    {
        //find max <stat> among all player ships
        float[] maxStatValues = new float[]
        {
            players.Select(i => i.MaxHealth.Value).Max(),
            players.Select(i => i.Power.Value).Max(),
            players.Select(i => i.Defense.Value).Max(),
            players.Select(i => i.ShootingSpeed.Value).Max()
        };

        //set statBarFillAmounts based on max stat values
        fillAmounts = new float[statBars.Length, players.Length];
        for (int i = 0; i < fillAmounts.GetLength(0); i++)
        {
            fillAmounts[0, i] = players[i].MaxHealth.Value / maxStatValues[0];
            fillAmounts[1, i] = players[i].Power.Value / maxStatValues[1];
            fillAmounts[2, i] = players[i].Defense.Value / maxStatValues[2];
            fillAmounts[3, i] = players[i].ShootingSpeed.Value / maxStatValues[3];
        }

        //adjust amounts to emphasize stat differences between each other
        for (int i = 0; i < fillAmounts.GetLength(0); i++)
        {
            for (int j = 0; j < fillAmounts.GetLength(1); j++)
            {
                fillAmounts[i, j] = (fillAmounts[i, j] - 0.5f) * 2;
            }
        }
    }

    public void AnimateStatBars(int selectedPlayerIndex)
    {
        for (int i = 0; i < statBars.Length; i++)
        {
            statBars[i].AnimateStatBar(fillAmounts[i, selectedPlayerIndex], players[selectedPlayerIndex].UIColour.value);
        }
    }
}