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

        ship.LoseLifeAction += OnEnemyLoseLife;

        for (int i = 0; i < ship.transform.childCount; i++)
        {
            if (ship.transform.GetChild(i).TryGetComponent(out IEnemyAttack enemyAttack))
            {
                enemyAttack.AttackStartAction += OnEnemyAttackStart;
            }
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