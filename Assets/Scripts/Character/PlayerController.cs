using UnityEngine;

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



    // [SerializeField] Collider groundCheckCol; //前方向に地面があるかの判定を行うコライダー

    ////崖の前では静止する
    //public float rayDistance = 1.0f; // レイキャストの距離
    //public LayerMask groundLayer;   // 地面を判定するレイヤー
    //public float stopThreshold = 0.5f; // 崖の手前で停止する距離

    private float pushBackTime = 0f;  // 崖に押し戻されてからの時間
    private float pushBackDuration = 0.2f; // 押し戻し後に移動を無視する時間

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

            // 崖の前では移動入力を無視する
            if (pushBackTime > 0f)
            {
                // 押し戻しの時間が経過したら移動入力を許可
                pushBackTime -= Time.deltaTime;
                input = Vector3.zero; // 入力を無視
            }

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

                //// 地面を判定するレイキャスト
                //if (IsGroundAhead())
                //{
                // 移動速度を掛け合わせてvelocityを設定
                if (IsGroundAhead())
                {
                    Debug.Log("地面があります");
                    velocity += moveDirection * currentSpeed;

                    //}
                    //else
                    //{
                    //    // 崖際では少し押し戻す
                    //    Vector3 pushBackDirection = -transform.forward * 0.5f; // 後方に押し戻すベクトル
                    //    controller.Move(pushBackDirection);
                    //}

                    //animator.SetFloat("Speed", currentSpeed); // 入力の強さをアニメーションに反映
                }
                else
                {
                    Debug.Log("崖です");
                    // 崖の前に来たら少し後退させる
                    Vector3 pushBackDirection = -transform.forward * 0.1f; // 後方に押し戻すベクトル
                    controller.Move(pushBackDirection);
                    pushBackTime = pushBackDuration; // 押し戻し後の無視時間を設定
                    currentSpeed = 0f; // 移動を止める
                }
                animator.SetFloat("Speed", currentSpeed); // 入力の強さをアニメーションに反映
            }
            else
            {
                // 入力がない場合はSpeedを0に設定
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



    bool IsGroundAhead()
    {
        //複数のレイを飛ばして地面を判定

        // 足元の少し前にレイを出す
        Vector3 origin = transform.position + Vector3.up * 0.5f; 

        // 正面の少し前、左右に少しズラした位置にレイを飛ばす
        Vector3 forwardLeft = origin + transform.forward * 0.5f + transform.right * -0.25f; // 左側
        Vector3 forwardRight = origin + transform.forward * 0.5f + transform.right * 0.25f;  // 右側

        // 地面判定をレイで行う
        Ray rayCenter = new Ray(origin, Vector3.down);
        Ray rayLeft = new Ray(forwardLeft, Vector3.down);
        Ray rayRight = new Ray(forwardRight, Vector3.down);

        // 各レイの長さ（キャラクターの接地を十分にチェックする）
        float rayDistance = 1.0f;

        // すべてのレイが地面に当たれば地面があると判定
        bool isGrounded = Physics.Raycast(rayCenter, rayDistance) && Physics.Raycast(rayLeft, rayDistance) && Physics.Raycast(rayRight, rayDistance);

        // レイをデバッグ表示
        Debug.DrawRay(rayCenter.origin, rayCenter.direction * rayDistance, Color.red);
        Debug.DrawRay(rayLeft.origin, rayLeft.direction * rayDistance, Color.green);
        Debug.DrawRay(rayRight.origin, rayRight.direction * rayDistance, Color.blue);

        return isGrounded;
    }
}
