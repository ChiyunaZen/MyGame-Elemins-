using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCollider : MonoBehaviour
{
    [SerializeField]GameObject followObj;

    [SerializeField] CameraController cameracon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           followObj.GetComponent<IFollowMov>().StartFollowing();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (followObj.CompareTag("Enemy"))
            {
            if (other.CompareTag("Player"))
            {
                cameracon.SwitchToEndingCamera();
                StartCoroutine(EnemyDestroy());
            }
            

        }
    }

    IEnumerator  EnemyDestroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(followObj);

    }
}
