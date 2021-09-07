using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class StatBarController : MonoBehaviour
{
    EventSystem currentEventSystem;

    [SerializeField] ShipObject[] players;
    [SerializeField] PlayerStatBar[] statBars;
    float[,] fillAmounts;

    [SerializeField] IntObject selectedPlayerIndex;

    void Awake()
    {
        currentEventSystem = EventSystem.current;

        SetFillAmounts();
        AnimateStatBars(selectedPlayerIndex.value);
    }

    //initialize values in statBarFillAmounts
    void SetFillAmounts()
    {
        //find max <stat> among all player ships
        float[] maxStatValues = new float[]
        {
            players.Select(i => i.Health.OriginalValue).Max(),
            players.Select(i => i.Power.OriginalValue).Max(),
            players.Select(i => i.Defense.OriginalValue).Max(),
            players.Select(i => i.ShootingSpeed.OriginalValue).Max()
        };

        //set statBarFillAmounts based on max stat values
        fillAmounts = new float[statBars.Length, players.Length];
        for (int i = 0; i < fillAmounts.GetLength(0); i++)
        {
            fillAmounts[0, i] = players[i].Health.OriginalValue / maxStatValues[0];
            fillAmounts[1, i] = players[i].Power.OriginalValue / maxStatValues[1];
            fillAmounts[2, i] = players[i].Defense.OriginalValue / maxStatValues[2];
            fillAmounts[3, i] = players[i].ShootingSpeed.OriginalValue / maxStatValues[3];
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

    void AnimateStatBars(int selectedPlayerIndex)
    {
        for (int i = 0; i < statBars.Length; i++)
        {
            statBars[i].AnimateStatBar(fillAmounts[i, selectedPlayerIndex], players[selectedPlayerIndex].UIColour.value);
        }
    }

    void Update()
    {
        if (selectedPlayerIndex.value != currentEventSystem.currentSelectedGameObject.transform.GetSiblingIndex())
        {
            selectedPlayerIndex.value = currentEventSystem.currentSelectedGameObject.transform.GetSiblingIndex();
            AnimateStatBars(selectedPlayerIndex.value);
        }
    }
}