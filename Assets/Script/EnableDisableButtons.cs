using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableButtons : MonoBehaviour
{
    public GameObject enabled1;
    public GameObject enabled2;
    public GameObject enabled3;
    public GameObject disabled1;
    public GameObject disabled2;
    public GameObject disabled3;
    public Material defaultMaterial;

    public void activate()
    {
        enabled1.SetActive(false);
        enabled2.SetActive(false);
        enabled3.SetActive(false);
        disabled1.SetActive(true);
        disabled2.SetActive(true);
        disabled3.SetActive(true);
        GameObject childButton = transform.GetChild(0).gameObject;
        childButton.GetComponent<Renderer>().sharedMaterial = defaultMaterial;
    }
}
