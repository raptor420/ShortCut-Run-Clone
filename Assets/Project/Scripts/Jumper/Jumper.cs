using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Jumper : MonoBehaviour
{
    public float jumpDist;
    public float jumpTime;
    bool jumping;
    [SerializeField] LayerMask ground;
    public void Jump()
    {
        jumping = true;
        Debug.Log("JUMP");
        // transform.DOJump(transform.forward*5,5,1,3);
        transform.GetComponent<Rigidbody>().AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
        transform.GetComponent<Rigidbody>().useGravity = true;
    }

    public void BoostedJump()
    {
        jumping = true;

        Debug.Log("we in boosted Jump");
        transform.GetComponent<Rigidbody>().AddForce(new Vector3(0, 30, 0), ForceMode.Impulse);
        transform.GetComponent<Rigidbody>().useGravity = true;
    }
    public bool IsJumping()
    {
        return jumping;
    }

    public void Land()
    {


    }
    private void Update()
    {
        JumpChecker();
        if (GetComponent<Builder>().IsBuilding())
        {
            jumping = false; 
        }
    }

    private void JumpChecker()
    {
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z) + transform.forward, -transform.up, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z) + transform.forward, Vector3.down, out hit))
        {
            if (hit.collider != null)
            {
              //  Debug.Log(hit.collider.name);
                if (hit.collider.gameObject == this) return;
                if (hit.collider.CompareTag("Water"))
                {
                    // Debug.Log("WATER");
                    // water
                    if (GetComponent<Stacker>().getStackAmount() <= 0)
                    {
                        if (jumping) return;

                      
                        Jump();


                    }


                }
                else
                {
                    //  transform.GetComponent<Rigidbody>().useGravity = false;
                    //landed
                }
            }

            //if (Physics.BoxCast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z) + transform.forward*.5f, Vector3.one * .2f, Vector3.down, out hit,Quaternion.identity,2))
            //{
            //    if (hit.collider != null)
            //    {
            //        Debug.Log(hit.collider.name);
            //        if (hit.collider.gameObject == this) return;
            //        if (hit.collider.CompareTag("Water"))
            //        {
            //            // Debug.Log("WATER");
            //            // water
            //            if (GetComponent<Stacker>().getStackAmount() <= 0)
            //            {
            //                if (jumping) return;

            //                jumping = true;
            //                Jump();


            //            }


            //        }
            //        else 
            //        {
            //            //  transform.GetComponent<Rigidbody>().useGravity = false;
            //            jumping = false;
            //           //landed
            //        }


            //    }

            //}





        }
    }


    public void JumpPodJump()
    {
        BoostedJump();
        Debug.Log("Wee jumopuin");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            jumping = false;

        }

       else if (collision.collider.CompareTag("JumpPod"))
        {
            jumping = true;
            JumpPodJump();

        }
      
    }
    private void OnTriggerEnter(Collider other)
    {
          if (other.CompareTag("JumpPod"))
        {
            jumping = true;
            JumpPodJump();

        }

    }
}
