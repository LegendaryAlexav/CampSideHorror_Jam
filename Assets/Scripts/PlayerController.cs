using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region - States -

    #region - Enum -
    public enum EPlayerDirection : byte {
        Right,
        Left,
    }

    public enum EPlayerState : byte {
        Idle,
        Walking,
        Jumping,
        Traveling,
        Running,
        Turning,
        Dying,
    }

    // End - Enum -
    #endregion

    private EPlayerDirection direction = EPlayerDirection.Right;
    private EPlayerState state = EPlayerState.Idle;

    private bool isRunning = false;
    private bool isGrounded = true;

    #region - Getters/Setters -

    public EPlayerState State {
        get { return state;  }
        set { state = value; }
    }
    public EPlayerDirection Direction {
        get { return direction; }
        set { direction = value; }
    }

    public bool IsRunning {
        get { return isRunning;  }
        set { isRunning = value;  }
    }

    public bool IsGrounded {
        get { return isGrounded; }
        set { isGrounded = value; }
    }

    // End - Getters/Setters -
    #endregion

    // End - States -
    #endregion

    #region - Movement -

    private Vector2 moveInput;

    void OnJump(InputValue jumpVal) {
        if (jumpVal.isPressed && isGrounded) {
            state = EPlayerState.Jumping;
            rb.velocity = new Vector2(rb.velocity.x, 5.0f);
        }
    }

    void OnMove(InputValue moveVal) {
        moveInput = moveVal.Get<Vector2>();
    }

    // End - Movement -
    #endregion

    private Animator animator;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(moveInput);
    }
}
