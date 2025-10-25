using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Traveling,
        Jumping,
        Running,
        Turning,
        Dying,
    }

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

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
