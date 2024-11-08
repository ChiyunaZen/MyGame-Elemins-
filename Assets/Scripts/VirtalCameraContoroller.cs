using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtalCameraContoroller : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
