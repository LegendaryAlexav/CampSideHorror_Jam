using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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


public class PlayerStates : MonoBehaviour
{

    private EPlayerDirection direction = EPlayerDirection.Right;
    private EPlayerState state = EPlayerState.Idle;

    private bool isRunning = false;

    public EPlayerState State {
        get { return state; }
        set { state = value; }
    }
    public EPlayerDirection Direction {
        get { return direction; }
        set { direction = value; }
    }

    public bool IsRunning {
        get { return isRunning; }
        set { isRunning = value; }
    }
}
