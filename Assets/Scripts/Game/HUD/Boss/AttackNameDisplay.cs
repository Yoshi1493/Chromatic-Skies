using UnityEngine;
using TMPro;

public class AttackNameDisplay : ShipHUDComponent<Boss>
{
    Animator anim;
    TextMeshProUGUI nameText;

    [SerializeField] StringObject[] attackNames;

    protected override void Awake()
    {
        base.Awake();

        anim = GetComponent<Animator>();
        nameText = GetComponent<TextMeshProUGUI>();

        if (ship != null)
        {
            ship.StartAttackAction += OnAttackStart;
            ship.LoseLifeAction += OnBossLoseLife;
        }
    }

    void OnAttackStart(int attackIndex)
    {
        if (attackNames[attackIndex] != null)
        {
            nameText.text = attackNames[attackIndex].value;
            anim.SetBool("show_name", true);
        }
    }

    void OnBossLoseLife()
    {
        anim.SetBool("show_name", false);
    }
}