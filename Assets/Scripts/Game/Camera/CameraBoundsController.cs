using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundsController : MonoBehaviour
{
    public CompositeCollider2D bounds;

    private void Awake()
    {
        GameObject.Find("CM vcam1").GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = bounds;
    }
}
