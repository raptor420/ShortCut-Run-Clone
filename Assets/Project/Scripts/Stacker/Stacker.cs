using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stacker : MonoBehaviour
{
    int stackAmount;



    private void Update()
    {
      
    }
   public int getStackAmount()
    {

        return stackAmount;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {

            Debug.Log("pick");
            Destroy(other.gameObject);
            stackAmount++;
        }
    }
}
