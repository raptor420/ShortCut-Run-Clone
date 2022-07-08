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
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<AnimatorController>();
    }
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {

                Quaternion YRot = Quaternion.Euler(0, touch.deltaPosition.x * rotateSpeed * Time.deltaTime, 0);

                transform.rotation = transform.rotation * YRot;
            }


        }
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        animator.StateRun();
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
