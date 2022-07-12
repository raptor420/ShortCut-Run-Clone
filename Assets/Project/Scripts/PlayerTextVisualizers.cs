using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerTextVisualizers : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtPlace;
    [SerializeField] TextMeshProUGUI txtStacker;


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
}
