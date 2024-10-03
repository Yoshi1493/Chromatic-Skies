using UnityEngine;
using TMPro;

public class AttackNameDisplay : ShipHUDComponent<Enemy>
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
            ship.LoseLifeAction += OnEnemyLoseLife;
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

    void OnEnemyLoseLife()
    {
        anim.SetBool("show_name", false);
    }
}