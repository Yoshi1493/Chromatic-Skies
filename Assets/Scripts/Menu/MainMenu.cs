using UnityEngine;

public class MainMenu : Menu
{
    [SerializeField] protected GameObject blackHoleEffect;

    public override void Enable(GameObject newSelectedGameObject)
    {
        base.Enable(newSelectedGameObject);
        blackHoleEffect.SetActive(true);
    }

    public override void Disable()
    {
        blackHoleEffect.SetActive(false);
        base.Disable();
    }
}