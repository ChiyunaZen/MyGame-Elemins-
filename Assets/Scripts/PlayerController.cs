using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    private CharacterController controller;
    private Animator animator;
    private Vector3 velocity = Vector3.zero; //

    [SerializeField]private float moveSpeed = 1.5f;
    [SerializeField]private float jumpPower = 5f;
    
    //　下方向に強制的に加える力
    [SerializeField] private Vector3 addForceDownPower = Vector3.down;

    [SerializeField] private GameObject footPrint;

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

            var input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            if (input.magnitude > 0f)
            {
                animator.SetFloat("Speed", input.magnitude);
                transform.LookAt(transform.position + input);
                velocity += input * moveSpeed;
            }
            else
            {
                animator.SetFloat("Speed", 0f);
            }

            if (Input.GetButtonDown("Jump")
                && !animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")
                )
            {
                animator.SetBool("Jump", true);
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

    void GenerateFootprint()
    {
        Vector3 footPosition = transform.position;
        Instantiate(footPrint, footPosition, Quaternion.identity);
    }
}
