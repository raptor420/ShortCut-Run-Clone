using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Touch touch;
    private Vector2 touchPos;
   [SerializeField] float rotateSpeed = 5;
   [SerializeField] float moveSpeed = 5;
    [SerializeField] AnimatorController animator;
    bool startRun= false;
    [SerializeField] Transform characterBody;
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<AnimatorController>();
    }
    private void Update()
    {
        PlayerInput();
        StackerAnimChecker();
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

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit,10))
        {
            Debug.Log(hit.transform.name);

        }
    }
}
