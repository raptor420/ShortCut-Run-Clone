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
    float waitTime = 0.15f;
    float timer=0;
    [SerializeField]bool building;
    [SerializeField] LayerMask layermask;
    private void Start()
    {
    }
    public void Update()
    {
        BuildChecker();
        BuildMethod();

    }

    private void BuildMethod()
    {
        if (!building) return;
        timer += Time.deltaTime;
        if (timer >= waitTime)
        {
            //build;
            GetComponent<Stacker>().UseItem();
           var plank =  Instantiate(buildPrefab, new Vector3(footPos.position.x, footPos.position.y, footPos.position.z), transform.rotation);
            // plank.transform.DOScale(Vector3.zero, .1f).OnComplete(() =>

            //plank.transform.DOScale(new Vector3(2, 0.1f, 0.5f), .2f)

            // ) ;
            // ;
            var initRot = plank.transform.rotation;
            plank.transform.DOPunchScale(Vector3.one*.25f, .5f);
            //plank.transform.DOLocalRotate(new Vector3(0, 0, -30), .1f, RotateMode.LocalAxisAdd).SetDelay(.25f).OnComplete(() => plank.transform.DOLocalRotate(new Vector3(0, 0, 60), .1f, RotateMode.LocalAxisAdd));
            //plank.transform.DOLocalRotateQuaternion(initRot,.1f).SetDelay(.4f);
            timer = 0;
        }
    }

  

    private void BuildChecker()
    {
        Debug.DrawRay(transform.position, Vector3.down*5, Color.green);
        RaycastHit hit;
        if (Physics.Raycast(footPos.position, Vector3.down, out hit, 5))
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject == this) return;
                if (hit.collider.CompareTag("Water"))
                {
                    Debug.Log("WATER");
                    // water
                    building = true;
                }
                else
                {
                    building = false;
                }


            }

        }
    }
}
