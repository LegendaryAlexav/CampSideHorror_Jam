using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private PlayerController characterController;
    private InputAction ia_Move, ia_Jump, ia_Transport;
    
    public void OnMove(InputAction.CallbackContext context) {
        if ((context.phase == InputActionPhase.Performed || context.phase == InputActionPhase.Canceled)) {
            float direction = context.ReadValue<float>();
            characterController.Move(direction);
        }
    }

    public void OnJump(InputValue value) {
        if (value.isPressed) {
            characterController.Jump();
        }
    }

    public void OnTransport(InputValue value) {
        if (value.isPressed) {
            characterController.Transport();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<PlayerController>();

        PlayerInput input = GetComponent<PlayerInput>();
        ia_Move = input.actions["Move"];
        ia_Jump = input.actions["Jump"];
        ia_Transport = input.actions["Transport"];
    }

    private void Update() {

    }

    private void OnJumpPerformed(InputAction.CallbackContext context) {

    }

    private void OnTransportPerformed(InputAction.CallbackContext context) {

    }
}
