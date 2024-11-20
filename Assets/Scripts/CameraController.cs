using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera mainCamera;   // 通常時のカメラ
    public CinemachineVirtualCamera endingCamera; // エンディング用のカメラ

    public void SwitchToEndingCamera()
    {
        // エンディングカメラを優先
        mainCamera.Priority = 5;      // 優先度を低くする
        endingCamera.Priority = 10;  // 優先度を高くする
    }
    
    public void SwitchTomainCamera()
    {
        
        mainCamera.Priority = 10;     
        endingCamera.Priority = 5;  
    }
}
