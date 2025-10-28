using TarodevController;
using UnityEngine;

public enum EPlayerState : byte {
    Idle,
    Running,
    Jumping,
}

public class Player : MonoBehaviour
{
    //private PlayerController playerController;

    private EPlayerState state;

    [SerializeField]
    private float groundColliderRadius;
    [SerializeField]
    private float groundColliderCastDistance;
    [SerializeField]
    private LayerMask groundLayer;

    private PlayerController playerController;

    #region - Getters/Setters -

    public EPlayerState State {
        get { return state; }
        set { state = value; }
    }

    public bool IsGrounded {
        get { return Physics2D.CircleCast(transform.position, groundColliderRadius, -transform.up, groundColliderCastDistance, groundLayer); }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        state = EPlayerState.Idle;
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position - transform.up * groundColliderCastDistance, groundColliderRadius);
    }

    public void ApplyStateChange(EPlayerState newState) {
        if (newState != state)
        {
            state = newState;
            switch (state)
            {
                case EPlayerState.Idle:
                    playerController.IdleAnim();
                    break;
                case EPlayerState.Running:
                    playerController.RunAnim();
                    break;
                case EPlayerState.Jumping:
                    playerController.JumpAnim();
                    break;
            }

        }

    }

}
