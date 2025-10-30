using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;
using UnityEngine.Assertions;

public class DoorTeleport : MonoBehaviour
{
    [SerializeField] protected DoorTeleport LinkedDoor;
    [SerializeField] protected PolygonCollider2D CameraConfiner2D;

    public bool isSnowyEntrance = false;

    private void Awake()
    {
        GameObject cameraConfinerObject = FindGameObjectInChildWithTag(transform.parent.gameObject, "CameraBoundingBox");
        if (cameraConfinerObject != null)
        {
            CameraConfiner2D = cameraConfinerObject.GetComponent<PolygonCollider2D>();
        }
    }

    public static GameObject FindGameObjectInChildWithTag(GameObject parent, string tag)
    {
        Transform t = parent.transform;

        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).gameObject.tag == tag)
            {
                return t.GetChild(i).gameObject;
            }

        }

        return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            player.GetComponent<PlayerController>().CollideWithDoor(this);

            if(!isSnowyEntrance) // Disable when touching Portal
                player.EnableMonster(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            player.GetComponent<PlayerController>().CollideWithDoor(null);
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
