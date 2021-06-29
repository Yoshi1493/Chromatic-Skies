using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSelectMenu : Menu
{
    StandaloneInputModule sim;

    protected override void Awake()
    {
        base.Awake();
        sim = FindObjectOfType<StandaloneInputModule>();
    }

    void Update()
    {
        if (Input.GetButtonDown(sim.horizontalAxis))
        {
            print("horizontal button pressed.");
        }
    }
}