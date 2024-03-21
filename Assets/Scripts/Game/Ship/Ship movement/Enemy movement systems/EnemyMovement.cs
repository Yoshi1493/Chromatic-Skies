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

        //set screen dimensions
        Camera mainCam = Camera.main;
        screenHalfHeight = mainCam.orthographicSize;
        screenHalfWidth = screenHalfHeight * mainCam.aspect;

        //find player
        if (playerShip == null)
        {
            playerShip = FindObjectOfType<Player>();
        }
    }

    protected virtual void OnEnable()
    {
        StartCoroutine(this.ReturnToOriginalPosition());
    }

    protected override void OnLoseLife()
    {
        StopAllCoroutines();
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