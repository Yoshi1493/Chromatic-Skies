using System.Collections;

public abstract class SpecialBullet : PlayerBullet
{
    public void Fire()
    {

    }

    protected abstract IEnumerator Move();
}