using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    private CharacterController controller;
    private Animator animator;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private float walkSpeed = 1.5f;
    [SerializeField] private float runSpeed = 3.0f;
    [SerializeField] private float jumpPower = 5f;

    //　下方向に強制的に加える力
    [SerializeField] private Vector3 addForceDownPower = Vector3.down;

    [SerializeField] Transform footPrints; //生成した足跡を格納しておく親オブジェクト
    [SerializeField] private GameObject footPrint;

    //カメラの参照
    [SerializeField] private Camera Camera;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        if (controller.isGrounded)
        {

            velocity = Vector3.zero;

            // 入力の取得とノーマライズ処理
            var input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            if (input.magnitude > 0f)
            {
                //スペースキーが押されているかで速度を切り替える
                float currentSpeed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? runSpeed : walkSpeed;

                // ノーマライズして移動速度を掛けることで、斜め方向でも同じ速度にする
                Vector3 normalizedInput = input.normalized;

                //カメラの正面方向を取得
                Vector3 cameraForward = Camera.transform.forward;
                cameraForward.y = 0; //高さは無視する
                cameraForward.Normalize();

                //カメラの右方向
                Vector3 cameraRight = Camera.transform.right;
                cameraRight.y = 0;
                cameraRight.Normalize();

                //カメラの方向に基づいて移動ベクトルを計算
                Vector3 moveDirection = cameraForward * normalizedInput.z + cameraRight * normalizedInput.x;

                // キャラクターの向きを移動方向に合わせる
                if (moveDirection != Vector3.zero)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * 10f);

                }

                // 移動速度を掛け合わせてvelocityを設定
                velocity += moveDirection * currentSpeed;

                animator.SetFloat("Speed", currentSpeed); // 入力の強さをアニメーションに反映
            }
            else
            {
                animator.SetFloat("Speed", 0f);
            }

            if (Input.GetButtonDown("Jump")
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")
                )
            {
                animator.SetTrigger("Jump");
                velocity.y += jumpPower;
            }
            else
            {
                //　ジャンプキーを押していない時は下向きに力を加える
                velocity += addForceDownPower;
            }
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        //　下向きのオフセット値を足して動かす
        controller.Move(velocity * Time.deltaTime);
    }

    //足跡プレハブ生成のメソッド
    void GenerateFootprint()
    {
        Vector3 footPosition = transform.position;
        var footPrintObj = Instantiate(footPrint, footPosition, Quaternion.identity);

        footPrintObj.transform.parent = footPrints;
    }

    void GenerateFootprintRight()
    {
        Vector3 footPosition = new Vector3(transform.position.x + 0.03f, transform.position.y, transform.position.z + 0.1f);
        var footPrintObj = Instantiate(footPrint, footPosition, Quaternion.identity);

        footPrintObj.transform.parent = footPrints;
    }

    void GenerateFootprintLeft()
    {
        Vector3 footPosition = new Vector3(transform.position.x - 0.03f, transform.position.y, transform.position.z + 0.1f);
        var footPrintObj = Instantiate(footPrint, footPosition, Quaternion.identity);

        footPrintObj.transform.parent = footPrints;
    }
}
