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

        for (int i = 0; i < ship.bulletSystems.Count; i++)
        {
            ship.bulletSystems[i].StartAttackAction += OnEnemyAttackStart;
        }

        ship.LoseLifeAction += OnEnemyLoseLife;
    }

    void OnEnemyAttackStart(int attackIndex)
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