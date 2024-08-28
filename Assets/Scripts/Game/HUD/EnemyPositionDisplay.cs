using UnityEngine;

public class EnemyPositionDisplay : ShipHUDComponent<Enemy>
{
    new Transform transform;

    protected override void Awake()
    {
        base.Awake();

        transform = GetComponent<Transform>();

        if (ship != null)
        {
            ship.DeathAction += () => SetActive(false);
        }
        else
        {
            enabled = false;
            SetActive(false);
        }
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = ship.transform.position.x;
        transform.position = pos;
    }

    void SetActive(bool state)
    {
        gameObject.SetActive(state);
    }
}