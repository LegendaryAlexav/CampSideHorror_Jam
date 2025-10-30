using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraHandler : MonoBehaviour
{

    [SerializeField] private CinemachineConfiner2D confiner2D;

    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNull(confiner2D);
    }
    
    public void ChangeConfinerCollider(PolygonCollider2D newConfinerCollider)
    {
        confiner2D.m_BoundingShape2D = newConfinerCollider;
    }
}
