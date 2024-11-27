using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EndingCamera : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform eleminTransform;

    CinemachineVirtualCamera cam;

    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        cam.LookAt = eleminTransform;
        cam.Follow =eleminTransform;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void LookElemin()
    {
        cam.m_Lens.FieldOfView = 15f;
    }

    public void LookLuxi()
    {
        cam.LookAt = playerTransform;
        cam.Follow = playerTransform;
    }
}
