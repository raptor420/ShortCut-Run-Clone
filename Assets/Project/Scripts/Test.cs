using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Touch touch;
    private Vector2 touchPos;
    float movespeed = 5;
    // Start is called before the first frame update
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {

                Quaternion YRot = Quaternion.Euler(0, touch.deltaPosition.x * movespeed * Time.deltaTime, 0);

                transform.rotation = transform.rotation * YRot;
            }


        }
        transform.position += transform.forward * 10 * Time.deltaTime;
    }
}
