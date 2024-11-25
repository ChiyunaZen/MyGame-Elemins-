using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

public class EleminController : MonoBehaviour, IFollowMov
{
    public Material material;
    public float alphaDecreaseAmount = 0.05f; // 透明度をあげる量

    NavMeshAgent navMeshAgent;
    Animator animator;
    [SerializeField] Light eleminLight;
    Transform playerTransform;

    bool isNearSymbol = false; //近くにシンボルが存在するかのフラグ

    public float addLightRange = 0.1f;　//照らす範囲の増え幅
    public float addLightIntensity = 0.1f; //ライトの強さの増え幅

    [SerializeField] GameManager manager;

    GameObject goalLight;

    [SerializeField] CameraController cameraController;

    [SerializeField] EndingCamera endingCam;



    void Start()
    {

        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.enabled = true;

        animator = GetComponent<Animator>();
        // eleminLight = GetComponentInChildren<Light>();

        //Debug.Log($"Initial Light Range: {eleminLight.range}");
        //Debug.Log($"Initial Light Intensity: {eleminLight.intensity}");

        eleminLight.range = 0;
        eleminLight.intensity = 0;


        material.SetColor("_Color", new Color(1f, 1f, 1f, 0.0f)); //マテリアルを透明に設定
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {

            playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Playerを見つけて設定
        }

        if (navMeshAgent != null)
        {
            // エージェントとシャドウレイヤーとの衝突を無効にする
            navMeshAgent.gameObject.layer = LayerMask.NameToLayer("Shadow"); // シャドウレイヤーを設定
        }
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);
        if (Input.GetKeyDown(KeyCode.Z))
        {
            OnGoalReached();
        }
    }

    // 透明度を上げるメソッド
    public void DecreaseTransparency()
    {
        // 現在の色を取得
        Color currentColor = material.GetColor("_Color");

        // アルファ値を増加させる
        float newAlpha = Mathf.Clamp(currentColor.a + alphaDecreaseAmount, 0f, 1f); // 0未満にならないように制限
        currentColor.a = newAlpha; // 新しいアルファ値を設定

        // マテリアルに新しい色を設定
        material.SetColor("_Color", currentColor);

        if (currentColor.a <= 0.9f)
        {
            eleminLight.range = currentColor.a;
        }
        else
        {
            eleminLight.range += addLightRange;

            if (eleminLight.intensity <= 3.5f)
            {
                eleminLight.intensity += addLightIntensity;
            }
        }
    }


    public void OnDetectPlayer()
    {

        if (!isNearSymbol && navMeshAgent.enabled)
        {
            navMeshAgent.destination = playerTransform.position;
        }

    }

    public void GoToSymbol(GameObject symbolObject)
    {

        if (!isNearSymbol && navMeshAgent.enabled)
        {
            //NavMeshのターゲットをシンボルに変更
            navMeshAgent.destination = symbolObject.transform.position;
            isNearSymbol = true;

            // シンボルに到達するまで確認するコルーチンを開始
            StartCoroutine(MoveToSymbolAndReturn(symbolObject));
        }
    }


    // シンボルへ移動し、光を分けた後プレイヤーに戻るコルーチン
    private IEnumerator MoveToSymbolAndReturn(GameObject symbolObject)
    {


        // シンボルに向かって移動
        while (Vector3.Distance(transform.position, symbolObject.transform.position) > 0.5f)
        {
            yield return null; // シンボルに到達するまで待機
        }


        SymbolController symbolController = symbolObject.GetComponent<SymbolController>();
        if (symbolController != null)
        {
            symbolController.ActivateSymbolLight();

            // SymbolControllerで設定された範囲と強度を取得して光を減少
            float decreaseRange = symbolController.getLightRange;
            float decreaseIntensity = symbolController.getLightIntensity;

            // 光の範囲と強度を一度だけ減少
            DecreaseLightRange(decreaseRange);
            DecreaseLightIntensity(decreaseIntensity);

            Collider collider = symbolObject.GetComponent<Collider>();
            Destroy(collider);
        }

        yield return new WaitForSeconds(1.5f);

        // ターゲットをプレイヤーに戻す
        navMeshAgent.destination = playerTransform.position;
        isNearSymbol = false;

    }


    //Eleminライトの範囲を減らすメソッド
    public void DecreaseLightRange(float value)
    {

        eleminLight.range = Mathf.Max(eleminLight.range - value, 0f); // 最小値を1にする
    }

    //Eleminライトの明るさを減らすメソッド
    public void DecreaseLightIntensity(float value)
    {

        eleminLight.intensity = Mathf.Max(eleminLight.intensity - value, 0f); // 最小値を0.1にする
    }

    //最初にプレイヤー追従を開始するメソッド
    public void StartFollowing()
    {

        if (!isNearSymbol && navMeshAgent.enabled)
        {
            navMeshAgent.destination = playerTransform.position;
        }
    }

    //NavMeshでの追従をストップさせるメソッド
    public void StopFollowing()
    {
        Debug.Log("Eleminを止める");
        navMeshAgent.isStopped = true;
        StartCoroutine(RestartFollowing());
    }

    //追従を再開させるコルーチン
    public IEnumerator RestartFollowing()
    {
        yield return new WaitForSeconds(3f);
        navMeshAgent.isStopped = false;
    }


    public float moveSpeed = 1f;        // 移動速度
    public float rotationSpeed = 3f; // 回転速度



    public void GoalToElemin(GameObject target)
    {
        Debug.Log("ゴールを見つけた");
        goalLight = target;


        // NavMeshAgentを無効化（既に無効になっている場合も確認）
        if (navMeshAgent != null && navMeshAgent.enabled)
        {
            navMeshAgent.enabled = false;
        }

        Vector3 targetPoint = target.transform.position;

        // ゴールに向かう移動処理を開始するコルーチンを呼び出し
        StartCoroutine(MoveToGoal(targetPoint));
    }

    private IEnumerator MoveToGoal(Vector3 targetPoint)
    {
        bool cameraSwitched = false; // カメラが切り替え済みかを記録

        while (Vector3.Distance(transform.position, targetPoint) > 0.2f) // ゴールに近づくまでループ
        {
            // Lerpで徐々にゴールに向かう
            transform.position = Vector3.Lerp(transform.position, targetPoint, moveSpeed * Time.deltaTime);

            // 近づいたらカメラを切り替える
            if (!cameraSwitched && Vector3.Distance(transform.position, targetPoint) <= 10f)
            {
                cameraController.SwitchToEndingCamera(); // カメラ切り替え処理を呼び出す
                cameraSwitched = true;  // カメラが切り替え済みであることを記録
            }

            // 回転 
            Vector3 direction = targetPoint - transform.position;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            yield return null; // 次のフレームまで待機
        }

        // ゴールに近づいたら終了処理
        OnGoalReached();
    }



    // ゴール到達時の処理
    private void OnGoalReached()
    {
        Debug.Log("ゴールに到達しました！");
        cameraController.SwitchTomainCamera();
        ExtinctionElemin();
    }

    public void ExtinctionElemin()
    {
        animator.SetTrigger("Extinction");
        StartCoroutine(Sunrise());

    }

    IEnumerator Sunrise()
    {
        yield return new WaitForSeconds(2f);

        manager.Ending();

        yield return new WaitForSeconds(2);

        Destroy(goalLight.GetComponent<MeshRenderer>());
        Destroy(gameObject);

    }

}