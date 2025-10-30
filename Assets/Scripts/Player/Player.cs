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

    private PlayerController playerController;
    [SerializeField] private GameObject transitionToNextMap;
    [SerializeField] private CameraHandler cameraHandler;
    [SerializeField] private PortalHandler portalHandler;
    [SerializeField] private MonsterHandler monsterHandler;

    [SerializeField] private int itemsCollected = 0;

    #region - Getters/Setters -

    public EPlayerState State {
        get { return state; }
        set { state = value; }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        state = EPlayerState.Idle;
        playerController = GetComponent<PlayerController>();

    }

    public void SetNewCameraBoundry(PolygonCollider2D collider)
    {
        cameraHandler.ChangeConfinerCollider(collider);
    }

    public void PlayTransition()
    {
        transitionToNextMap.GetComponent<Animator>().Play("CutToNextMap");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectable"))
        {
            collision.gameObject.SetActive(false);
            itemsCollected++;

            // Calculate Door Index
            int doorIndex = itemsCollected;
            if (doorIndex <= 1)
            {
                doorIndex = 0;
            }
            else if (doorIndex >= 2 && doorIndex <= 3)
            {
                doorIndex = 1;
            }
            else if(doorIndex <= 4)
            {
                doorIndex = 2;
            }

            // Calculate Monster Index
            int monsterIndex = 0;
            if (itemsCollected > 1) {
                monsterIndex = 1;
            }

            monsterHandler.SetMonsterType(monsterIndex);
            portalHandler.SetPortalSprite(doorIndex);
        }
    }

    public void EnableMonster(bool enable)
    {
        monsterHandler.SetMonsterEnable(enable);
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
