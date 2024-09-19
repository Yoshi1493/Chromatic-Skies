using UnityEngine;
using UnityEngine.UI;

public class EnemySelectMenu : Menu
{
    [SerializeField] Button backButton;

    [Space]

    [SerializeField] IntObject selectedEnemyIndex;

    public void SelectEnemy(int enemyIndex)
    {
        selectedEnemyIndex.value = enemyIndex;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            backButton.OnPointerClick(eventData);
        }
    }
}