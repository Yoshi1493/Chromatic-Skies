using UnityEngine;

public class Enemy : Ship
{
    protected override void Die()
    {
        gameObject.SetActive(false);
    }
}