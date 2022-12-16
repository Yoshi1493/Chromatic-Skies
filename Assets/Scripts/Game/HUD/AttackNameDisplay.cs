using UnityEngine;
using TMPro;

public class AttackNameDisplay : HUDComponent<Enemy>
{
    Animator anim;
    TextMeshProUGUI nameText;

    [SerializeField] StringObject[] attackNames;

    protected override void Awake()
    {
        base.Awake();

        anim = GetComponent<Animator>();
        nameText = GetComponent<TextMeshProUGUI>();

        ship.LoseLifeAction += OnEnemyLoseLife;

        for (int i = 0; i < ship.transform.childCount; i++)
        {
            ship.transform.GetChild(i).GetComponent<IEnemyAttack>().AttackStartAction += OnEnemyAttackStart;
        }
    }

    void OnEnemyAttackStart(int attackIndex)
    {
        nameText.text = attackNames[attackIndex].value;
        anim.SetBool("show_name", true);
    }

    void OnEnemyLoseLife()
    {
        anim.SetBool("show_name", false);
    }
}