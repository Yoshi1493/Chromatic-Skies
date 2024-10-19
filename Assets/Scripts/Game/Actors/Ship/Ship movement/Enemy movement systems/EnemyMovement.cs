using System.Collections;
using UnityEngine;

public abstract class EnemyMovement : ShipMovement<Enemy>
{
    protected IEnumerator moveCoroutine;
    protected abstract IEnumerator Move();

    protected float screenHalfHeight;
    protected float screenHalfWidth;

    protected Player playerShip;
    protected Vector3 PlayerPosition => playerShip.transform.position;

    protected override void Awake()
    {
        base.Awake();

        //get respective bullet system
        int siblingIndex = transform.GetSiblingIndex();
        parentShip.bulletSystems[siblingIndex].StartMoveAction += StartMove;

        //find player
        playerShip = FindObjectOfType<Player>();

        //set screen dimensions
        Camera mainCam = Camera.main;
        screenHalfHeight = mainCam.orthographicSize;
        screenHalfWidth = screenHalfHeight * mainCam.aspect;
    }

    protected override void Start()
    {
        base.Start();
        playerShip.DeathAction += OnPlayerDie;
    }

    protected virtual void OnEnable()
    {
        StartCoroutine(this.ReturnToOriginalPosition());
    }

    protected override void OnLoseLife()
    {
        StopAllCoroutines();
    }

    void OnPlayerDie()
    {
        StopAllCoroutines();
        currentSpeed = 0f;
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