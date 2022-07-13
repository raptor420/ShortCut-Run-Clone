using System.Collections;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
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
    [Header("FINISHER")]
    bool finished;
    public int playerPlace;
    public int finalPlayerPlace;
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
        if (finished)
        {
            return;
        }
        
        playerPlace = GameManager.instance.GetPlayerPlace();
        GetComponent<PlayerTextVisualizers>().UpdatetxtPlace(playerPlace);
        SpeedBoostControl();
        PlayerInput();
        StackerAnimChecker();
        if (GetComponent<Jumper>().IsJumping())
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
        else
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }

    }

    private void LateUpdate()
    {
        
      
    }
    private void SpeedBoostControl()
    {
        if (GetComponent<Builder>().IsBuilding()||GetComponent<Jumper>().IsJumping())
        {
            timer += Time.deltaTime;
            moveSpeed = Mathf.Lerp(8, 16, timer);
        }

        else
        {
            timer = 0;
            moveSpeed = 8;
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
       // GetComponent<Rigidbody>().velocity= transform.forward * moveSpeed ;
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
            Death(collision);
        }
    }

    private void Death(Collision collision)
    {
        death = true;
        GetComponent<Jumper>().enabled = false;
        GetComponent<Builder>().enabled = false;
        GetComponent<Stacker>().enabled = false;
        AudioManager.instance.PlayAudio(AudioManager.instance.splash);

        VCamFollowNullSetter();
        collision.collider.GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = true;
        transform.DOMoveY(-10, 3);
        animator.StateDeath();
        // Time.timeScale = .5f;
        GetComponent<ParticlePlayer>().PlayWaterParticle();
        Destroy(this.gameObject, 5);
        GameManager.instance.LoadScene();
    }

 

    private void VCamFollowNullSetter()
    {
        vCamFollow.Follow = null;
        vCamFollow.LookAt = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            Debug.Log("wrtf");
            StartCoroutine( FinishGame());

        }
    }

    public bool IsFinished()
    {
        return finished;
    }
    private IEnumerator FinishGame()
    {
        finished = true;
        AudioManager.instance.PlayAudio(AudioManager.instance.victory);
        transform.DOMove(GameManager.instance.finisher.transform.position + Vector3.one, 1);
        yield return new  WaitForSeconds(1);
        if (GetComponent<Stacker>().getStackAmount() > 0) { animator.StateThrow(); yield return (StartCoroutine(GetComponent<Stacker>().ThrowStackCoroutine())); }

        yield return new  WaitForSeconds(2);
        vCamFollow.LookAt = GameManager.instance.finisher.transform;
        transform.DOLocalRotate(new Vector3(0,180,0),.1f,RotateMode.LocalAxisAdd);
        if (playerPlace == 1)
        {
            animator.StateVictory();

        }
        else
        {
            animator.StateLose();

        }
        GameManager.instance.LoadScene();

    }
}
