using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalHandler : DoorTeleport
{
    [SerializeField] protected Sprite[] listSprites;

    [SerializeField] protected DoorTeleport finalDoor;


    public void SetPortalSprite(int portalIndex)
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = listSprites[portalIndex];
        if(portalIndex == 2)
        {
            LinkedDoor = finalDoor;
        }
    }
}
