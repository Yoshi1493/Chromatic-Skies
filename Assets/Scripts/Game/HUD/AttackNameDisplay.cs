using System.Collections;
using System.Linq;
using UnityEngine;
using TMPro;
using static CoroutineHelper;

public class AttackNameDisplay : MonoBehaviour
{
    Animator anim;
    TextMeshProUGUI nameText;

    [SerializeField] StringObject[] moduleNames;
    [SerializeField] StringObject[] attackNames;

    Enemy enemy;

    IEnumerator displayNameCoroutine;

    void Awake()
    {
        anim = GetComponent<Animator>();
        nameText = GetComponent<TextMeshProUGUI>();

        enemy = FindObjectOfType<Enemy>();
        var shooters = enemy.GetComponentsInChildren<EnemyBulletSystem>().Where(i => !(i is EnemyBulletSubsystem));

        foreach (EnemyShooter es in shooters)
            es.AttackStartAction += OnEnemyAttackStart;
    }

    void OnEnemyAttackStart()
    {
        int currentAttackIndex = enemy.shipData.MaxLives.Value - enemy.shipData.CurrentLives.Value;

        nameText.text = $"{moduleNames[currentAttackIndex].value} Module | {attackNames[currentAttackIndex].value}";
        //anim.SetTrigger("Show");

        if (displayNameCoroutine != null)
            StopCoroutine(displayNameCoroutine);

        displayNameCoroutine = DisplayName();
        StartCoroutine(displayNameCoroutine);
    }

    IEnumerator DisplayName()
    {
        Color c = nameText.color;

        while (c.a <= 1f)
        {
            c.a += Time.deltaTime;
            nameText.color = c;

            yield return EndOfFrame;
        }

        yield return WaitForSeconds(5f);

        while (c.a >= 0f)
        {
            c.a -= Time.deltaTime;
            nameText.color = c;

            yield return EndOfFrame;
        }

        displayNameCoroutine = null;
    }
}