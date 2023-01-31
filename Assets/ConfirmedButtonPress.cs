using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmedButtonPress : MonoBehaviour
{

    public void confirm(Material confirmMaterial)
    {
        GameObject childButton = transform.GetChild(0).gameObject;
        childButton.GetComponent<Renderer>().sharedMaterial = confirmMaterial;
    }
}
