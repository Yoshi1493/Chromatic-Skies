using System.Collections;
using static CoroutineHelper;

public abstract class EnemyMovement : ShipMovement<Enemy>
{
    protected IEnumerator moveCoroutine;
    protected abstract IEnumerator Move();

    protected override void Awake()
    {
        base.Awake();

        // get respective bullet system
        int siblingIndex = transform.GetSiblingIndex();
        parentShip.bulletSystems[siblingIndex].AttackStartAction += OnAttackStart;
        parentShip.bulletSystems[siblingIndex].AttackFinishAction += OnAttackFinish;
    }

    void OnEnable()
    {
        StartCoroutine(this.ReturnToOriginalPosition());
    }

    protected override void OnLoseLife()
    {
        StopAllCoroutines();
        StartCoroutine(this.ReturnToOriginalPosition());
    }

    protected void StartMove()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        moveCoroutine = Move();
        StartCoroutine(moveCoroutine);
    }

    protected virtual void OnAttackStart(int _)
    {

    }

    protected virtual void OnAttackFinish()
    {

    }
}