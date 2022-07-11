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
    float timer;
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<AnimatorController>();
        animator.StateIdle();
    }
    private void Update()
    {
        SpeedBoostControl();
        PlayerInput();
        StackerAnimChecker();
    }

    private void SpeedBoostControl()
    {
        if (GetComponent<Builder>().IsBuilding())
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

   



}
