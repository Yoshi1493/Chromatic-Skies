using System.Collections;
using static CoroutineHelper;

public class PSInvinciblePlayerShield : ParticleEffect
{
    Player playerShip;

    protected override void Awake()
    {
        base.Awake();
        playerShip = FindObjectOfType<Player>();
    }

    protected override IEnumerator Play()
    {
        yield return base.Play();

        float particleLifetime = ParticleSystem.GetFloat("ParticleLifetime");
        yield return WaitForSeconds(particleLifetime);

        VFXObjectPool.Instance.ReturnToPool(gameObject, VFXType.InvinciblePlayerShield);
    }

    void Update()
    {
        transform.position = playerShip.transform.position;
    }
}