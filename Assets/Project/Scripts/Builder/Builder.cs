using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//using stacjking
//namespace building
public class Builder : MonoBehaviour
{
    [SerializeField] GameObject buildPrefab;
    [SerializeField] Transform footPos;
    float waitTime = 0.1f;
    float timer=0;
    [SerializeField]bool building;
    [SerializeField] LayerMask layermask;
    private void Start()
    {
    }
    public void Update()
    {
        transform.GetComponent<Rigidbody>().useGravity = !building;

        BuildChecker();
        BuildMethod();
       
        
    }

    public bool IsBuilding()
    {
        return building;
    }

    private void BuildMethod()
    {
        if (!building) return;
       
        timer += Time.deltaTime;
        if (timer >= waitTime)
        {
            //build;
            GetComponent<Stacker>().UseItem();
           var plank =  Instantiate(buildPrefab, new Vector3(footPos.position.x, 1, footPos.position.z), transform.rotation);
          
            var initRot = plank.transform.rotation;
            plank.transform.DOPunchScale(Vector3.one*.25f, 1).OnComplete(()=>plank.GetComponent<Collider>().enabled=true);
            AudioManager.instance.PlayAudio(AudioManager.instance.build);

            timer = 0;
        }
    }

  

    private void BuildChecker()
    {
        Debug.DrawRay(new Vector3(transform.position.x,transform.position.y+1,transform.position.z), -transform.up, Color.red);
        RaycastHit hit;
      //  if (Physics.BoxCast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z) + transform.forward*.5f, Vector3.one * .25f, Vector3.down, out hit,Quaternion.identity,5))
     // if ((Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z) + transform.forward, Vector3.down, out hit,2.5f)))
      if ((Physics.Raycast(transform.position+ Vector3.one, Vector3.down, out hit,3f)))
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
                        building = false;
                        return;
                    }

                    building = true;
                }
                else
                {
                    building = false;
                }


            }

        }
        //if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z) + transform.forward, -transform.up, out hit))
        //{
        //    if (hit.collider != null)
        //    {
        //        Debug.Log(hit.collider.name);
        //        if (hit.collider.gameObject == this) return;
        //        if (hit.collider.CompareTag("Water"))
        //        {
        //           // Debug.Log("WATER");
        //            // water
        //           building = true;
        //        }
        //        else
        //        {
        //            building = false;
        //        }


        //    }

        //}


    }
    private void OnDrawGizmos()
    {
        RaycastHit hit;
      
        //if ((Physics.Raycast(transform.position + Vector3.one, Vector3.down, out hit, 3f)) &&GetComponent<PlayerController>()!=null)
        //    Debug.Log(hit.collider.name);
    }
}
