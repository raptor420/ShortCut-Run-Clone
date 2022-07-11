using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    private Touch touch;
    private Vector2 touchPos;
   [SerializeField] float rotateSpeed = 5;
   [SerializeField] float moveSpeed = 5;
    [SerializeField] AnimatorController animator;
    bool startRun= false;
    [SerializeField] Transform characterBody;
    [SerializeField]  CinemachineVirtualCamera vCamFollow;
    float timer;
    bool death;
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<AnimatorController>();
        if (vCamFollow == null)
        {
            vCamFollow= GameObject.FindGameObjectWithTag("VcamFollow").
            GetComponent<CinemachineVirtualCamera>();
        }
        animator.StateIdle();
    }
    private void Update()
    {
        if (death) return;
        SpeedBoostControl();
        PlayerInput();
        StackerAnimChecker();
        if (GetComponent<Jumper>().IsJumping())
        {
            GetComponent<Rigidbody>().constraints =RigidbodyConstraints.FreezeRotation;
        }
        else
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation| RigidbodyConstraints.FreezePositionY;
        }
      
    }

    private void SpeedBoostControl()
    {
        if (GetComponent<Builder>().IsBuilding()||GetComponent<Jumper>().IsJumping())
        {
            timer += Time.deltaTime;
            moveSpeed = Mathf.Lerp(5, 8, timer);
        }

        else
        {
            timer = 0;
            moveSpeed = 5;
        }
    }

    private void StackerAnimChecker()
    {
        if (!startRun) return;
        if (GetComponent<Stacker>().getStackAmount() > 0)
        {
            characterBody.localRotation = Quaternion.identity;

            GetComponent<AnimatorController>().StateCarry();
        }
        else
        {
            characterBody.localRotation = Quaternion.identity;

            GetComponent<AnimatorController>().StateRun();

        }
    }

    private void PlayerInput()
    {
        if (Input.touchCount > 0)
        {
            CheckStartRun();
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {

                Quaternion YRot = Quaternion.Euler(0, touch.deltaPosition.x * rotateSpeed * Time.deltaTime, 0);

                transform.rotation = transform.rotation * YRot;
            }


        }
        if(startRun)
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    private void CheckStartRun()
    {
        if (!startRun)
        {
            startRun = true;
           characterBody.localRotation = Quaternion.identity;
            animator.StateRun();

        }
    }


    private void OnCollisionEnter(Collision collision)
    {
       if( collision.collider.CompareTag("Water"))
         {// better to make it OnDeath, but theres no time
            death = true;
            vCamFollow.Follow = null;
            vCamFollow.LookAt = null;
            collision.collider.GetComponent<Collider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = true;
            animator.StateDeath();
           // Time.timeScale = .5f;
            GetComponent<ParticlePlayer>().PlayWaterParticle();
            Destroy(this.gameObject,5);
            SceneManager.LoadScene(0);
        }
    }


}
