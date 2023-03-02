using UnityEngine;

public class EnemyPositionDisplay : HUDComponent<Enemy>
{
    new Transform transform;

    protected override void Awake()
    {
        base.Awake();

        transform = GetComponent<Transform>();
        ship.DeathAction += () => SetActive(false);
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