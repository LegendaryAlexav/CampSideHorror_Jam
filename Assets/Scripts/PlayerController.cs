using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerStates playerStates;

    private const float c_moveSpeed = 10.0f, c_jumpForce = 5.0f, c_gravity = -9.8f;

    private float verticalVelocity = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update() {
        
    }

    private void DoGravity() {
        verticalVelocity = verticalVelocity * c_gravity * Time.deltaTime;
        controller.Move(new Vector2(0, verticalVelocity));
    }

    #region - Movement -


    public void Move(float direction) {
        controller.Move(move);
    }

    public void Jump() {
        if (controller.isGrounded) {
            verticalVelocity = c_jumpForce;
        }   
    }
    public void Transport() {
        if (controller.isGrounded) {

        }
    }

    // End - Movement -
    #endregion
}
