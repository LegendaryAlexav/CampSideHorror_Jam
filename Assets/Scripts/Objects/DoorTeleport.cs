using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;
using UnityEngine.Assertions;

public class DoorTeleport : MonoBehaviour
{
    [SerializeField] private DoorTeleport LinkedDoor;
    [SerializeField] private PolygonCollider2D CameraConfiner2D;

    private void Start()
    {
        Assert.IsNull(LinkedDoor); // Door not Linked
        Assert.IsNull(CameraConfiner2D); // Camera Bounding Box Not Linked
        Assert.IsTrue(LinkedDoor == this); // Can't Set Linked Door as This Door
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

    public DoorTeleport GetLinkedDoor()
    {
        return LinkedDoor;
    }

    public PolygonCollider2D GetCameraCollider()
    {
        return CameraConfiner2D;
    }
    
}
