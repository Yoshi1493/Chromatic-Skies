using UnityEngine;
using TMPro;

public class AttackNameDisplay : HUDComponent<Enemy>
{
    Animator anim;
    TextMeshProUGUI nameText;

    protected override void Awake()
    {
        base.Awake();

        anim = GetComponent<Animator>();
        nameText = GetComponent<TextMeshProUGUI>();

        ship.LoseLifeAction += OnEnemyLoseLife;

        for (int i = 0; i < ship.transform.childCount; i++)
        {
            IEnemyAttack enemyAttack = ship.transform.GetChild(i).GetComponent<IEnemyAttack>();
            enemyAttack.AttackStartAction += OnEnemyAttackStart;
        }
    }

    void OnEnemyAttackStart(StringObject moduleName, StringObject attackName)
    {
        nameText.text = $"{moduleName.value} Module | {attackName.value}";

        anim.SetBool("show_name", true);
    }

    void OnEnemyLoseLife()
    {
        anim.SetBool("show_name", false);
    }
}