using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpecialBar : ShipHUDComponent<Player>
{
    [SerializeField] Image[] specialBarImages;
    [SerializeField] FloatObject specialMeter;

    protected override void Awake()
    {
        base.Awake();

        ship.GetComponentInChildren<PlayerSpecialShooter>().SpecialMeterUpdateAction += OnSpecialMeterUpdate;
    }

    void Start()
    {
        for (int i = 0; i < specialBarImages.Length; i++)
        {
            specialBarImages[i].color = ship.shipData.UIColour.value;
        }
    }

    void OnSpecialMeterUpdate()
    {
        int trunc = (int)specialMeter.value;
        float dec = specialMeter.value % 1f;

        for (int i = 0; i < specialBarImages.Length; i++)
        {
            if (i < trunc)
            {
                specialBarImages[i].fillAmount = 1f;
            }
            else
            {
                specialBarImages[i].fillAmount = 0f;
            }
        }

        if (trunc < PlayerSpecialShooter.MaxSpecialMeter)
        {
            specialBarImages[trunc].fillAmount = dec;
        }
    }
}