using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private CapsuleCollider2D collider;
    private PlayerStates playerStates;
    private PlayerController controller;


    private void Start() {
        animator = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider2D>();
        
        playerStates = GetComponent<PlayerStates>();

        controller = GetComponent<PlayerController>();
    }
}
