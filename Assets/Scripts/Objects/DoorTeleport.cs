using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class DoorTeleport : MonoBehaviour
{
    [SerializeField] DoorTeleport LinkedDoor;

    private Transform teleportPoint;

    private void Start()
    {
        if (LinkedDoor == null)
            return;
        teleportPoint = GetComponent<GameObject>().transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            player.CollideWithDoor(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            player.CollideWithDoor(null);
        }
    }

    public GameObject GetLinkedDoor()
    {
        return LinkedDoor.gameObject;
    }
    
}
