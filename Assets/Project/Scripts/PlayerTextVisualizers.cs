using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class PlayerTextVisualizers : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtPlace;
    [SerializeField] TextMeshProUGUI txtStacker;
    int amount;
    bool onroutine;
    public void UpdatetxtPlace(int place)
    {
        if (place==0)
        {
            txtPlace.text = "";
            return;
        }
        var attach = "";
        if (place == 1)
        {
            attach = "st";
        }
        else if(place == 2)
        {
            attach = "nd";
        }
            else if (place == 3)
                {
            attach = "rd";
                }
        else
        {
            attach = "th";
        }
        txtPlace.text = place + attach;
    }

    public void UpdatetxtStacker()
    {
        amount++;

        txtStacker.text = amount.ToString();
        StopAllCoroutines();
        txtStacker.transform.localScale = Vector3.one;

        StartCoroutine(txtCoroutine());
    }
    IEnumerator txtCoroutine()
    {
        txtStacker.transform.DOPunchScale(Vector3.one*.25f, .1f);
        txtStacker.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        txtStacker.gameObject.SetActive(false);
        amount = 0;
        txtStacker.transform.localScale = Vector3.one;
    }
}
