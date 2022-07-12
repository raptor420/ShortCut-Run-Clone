using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
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
        AudioManager.instance.PlayAudio(AudioManager.instance.pickup);
    }
    public void UseItem()
    {

        if (stackedItems.Count > 0)
        {
            Destroy(stackedItems[stackedItems.Count - 1]);
            stackedItems.RemoveAt(stackedItems.Count - 1);
            stackAmount--;
        }
    }
    public void ThrowStacks ()
    {
       StartCoroutine( ThrowStackCoroutine());
    }

    public IEnumerator ThrowStackCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        if (stackedItems.Count > 0)
            foreach (var item in stackedItems)
            {
                item.transform.SetParent( null);
                item.transform.DOJump(transform.position+ (transform.forward * 25 )+ Vector3.down*2, 20, 1, Random.Range(2.5f,5));
            }
        stackedItems.Clear();
        stackAmount = 0;
        yield return null;
    }
}
