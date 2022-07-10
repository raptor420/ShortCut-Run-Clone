using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//namespace stacking
public class Stacker : MonoBehaviour
{
    int stackAmount;
    [SerializeField] Transform stackHolderParent;
    [SerializeField] GameObject stackPrefab;
    List<GameObject> stackedItems = new List<GameObject>();

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
            PickUp(other);
        }
    }

    private void PickUp(Collider other)
    {
       // Debug.Log("pick");
        Destroy(other.gameObject);
        stackAmount++;
        var obj = Instantiate(stackPrefab, stackHolderParent);
        obj.transform.localPosition = Vector3.zero + Vector3.up * obj.transform.localScale.y * (stackAmount - 1);
        stackedItems.Add(obj);
    }
    public void UseItem()
    {

        if (stackedItems.Count > 0)
        {
            Destroy(stackedItems[stackedItems.Count - 1]);
            stackedItems.RemoveAt(stackedItems.Count - 1);

        }
    }
}
