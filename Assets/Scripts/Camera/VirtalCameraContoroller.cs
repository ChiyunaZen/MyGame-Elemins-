using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ZoomController : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera; // 使用するVirtualCamera
    [SerializeField] float zoomSpeed = 5f;                    // ズームのスピード調整
    [SerializeField] float minFOV = 15f;                       // 最小FOV（ズームインの限界）
    [SerializeField] float maxFOV = 60f;                      // 最大FOV（ズームアウトの限界）

    private float targetFOV;                                  // 目標とするFOV

    [SerializeField] float rotationSpeed = 2f;                // 回転の速度
    //[SerializeField] float minXOffset = -90;                  // X方向の最小オフセット
    //[SerializeField] float maxXOffset = 90;                   // X方向の最大オフセット
    [SerializeField] float minYOffset = -3f;                  // Y方向の最小オフセット
    [SerializeField] float maxYOffset = 5f;                   // Y方向の最大オフセット
    

    private Vector3 followOffset;                             // Follow Offsetの現在の値
    private CinemachineTransposer transposer;                 //transposterの参照

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
    }

    void Start()
    {
        if (virtualCamera != null)
        {
            // 初期のFOVを設定
            targetFOV = virtualCamera.m_Lens.FieldOfView = 30f;

            if (transposer != null)
            {

                //初期のtranspoaterの位置を設定
                followOffset = new Vector3(0, 4.5f, -10);
                transposer.m_FollowOffset = followOffset;
            }

        }
    }

    void Update()
    {

        // マウスのスクロール入力を取得
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollInput != 0)
        {
            // スクロール入力に基づいて目標FOVを更新
            targetFOV -= scrollInput * zoomSpeed;
            targetFOV = Mathf.Clamp(targetFOV, minFOV, maxFOV); // FOVの範囲を制限
        }

        // 滑らかにズーム
        virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, targetFOV, Time.deltaTime * zoomSpeed);



        // 右クリックを押している間だけオフセット変更を有効化
        if (Input.GetMouseButton(2))
        {
            // マウス移動量を取得
            //float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // X軸のオフセットを上下の範囲制限内で更新
           // followOffset.x += mouseX * rotationSpeed;
            //followOffset.x = Mathf.Clamp(followOffset.x, minXOffset, maxXOffset); 
            
            // Y軸のオフセットを上下の範囲制限内で更新
            followOffset.y += mouseY * rotationSpeed;
            followOffset.y = Mathf.Clamp(followOffset.y, minYOffset, maxYOffset);

           

            // Follow Offsetの値を更新
            if (transposer!=null)
            transposer.m_FollowOffset = followOffset;
        }
    }
}
