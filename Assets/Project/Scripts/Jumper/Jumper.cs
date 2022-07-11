using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Jumper : MonoBehaviour
{
    public float jumpDist;
    public float jumpTime;
    bool jumping;
    public void Jump()
    {
        Debug.Log("JUMP");
        // transform.DOJump(transform.forward*5,5,1,3);
        transform.GetComponent<Rigidbody>().AddForce(new Vector3(0, 10,0),ForceMode.Impulse);
        transform.GetComponent<Rigidbody>().useGravity = true;
     // transform.GetComponent<Rigidbody>().doju
    }
    public bool IsJumping()
    {
    return    jumping;
    }

    public void Land()
    {


    }
    private void Update()
    {
        JumpChecker();
    }

    private void JumpChecker()
    {
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z) + transform.forward, -transform.up, Color.red);
        RaycastHit hit;
        if (Physics.BoxCast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z) + transform.forward, Vector3.one * .5f, -transform.up, out hit,Quaternion.identity,2))
        {
            if (hit.collider != null)
            {
               // Debug.Log(hit.collider.name);
                if (hit.collider.gameObject == this) return;
                if (hit.collider.CompareTag("Water"))
                {
                    // Debug.Log("WATER");
                    // water
                    if (GetComponent<Stacker>().getStackAmount() <= 0)
                    {
                        if (jumping) return;

                        jumping = true;
                        Jump();


                    }


                }
                else 
                {
                    jumping = false;
                  //  transform.GetComponent<Rigidbody>().useGravity = false;

                   //landed
                }


            }

        }
   


    }
}
