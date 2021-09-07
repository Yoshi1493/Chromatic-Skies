using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    Image healthBarImage;

    [SerializeField] ColourObject healthBarColour;

    [SerializeField] IntObject currentHealth;
    [SerializeField] IntObject maxHealth;

    void Awake()
    {
        healthBarImage = GetComponent<Image>();
        healthBarImage.color = healthBarColour.value;
    }

    void Update()
    {
        healthBarImage.fillAmount = (float)currentHealth.value / maxHealth.value;
    }
}