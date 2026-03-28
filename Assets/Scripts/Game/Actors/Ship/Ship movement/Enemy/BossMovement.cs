using System.Collections;
using UnityEngine;

public abstract class BossMovement : ShipMovement<Boss>
{
    public ShipObject shipData;

    protected IEnumerator moveCoroutine;
    protected abstract IEnumerator Move();

    protected Player playerShip;
    protected Vector3 PlayerPosition => playerShip.transform.position;

    protected override void Awake()
    {
        base.Awake();

        //get respective bullet system
        int siblingIndex = transform.GetSiblingIndex();
        parentShip.bulletSystems[siblingIndex].StartMoveAction += StartMove;

        //find player
        playerShip = FindAnyObjectByType<Player>();
    }

    protected override void Start()
    {
        base.Start();
        playerShip.DeathAction += OnPlayerDie;
    }

    protected virtual void OnEnable()
    {
        StartCoroutine(parentShip.ReturnToOriginalPosition());
    }

    protected override void OnLoseLife()
    {
        StopAllCoroutines();
    }

    void OnPlayerDie()
    {
        StopAllCoroutines();
        parentShip.MoveSpeed = 0f;
        enabled = false;
    }

    protected virtual void StartMove()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }

        moveCoroutine = Move();
        StartCoroutine(moveCoroutine);
    }
}