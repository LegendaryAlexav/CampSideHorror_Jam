using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPlayerState : byte {
    Idle,
    Walking,
    Running,

}

public enum EPlayerDirection : byte {
    Right,
    Left,
}

public class Player : MonoBehaviour
{
    //private PlayerController playerController;

    private EPlayerState state;
    private EPlayerDirection direction;

    [SerializeField]
    private float groundColliderRadius;
    [SerializeField]
    private float groundColliderCastDistance;
    [SerializeField]
    private LayerMask groundLayer;

    #region - Getters/Setters -

    public EPlayerState State {
        get { return state; }
        set { state = value; }
    }

    public EPlayerDirection Direction {
        get { return direction; }
        set { direction = value; }
    }

    public bool IsGrounded {
        get { return Physics2D.CircleCast(transform.position, groundColliderRadius, -transform.up, groundColliderCastDistance, groundLayer); }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position - transform.up * groundColliderCastDistance, groundColliderRadius);
    }

}
