using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class Enemyground : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float walkStopRate = 0.05f;
    public DetectionZone attackZone;
    Rigidbody2D rb;
    TouchingDirections touchingdirections;
    Animator animator;
    public enum WalkableDirection { Left, Right }
    private Vector2 walkDirectionVector = Vector2.left;

    private WalkableDirection _walkDirection;

    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;

                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }


            _walkDirection = value;
        }
    }
    public bool _hasTarget = false;

    private bool HasTarget
    {
        get
        {
            return _hasTarget;
        }
        set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }
    public bool CanMove
    {
        get
        { 
            return animator.GetBool(AnimationStrings.canMove); 
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingdirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;

    }

    private void FixedUpdate()
    {
        if (touchingdirections.IsOnWall)
        {
            FlipDirection();
        }
        if(CanMove)
        {
            rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
        }

}

private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.LogError("El valor WalkableDirection no está asignado a un valor legal de derecha o izquierda");
        }
    }
}