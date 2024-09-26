using System.Collections;
using UnityEngine;

public class ControlsRemapScreen : Menu
{
    ControlsMenu controlsMenu;

    protected override void Awake()
    {
        base.Awake();

        controlsMenu = GetComponent<ControlsMenu>();
    }

    void Update()
    {
        
    }
}